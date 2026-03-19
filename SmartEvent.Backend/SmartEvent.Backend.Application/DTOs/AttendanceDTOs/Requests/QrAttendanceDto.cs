using SmartEvent.Backend.Core.Enums;

namespace SmartEvent.Backend.Application.DTOs.AttendanceDTOs.Requests;

public class QrAttendanceDto
{
    public Guid EventId { get; set; } = Guid.Empty;
    public string ScannedToken { get; set; } =  string.Empty;
    public double?  UserLatitude { get; set; } = null;
    public double?  UserLongitude { get; set; } = null;
}