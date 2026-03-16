namespace SmartEvent.Backend.Application.DTOs.EventDTOs.Responses;

public class LocationDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Address { get; set; } = string.Empty;
}