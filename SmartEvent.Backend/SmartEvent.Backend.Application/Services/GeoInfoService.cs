using System.Net;
using SmartEvent.Backend.Application.DTOs;
using SmartEvent.Backend.Application.Interfaces.IServices;
using SmartEvent.Backend.Core.Common;

namespace SmartEvent.Backend.Application.Services;

public class GeoInfoService(ILocationService locationService): IGeoInfoService
{
    public async Task<Result<LocationDto>> GetLocationByAddressAsync(string address)
    {
        if(string.IsNullOrWhiteSpace(address)) 
            return Result<LocationDto>
                .Failure("Invalid Address", HttpStatusCode.BadRequest);

        var location = 
            await locationService.GetCoordinatesByAddressAsync(address);

        return location.IsSuccess ? location : Result<LocationDto>
            .Failure("Something went wrong", HttpStatusCode.FailedDependency);
    }
}