using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartEvent.Backend.Application.Common.Models;
using SmartEvent.Backend.Application.DTOs.EventDTOs.Requests;
using SmartEvent.Backend.Application.Interfaces.IServices;
using SmartEvent.Backend.Core.Models;

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
    
    [HttpGet("get/{id:guid}")]
    public async Task<IActionResult> GetEventDetailsById(Guid id)
    {
        var response = await eventService.GetEventDetailsAsync(id);
        return HandleResult(response);
    }

    [HttpPost("add")]
    [Authorize(Policy = "AtLeastEmployee")]
    public async Task<IActionResult> AddEventAsync(AddEventDto addEventDto)
    {
        var response = await eventService.AddEventAsync(addEventDto);
        
        return HandleResult(response);
    }
    
    [HttpDelete("delete/{eventId:guid}")]
    public async Task<IActionResult> DeleteEvent(Guid eventId)
    {
        throw new NotImplementedException();
    }
    
    
}