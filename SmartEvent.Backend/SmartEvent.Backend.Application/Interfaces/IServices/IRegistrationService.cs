using SmartEvent.Backend.Core.Common;

namespace SmartEvent.Backend.Application.Interfaces.IServices;

public interface IRegistrationService
{
    public Task<Result<Guid>> RegisterForEventAsync(Guid eventId);
    
    public Task<Result<Guid>> IsRegistrationExistAsync(Guid eventId);
}