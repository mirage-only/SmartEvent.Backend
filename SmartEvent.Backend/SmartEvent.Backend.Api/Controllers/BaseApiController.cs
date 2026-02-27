using Microsoft.AspNetCore.Mvc;
using SmartEvent.Backend.Core.Common;

namespace SmartEvent.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController: ControllerBase
{
    protected IActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            if(result.Data == null) return NoContent();
            
            return  Ok(result.Data);
        }
        
        return StatusCode(result.StatusCode, new ProblemDetails
        {
            Status = result.StatusCode,
            Title = GetTitleForStatus(result.StatusCode),
            Detail = result.ErrorMessage,
            Instance = HttpContext.Request.Path
        });
    }

    private string GetTitleForStatus(int resultStatusCode) => resultStatusCode switch
    {
        400 => "Bad Request",
        401 => "Unauthorized",
        403 => "Forbidden",
        404 => "Not Found",
        409 => "Conflict",
        _ => "Server Error"
    };
}