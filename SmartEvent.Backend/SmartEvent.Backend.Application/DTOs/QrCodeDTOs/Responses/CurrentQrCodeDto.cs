namespace SmartEvent.Backend.Application.DTOs.QrCodeDTOs.Responses;

public class CurrentQrCodeDto
{
    public string TokenValue {get; set;} =  string.Empty;
    public DateTime ExpiresAt {get; set;}
}