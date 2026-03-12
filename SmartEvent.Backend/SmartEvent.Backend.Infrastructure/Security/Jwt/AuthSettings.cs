namespace SmartEvent.Backend.Infrastructure.Security.Jwt;

public class AuthSettings
{
    public TimeSpan Expires { get; set; }
    public string SecretKey { get; set; }
}