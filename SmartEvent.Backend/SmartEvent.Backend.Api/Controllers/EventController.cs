using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartEvent.Backend.Application.Common.Models;
using SmartEvent.Backend.Application.Interfaces.IServices;

namespace SmartEvent.Backend.Api.Controllers;

[ApiController]
[Route("events")]
[Authorize]
public class EventController(IEventService eventService): BaseApiController
{
    [HttpPost("getLightEventsWithPagination")]
    public async Task<IActionResult> GetLightEventsWithPagination([FromBody] PaginationParams  paginationParams)
    {
        var response = await eventService.GetLightEventsWithPaginationAsync(paginationParams);

        return HandleResult(response);
    }
    
}