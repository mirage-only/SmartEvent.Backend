using System.Diagnostics;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SmartEvent.Backend.Core.Exceptions;
using SmartEvent.Backend.Shared.Contracts.AnalyticsEvents;

namespace SmartEvent.Backend.Api.Handlers;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger,
    IHostEnvironment hostEnvironment): IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        const string defaultTraceIdAndSpanId = "00000000000000000000000000000000";
        
        var traceId = Activity.Current?.TraceId.ToString();
        if (string.IsNullOrEmpty(traceId) || traceId == defaultTraceIdAndSpanId)
        {
            traceId = httpContext.TraceIdentifier;
        }
        
        var spanId = Activity.Current?.SpanId.ToString();
        if (spanId == defaultTraceIdAndSpanId) spanId = null;
        
        logger.LogError(exception,"Something went wrong. TraceId: {TraceId}, SpanId: {SpanId},  Path: {Path}", traceId, spanId, httpContext.Request.Path);

        var (statusCode, title) = exception switch
        {
            ValidationException => (StatusCodes.Status400BadRequest, "Validation Error"),
            BaseException e => (e.StatusCode, "Application Error"),
            _ => (StatusCodes.Status500InternalServerError, "Server Error")
        };

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = exception.Message,
            Instance = httpContext.Request.Path
        };

        if (exception is ValidationException validationException)
        {
            problemDetails.Extensions.Add("errors", validationException.Errors);
        }

        if (statusCode == StatusCodes.Status500InternalServerError)
        {
            var publishEndpoint = httpContext.RequestServices.GetRequiredService<IPublishEndpoint>();
            
            await publishEndpoint.Publish(new SystemLogEvent
            {
                LogLevel = "Error",
                ServiceName = "Monolith",
                EnvironmentName = hostEnvironment.EnvironmentName,
                TraceId = traceId,
                SpanId = spanId,
                Message = exception.Message,
                Exception = exception.ToString(),
                Properties = new Dictionary<string, object?>()
                {
                    ["HttpPath"] = httpContext.Request.Path.Value,
                    ["HttpMethod"] = httpContext.Request.Method
                }
            }, cancellationToken);
        }

        httpContext.Response.StatusCode = statusCode;
        
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        
        return true;
    }
}