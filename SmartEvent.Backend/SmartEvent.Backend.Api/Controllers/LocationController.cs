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
    [AllowAnonymous]
    public async Task<IActionResult> GetEventLocationByAddress(string address)
    {
        var response =
            await geoInfoService.GetLocationByAddressAsync(address);
        
        return HandleResult(response);
    }
}