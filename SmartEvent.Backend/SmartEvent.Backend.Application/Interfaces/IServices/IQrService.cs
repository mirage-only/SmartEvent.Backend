using SmartEvent.Backend.Application.DTOs.QrCodeDTOs.Responses;
using SmartEvent.Backend.Core.Common;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Application.Interfaces.IServices;

public interface IQrService
{
    public Task<Result<CurrentQrCodeDto>> GetOrGenerateQrCodeAsync(Guid? id);
}