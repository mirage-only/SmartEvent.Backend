using SmartEvent.Backend.Api.Handlers;
using SmartEvent.Backend.Application;
using SmartEvent.Backend.Infrastructure;
using SmartEvent.Backend.Persistence;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

builder.Host.UseDefaultServiceProvider(options =>
{
    options.ValidateScopes = true;
    options.ValidateOnBuild = true;
});

const string specialCorsPolicy = "corsPolicy";

services.AddCors(options =>
{
    options.AddPolicy(name: specialCorsPolicy,
        policy =>
        {
            if (builder.Environment.IsDevelopment())
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }
            else
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }
        });
});

services
    .AddPersistence(configuration)
    .AddApplication()
    .AddInfrastructure(configuration);

services.AddHttpContextAccessor();

services.AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"))
    .AddPolicy("AtLeastEmployee", policy => policy.RequireRole("Employee", "Admin"))
    .AddPolicy("AtLeastStudent", policy => policy.RequireRole("Student", "Employee", "Admin"));

services.AddControllers();

services.AddSwaggerGen();
services.AddEndpointsApiExplorer();

services.AddProblemDetails();
services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

app.UseExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartEvent API v1");
    options.RoutePrefix = "swagger"; 
});

app.UseRouting(); 

app.UseCors(specialCorsPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
