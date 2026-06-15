using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartEvent.Backend.Application.DTOs.AttendanceDTOs.Requests;
using SmartEvent.Backend.Application.Interfaces.IServices;

namespace SmartEvent.Backend.Api.Controllers;

[ApiController]
[Route("attendance")]
[Authorize]
public class AttendanceController(IAttendanceService attendanceService): BaseApiController
{
    [HttpPost("AttendManually")]
    [Authorize(Policy = "AtLeastEmployee")]
    public async Task<IActionResult> AttendManually([FromBody] ManualAttendanceDto manualAttendanceDto)
    {
        var response = await attendanceService.ConfirmManually(manualAttendanceDto);

        return HandleResult(response);
    }
    
    [HttpPost("AttendByOrganizer")]
    [Authorize(Policy = "AtLeastStudent")]
    public async Task<IActionResult> AttendByOrganizer([FromBody] QrAttendanceDto qrAttendanceDto)
    {
        var response = await attendanceService.ConfirmAttendanceByQrAsync(qrAttendanceDto);

        return HandleResult(response);   
    }
}