using System.Net;
using SmartEvent.Backend.Application.DTOs;
using SmartEvent.Backend.Application.Interfaces.IServices;
using SmartEvent.Backend.Core.Common;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Application.Services;

public class GeoInfoService(ILocationService locationService): IGeoInfoService
{
    public async Task<Result<LocationDto>> GetEventLocationByAddressAsync(string address)
    {
        if(string.IsNullOrWhiteSpace(address)) 
            return Result<LocationDto>
                .Failure("Invalid Address", HttpStatusCode.BadRequest);

        var location = 
            await locationService.GetCoordinatesByAddressAsync(address);
    
        return location.IsSuccess ? location : Result<LocationDto>
            .Failure("Something went wrong", HttpStatusCode.FailedDependency);
    }

    public double CalculateDistanceDifference(double eventLat, double eventLong, double userLat, double userLong)
    {
        const int earthRadius = 6371000;
        var eventLatRad = ToRadians(eventLat);
        var eventLongRad = ToRadians(eventLong);
        var userLatRad = ToRadians(userLat);
        var userLongRad = ToRadians(userLong);
        var sinusLatitudesInSecondDegree = Math.Pow(Math.Sin((eventLatRad - userLatRad) / 2), 2);
        var sinusLongitudesInSecondDegree = Math.Pow(Math.Sin((eventLongRad - userLongRad) / 2), 2);
        var haversine = sinusLatitudesInSecondDegree + Math.Cos(eventLatRad) * Math.Cos(userLatRad) * sinusLongitudesInSecondDegree;
        var angularDistance = 2 * Math.Atan2(Math.Sqrt(haversine), Math.Sqrt(1 - haversine));
        var distance = earthRadius * angularDistance;
        if (distance < 0)
        {
            distance = -distance;
        }
        return distance;
    }
    
    private double ToRadians(double angle) => angle * (Math.PI / 180);
}