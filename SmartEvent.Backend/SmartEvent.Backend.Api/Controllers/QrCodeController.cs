using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartEvent.Backend.Application.Interfaces.IServices;

namespace SmartEvent.Backend.Api.Controllers;

[ApiController ]
[Route("qr")]
[Authorize]
public class QrCodeController(IQrService qrService): BaseApiController
{
    [HttpGet("getActiveQr/{id:guid}")]
    public async Task<IActionResult> GetActiveQrCode(Guid? id)
    {
        var response = await qrService.GetOrGenerateQrCodeAsync(id);
        
        return HandleResult(response);
    }
}