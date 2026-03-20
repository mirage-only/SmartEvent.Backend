using System.Net;
using AutoMapper;
using SmartEvent.Backend.Application.Interfaces.ICommon;
using SmartEvent.Backend.Application.Interfaces.IServices;
using SmartEvent.Backend.Core.Common;
using SmartEvent.Backend.Core.Exceptions;
using SmartEvent.Backend.Core.Interfaces.IRepositories;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Application.Services;

public class RegistrationService(IRegistrationRepository registrationRepository, IEventRepository eventRepository, IMapper mapper, IUserContext userContext): IRegistrationService
{
    public async Task<Result<Guid>> RegisterForEventAsync(Guid eventId)
    {
        const string errorMessage = "Something went wrong during registration";
        const string eventNotFound = "Event not found";
        const string creatorCantRegister = "Creator can't register for the own event";
        const string registrationExistsErrorMessage = "Registration already exists";
        
        var userId = userContext.UserId;
        if (userId == Guid.Empty) return Result<Guid>.Failure(errorMessage, HttpStatusCode.BadRequest);
        if (eventId == Guid.Empty) return Result<Guid>.Failure(errorMessage, HttpStatusCode.BadRequest);
        
        var eventEntity = await eventRepository.GetEventById(eventId);
        if (eventEntity == null) return Result<Guid>.Failure(eventNotFound, HttpStatusCode.NotFound);
        if(eventEntity.CreatorId ==  userId) return Result<Guid>.Failure(creatorCantRegister, HttpStatusCode.BadRequest);

        var registrationExist = await registrationRepository.GetRegistrationByEventIdAndUserId(eventId, userId);
        if (registrationExist != null) return Result<Guid>.Failure(registrationExistsErrorMessage, HttpStatusCode.NotFound);
        
        var registrationEntity = new Registration()
        {
            EventId = eventId,
            UserId = userId
        };

        var result = await registrationRepository.AddRegistration(registrationEntity);
        if (result.Id == Guid.Empty) return Result<Guid>.Failure(errorMessage, HttpStatusCode.BadRequest);

        return Result<Guid>.Success(result.Id);
    }

    public async Task<Result<Guid>> IsRegistrationExistAsync(Guid eventId)
    {
        const string idCantBeNullMessage = "Id can't be null";
        
        var userId = userContext.UserId;
        if (userId == Guid.Empty) return Result<Guid>.Failure(idCantBeNullMessage, HttpStatusCode.BadRequest);
        if (eventId == Guid.Empty) return Result<Guid>.Failure(idCantBeNullMessage, HttpStatusCode.BadRequest);

        var registration = await registrationRepository.GetRegistrationByEventIdAndUserId(eventId, userId);
        if (registration == null) return Result<Guid>.Success(Guid.Empty);
        return Result<Guid>.Success(registration.Id);
    }
}