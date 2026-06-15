using System.Net;
using SmartEvent.Backend.Application.DTOs.AttendanceDTOs.Requests;
using SmartEvent.Backend.Application.Interfaces.ICommon;
using SmartEvent.Backend.Application.Interfaces.IServices;
using SmartEvent.Backend.Core.Common;
using SmartEvent.Backend.Core.Enums;
using SmartEvent.Backend.Core.Interfaces.IRepositories;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Application.Services;

public class AttendanceService(IAttendanceRepository attendanceRepository, IEventRepository eventRepository,
    IUserRepository userRepository, IGeoInfoService geoInfoService, IUserContext userContext): IAttendanceService
{
    private const string UnauthorizedUserMessage = "User is not authorized";
    private const string NotFoundUserMessage = "User not found";

    private const string EmptyEventIdMessage = "EventId can't be empty";
    private const string NotFoundEventMessage = "Event not found";

    private const string UserNotRegisteredMessage = "User is not registered for event";
    private const string UserAlreadyAttendedForEventMessage = "User is already attended for event";
    private const string NoPermissionsMessage = "No permissions to attend people";

    private const string NoActiveQrCodeMessage = "The event does not have an active QR code at the moment. Try refreshing the QR code";
    private const string InvalidQrCodeMessage = "Outdated or invalid QR code";

    private const string UserLocationUnavailable = "User location is unavailable, check access to geodata, please";
    private const string UserSoFarMessage = "User so far from event";
    
    public async Task<Result<Guid>> ConfirmAttendanceByQrAsync(QrAttendanceDto qrAttendanceDto)
    {
        var userId = userContext.UserId;
        if (userId == Guid.Empty) 
            return Result<Guid>.Failure(UnauthorizedUserMessage, HttpStatusCode.Unauthorized);

        if (qrAttendanceDto.EventId == Guid.Empty)
        {
            return Result<Guid>.Failure(EmptyEventIdMessage, HttpStatusCode.BadRequest);
        }

        var eventEntity = await eventRepository.GetEventForAttendanceAsync(qrAttendanceDto.EventId);
        if (eventEntity == null) 
            return Result<Guid>.Failure(NotFoundEventMessage, HttpStatusCode.NotFound);

        var basicValidation = ValidateBasicAttendance(eventEntity, userId);
        if (!basicValidation.IsSuccess) return basicValidation;
        
        var validQr = eventEntity.CurrentQrCode;
        if (validQr == null) 
            return Result<Guid>.Failure(NoActiveQrCodeMessage, HttpStatusCode.BadRequest);

        if (validQr.TokenValue != qrAttendanceDto.ScannedToken)
            return Result<Guid>.Failure(InvalidQrCodeMessage, HttpStatusCode.BadRequest);
        
        if (qrAttendanceDto.UserLatitude == null || qrAttendanceDto.UserLongitude == null)
            return Result<Guid>.Failure(UserLocationUnavailable, HttpStatusCode.BadRequest);

        var distance = geoInfoService.CalculateDistanceDifference(
            eventEntity.Latitude, eventEntity.Longitude, 
            qrAttendanceDto.UserLatitude.Value, qrAttendanceDto.UserLongitude.Value);

        if (distance > 100)
            return Result<Guid>.Failure(UserSoFarMessage, HttpStatusCode.Forbidden);
        
        return await CreateAndSaveAttendanceAsync(
            eventEntity.Id, userId, AttendanceMethod.Qr, null, validQr.Id, qrAttendanceDto.UserLatitude, qrAttendanceDto.UserLongitude);
    }
    
    public async Task<Result<Guid>> ConfirmManually(ManualAttendanceDto manualDto)
    {
        var employeeId = userContext.UserId;
        
        var eventEntity = await eventRepository.GetEventForAttendanceAsync(manualDto.EventId);
        if (eventEntity == null) 
            return Result<Guid>.Failure(NotFoundEventMessage, HttpStatusCode.NotFound);
        
        var hasRights = eventEntity.CreatorId == employeeId || 
                        eventEntity.Organizers.Any(organizer => organizer.UserId == employeeId);
        
        if (!hasRights)
            return Result<Guid>.Failure(NoPermissionsMessage, HttpStatusCode.Forbidden);
        
        var basicValidation = ValidateBasicAttendance(eventEntity, manualDto.TargetUserId);
        if (!basicValidation.IsSuccess) return basicValidation;
        
        var result = await CreateAndSaveAttendanceAsync(
            eventEntity.Id, manualDto.TargetUserId, AttendanceMethod.TeacherMarked, employeeId, null, null, null);
        
        return result;
    }
    
    private Result<Guid> ValidateBasicAttendance(Event eventEntity, Guid userId)
    {
        var isRegistered = eventEntity.Registrations.Any(r => r.UserId == userId);
        if (!isRegistered)
        {
            return Result<Guid>.Failure(UserNotRegisteredMessage, HttpStatusCode.Forbidden);   
        }

        var isAlreadyAttended = eventEntity.Attendances.Any(a => a.UserId == userId);
        if (isAlreadyAttended)
        {
            return Result<Guid>.Failure(UserAlreadyAttendedForEventMessage, HttpStatusCode.BadRequest);   
        }

        return Result<Guid>.Success(Guid.Empty);
    }
    
    private async Task<Result<Guid>> CreateAndSaveAttendanceAsync( Guid eventId, Guid userId, AttendanceMethod method,
        Guid? organizerId, Guid? qrCodeId, double? userLatitude, double? userLongitude)
    {
        var attendance = new Attendance
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            EventId = eventId,
            Method = method,
            ConfirmedByOrganizerId = organizerId,
            QrCodeId = qrCodeId,
            Latitude = userLatitude,
            Longitude = userLongitude
        };
        
        await attendanceRepository.AddAttendanceAsync(attendance);
        return Result<Guid>.Success(attendance.Id);
    }
}