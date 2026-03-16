using SmartEvent.Backend.Application.DTOs;
using SmartEvent.Backend.Core.Common;

namespace SmartEvent.Backend.Application.Interfaces.IServices;

public interface ILocationService
{
    Task<Result<LocationDto>> GetCoordinatesByAddressAsync(string address);
    Task<Result<LocationDto>> GetAddressByCoordinatesAsync(double latitude, double longitude);
}