using AutoMapper;
using SmartEvent.Backend.Application.DTOs.EventDTOs.Responses;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Application.Common.Mapping;

public class EventMappingProfile: Profile
{
    public EventMappingProfile()
    {
        CreateMap<Event, GetLightEventDto>();

        CreateMap<Event, GetEventDetailsDto>();
    }
}