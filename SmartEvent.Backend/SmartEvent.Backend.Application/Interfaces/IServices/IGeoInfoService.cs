using SmartEvent.Backend.Application.DTOs;
using SmartEvent.Backend.Core.Common;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Application.Interfaces.IServices;

public interface IGeoInfoService
{
    public Task<Result<LocationDto>> GetEventLocationByAddressAsync(string address);
    public double CalculateDistanceDifference(double eventLat, double eventLong, double userLat, double userLong);
}