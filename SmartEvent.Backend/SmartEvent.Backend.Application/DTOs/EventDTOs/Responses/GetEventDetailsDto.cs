using static System.String;

namespace SmartEvent.Backend.Application.DTOs.EventDTOs.Responses;

public class GetEventDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = Empty;
    public string Description { get; set; } = Empty;
    public string ImageUrl { get; set; } = Empty;
    public DateTime StartTime { get; set; }
    public LocationDto Location { get; set; } = new LocationDto();
    public string Room { get; set; } = string.Empty;
    public Guid CreatorId { get; set; }
}