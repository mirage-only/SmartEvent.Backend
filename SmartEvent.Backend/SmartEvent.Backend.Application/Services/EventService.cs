using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using SmartEvent.Backend.Application.Common.Extensions;
using SmartEvent.Backend.Application.Common.Models;
using SmartEvent.Backend.Application.DTOs.EventDTOs.Responses;
using SmartEvent.Backend.Application.Interfaces.IServices;
using SmartEvent.Backend.Core.Common;
using SmartEvent.Backend.Core.Interfaces.IRepositories;

namespace SmartEvent.Backend.Application.Services;

public class EventService(IEventRepository eventRepository, IMapper mapper): IEventService
{
    public async Task<Result<PagedResult<GetLightEventDto>>> GetLightEventsWithPaginationAsync(PaginationParams paginationParams)
    {
        var query = eventRepository.GetAllEvents();

        var mappedQuery = query.ProjectTo<GetLightEventDto>(mapper.ConfigurationProvider);

        var pagedResult =
            await mappedQuery.ToPagedResultAsync( paginationParams.PageNumber, paginationParams.PageSize);
        
        return Result<PagedResult<GetLightEventDto>>.Success(pagedResult);
    }

    public async Task<Result<GetEventDetailsDto>> GetEventDetailsAsync(Guid id)
    {
        if (id == Guid.Empty) 
            return Result<GetEventDetailsDto>.Failure("ID can't be empty", HttpStatusCode.BadRequest);

        var searchedEvent = await eventRepository.GetEventById(id);
        var result = mapper.Map<GetEventDetailsDto>(searchedEvent);
        
        return Result<GetEventDetailsDto>.Success(result);
    }
}