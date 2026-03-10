using static System.String;

namespace SmartEvent.Backend.Application.DTOs.EventDTOs.Responses;

public class GetEventDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = Empty;
    public string Description { get; set; } = Empty;
    public DateTime StartTime { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public Guid CreatorId { get; set; }
}