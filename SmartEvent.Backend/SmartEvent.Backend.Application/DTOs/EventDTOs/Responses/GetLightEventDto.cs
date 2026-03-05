namespace SmartEvent.Backend.Application.DTOs.EventDTOs.Responses;

public class GetLightEventDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description {get; set;}
    public string ImageUrl { get; set; }
    public DateTime StartDate { get; set; }

    public GetLightEventDto() {}
};