using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartEvent.Backend.Application.Interfaces.IServices;

namespace SmartEvent.Backend.Api.Controllers;

[ApiController]
[Route("location")]
[Authorize]
public class LocationController(IGeoInfoService geoInfoService) : BaseApiController
{
    [HttpGet("getEventLocationByAddress/{address}")]
    [Authorize(Policy = "AtLeastEmployee")]
    public async Task<IActionResult> GetEventLocationByAddress(string address)
    {
        var response =
            await geoInfoService.GetEventLocationByAddressAsync(address);
        
        return HandleResult(response);
    }
}