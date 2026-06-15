using System.Net;
using AutoMapper;
using SmartEvent.Backend.Application.DTOs.QrCodeDTOs.Responses;
using SmartEvent.Backend.Application.Interfaces.IServices;
using SmartEvent.Backend.Core.Common;
using SmartEvent.Backend.Core.Enums;
using SmartEvent.Backend.Core.Interfaces.IRepositories;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Application.Services;

public class QrService(IQrCodeRepository qrCodeRepository, IEventRepository eventRepository, IMapper mapper): IQrService
{
    public async Task<Result<CurrentQrCodeDto>> GetOrGenerateQrCodeAsync(Guid? eventId)
    {
        const string badRequestMessage = "Event Id can't be null or empty";
        const string notFoundMessage = "Event not found";
        const string cantSaveQrCodeMessage = "We can't save the QrCode now, try again later";
        if (eventId == null || eventId.Value == Guid.Empty) 
            return Result<CurrentQrCodeDto>.Failure(badRequestMessage, HttpStatusCode.BadRequest);
        var eventWithQrCodes = await eventRepository.GetEventWithQrCodeByIdAsync(eventId.Value);
        if (eventWithQrCodes == null) return Result<CurrentQrCodeDto>.Failure(notFoundMessage, HttpStatusCode.NotFound);
        var currentQr = eventWithQrCodes.CurrentQrCode;
        if (currentQr != null)
        {
            var currentQrCodeDto = mapper.Map<CurrentQrCodeDto>(currentQr);
            return Result<CurrentQrCodeDto>.Success(currentQrCodeDto);
        }
        var newQr = new QrCode
        {
            Id = Guid.NewGuid(),
            EventId = eventWithQrCodes.Id,
            TokenValue = Guid.NewGuid().ToString(),
            Status = Status.Active,
            ExpiresAt = DateTime.UtcNow.AddSeconds(eventWithQrCodes.QrCodeExpirationTime),
            CreatedAt = DateTime.UtcNow
        };
        var dbResponse = await qrCodeRepository.AddQrCodeAsync(newQr);
        if (!dbResponse) return Result<CurrentQrCodeDto>.Failure(cantSaveQrCodeMessage, HttpStatusCode.InternalServerError);
        var response = mapper.Map<CurrentQrCodeDto>(newQr);
        return Result<CurrentQrCodeDto>.Success(response);
    }
}