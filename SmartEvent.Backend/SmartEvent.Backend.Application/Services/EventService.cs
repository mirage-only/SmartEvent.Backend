using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using SmartEvent.Backend.Application.Common.Extensions;
using SmartEvent.Backend.Application.Common.Models;
using SmartEvent.Backend.Application.Common.Validators;
using SmartEvent.Backend.Application.DTOs.EventDTOs.Requests;
using SmartEvent.Backend.Application.DTOs.EventDTOs.Responses;
using SmartEvent.Backend.Application.Interfaces.ICommon;
using SmartEvent.Backend.Application.Interfaces.IServices;
using SmartEvent.Backend.Core.Common;
using SmartEvent.Backend.Core.Interfaces.IRepositories;
using SmartEvent.Backend.Core.Models;
using ValidationException = SmartEvent.Backend.Core.Exceptions.ValidationException;

namespace SmartEvent.Backend.Application.Services;

public class EventService(IEventRepository eventRepository, IMapper mapper, IUserContext userContext): IEventService
{
    public async Task<Result<PagedResult<GetLightEventDto>>> GetLightEventsWithPaginationAsync(PaginationParams paginationParams)
    {
        var query = eventRepository.GetAllEvents();

        var mappedQuery = query.ProjectTo<GetLightEventDto>(mapper.ConfigurationProvider);

        var pagedResult =
            await mappedQuery.ToPagedResultAsync( paginationParams.PageNumber, paginationParams.PageSize);
        
        return Result<PagedResult<GetLightEventDto>>.Success(pagedResult);
    }

    public async Task<Result<List<GetLightEventDto>>> GetLightEventsWhereUserRegisterAsync()
    {
        var userId = userContext.UserId;
        
        if (userId == Guid.Empty)
            throw new ValidationException("userId", "Invalid user id");
        
        var events = await eventRepository.GetEventsByUserIdAsync(userId);
        var result = mapper.Map<List<GetLightEventDto>>(events);
        
        return Result<List<GetLightEventDto>>.Success(result);
    }

    public async Task<Result<GetEventDetailsDto>> GetEventDetailsAsync(Guid id)
    {
        const string badIdMessage = "ID can't be empty";
        
        if (id == Guid.Empty) 
            return Result<GetEventDetailsDto>.Failure(badIdMessage, HttpStatusCode.BadRequest);

        var searchedEvent = await eventRepository.GetEventById(id);
        var result = mapper.Map<GetEventDetailsDto>(searchedEvent);
        
        return Result<GetEventDetailsDto>.Success(result);
    }

    public async Task<Result<Guid>> AddEventAsync(AddEventDto addEventDto)
    {
        const  string cantAddMessage = "We can't add event";
        
        const uint defaultExpirationTime = 30;
        
        if (addEventDto.QrCodeExpirationTime == 0)
            addEventDto.QrCodeExpirationTime = defaultExpirationTime;

        var validator = new AddEventDtoValidator();
        var  validationResult = await validator.ValidateAsync(addEventDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.ToDictionary());
        }

        var newEvent = mapper.Map<Event>(addEventDto);
        var creatorId = userContext.UserId;
        newEvent.CreatorId = creatorId;
        
        var result = await eventRepository.AddEvent(newEvent);

        if (result.Id == Guid.Empty)
            return Result<Guid>.Failure( cantAddMessage, HttpStatusCode.ExpectationFailed);
        
        return Result<Guid>.Success(newEvent.Id);
    }
}