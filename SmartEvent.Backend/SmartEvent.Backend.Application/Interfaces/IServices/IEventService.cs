using SmartEvent.Backend.Application.Common.Models;
using SmartEvent.Backend.Application.DTOs.EventDTOs.Responses;
using SmartEvent.Backend.Core.Common;

namespace SmartEvent.Backend.Application.Interfaces.IServices;

public interface IEventService
{
    public Task<Result<PagedResult<GetLightEventDto>>> GetLightEventsWithPaginationAsync(PaginationParams paginationParams);
    
    public Task<Result<GetEventDetailsDto>> GetEventDetailsAsync(Guid id);

}