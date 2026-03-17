using System.Globalization;
using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using SmartEvent.Backend.Application.DTOs;
using SmartEvent.Backend.Application.Interfaces.IServices;
using SmartEvent.Backend.Core.Common;

namespace SmartEvent.Backend.Infrastructure.ExternalServices.LocationServices;

public class NominatimService(IHttpClientFactory httpClientFactory, IMemoryCache cache): ILocationService
{
    private const string NominatimBaseUrl = "https://nominatim.openstreetmap.org/";
    
    public async Task<Result<LocationDto>> GetCoordinatesByAddressAsync(string address)
    {
        if (string.IsNullOrEmpty(address))
            return Result<LocationDto>.Failure("Address can't be empty", HttpStatusCode.BadRequest);

        string cacheKey = $"addr_{address.ToLower().Trim()}";
        
        if(cache.TryGetValue(cacheKey, out LocationDto? resultFromCache)) 
            return Result<LocationDto>.Success(resultFromCache);
        
        var requestUrl = $"{NominatimBaseUrl}/search?q={Uri.EscapeDataString(address)}&format=jsonv2&limit=1&accept-language=ru";

        return await SendNominatimRequestAsync(requestUrl, cacheKey);
    }

    public async Task<Result<LocationDto>> GetAddressByCoordinatesAsync(double latitude, double longitude)
    {
        string cacheKey = $"rev_{latitude.ToString(CultureInfo.InvariantCulture)}_{longitude.ToString(CultureInfo.InvariantCulture)}";

        if (cache.TryGetValue(cacheKey, out LocationDto? resultFromCache))
            return Result<LocationDto>.Success(resultFromCache!);
        
        var url = $"{NominatimBaseUrl}/reverse?lat={latitude.ToString(CultureInfo.InvariantCulture)}&lon={longitude.ToString(CultureInfo.InvariantCulture)}&format=jsonv2&accept-language=ru";
        
        return await SendNominatimRequestAsync(url, cacheKey, latitude, longitude);
    }

    private async Task<Result<LocationDto>> SendNominatimRequestAsync(string url, string cacheKey,
        double? latitude = null, double? longitude = null)
    {
        var client = httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Add("User-Agent", "SmartEventApp/1.0");
        
        var response = await client.GetAsync(url);
        if (!response.IsSuccessStatusCode)
            return Result<LocationDto>.Failure("The maps are temporarily unavailable!", response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        using var document = JsonDocument.Parse(json);
        JsonElement root;

        if (document.RootElement.ValueKind == JsonValueKind.Array)
        {
            if(document.RootElement.GetArrayLength() == 0)
                return Result<LocationDto>.Failure("Location not found!", HttpStatusCode.NotFound);
            root = document.RootElement[0];
        }
        else
        {
            root = document.RootElement;
            if(root.TryGetProperty("error", out _))
               return Result<LocationDto>.Failure("Coordinates not identified on maps!", HttpStatusCode.NotFound);
        }
        
        var result = new LocationDto
        {
            Latitude = latitude ??
                       double.Parse(root.GetProperty("lat").GetString()!, CultureInfo.InvariantCulture),
            Longitude = longitude ??
                        double.Parse(root.GetProperty("lon").GetString()!, CultureInfo.InvariantCulture),
            Address = root.GetProperty("display_name").GetString() ?? "Address not found!"
        };
            
        cache.Set(cacheKey, result, TimeSpan.FromDays(1));
            
        return Result<LocationDto>.Success(result);
    }
}