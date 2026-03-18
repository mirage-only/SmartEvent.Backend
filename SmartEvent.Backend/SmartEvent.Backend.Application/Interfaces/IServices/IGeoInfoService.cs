using SmartEvent.Backend.Application.DTOs;
using SmartEvent.Backend.Core.Common;

namespace SmartEvent.Backend.Application.Interfaces.IServices;

public interface IGeoInfoService
{
    public Task<Result<LocationDto>> GetLocationByAddressAsync(string address);
}