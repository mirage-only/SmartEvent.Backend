using SmartEvent.Backend.Application.DTOs.AttendanceDTOs.Requests;
using SmartEvent.Backend.Core.Common;

namespace SmartEvent.Backend.Application.Interfaces.IServices;

public interface IAttendanceService
{
    public Task<Result<Guid>> ConfirmAttendanceByQrAsync(QrAttendanceDto qrAttendanceDto);
    public Task<Result<Guid>> ConfirmManually(ManualAttendanceDto manualDto);
}