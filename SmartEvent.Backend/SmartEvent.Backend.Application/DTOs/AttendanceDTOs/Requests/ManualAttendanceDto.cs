namespace SmartEvent.Backend.Application.DTOs.AttendanceDTOs.Requests;

public class ManualAttendanceDto
{
    public Guid EventId { get; set; }
    public Guid TargetUserId { get; set; }
}