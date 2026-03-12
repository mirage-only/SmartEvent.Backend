using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartEvent.Backend.Application.Interfaces.IServices;

namespace SmartEvent.Backend.Api.Controllers;

[ApiController]
[Route("registration")]
[Authorize]
public class RegistrationController(IRegistrationService registrationService): BaseApiController
{
    [HttpPost("regForEvent/{eventId:guid}")]
    public async Task<IActionResult> RegisterForEvent([FromRoute] Guid eventId)
    {
        var response = await registrationService.RegisterForEventAsync(eventId);
        
        return HandleResult(response);
    }
}