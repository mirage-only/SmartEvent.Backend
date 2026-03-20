using SmartEvent.Backend.Application.Common.Models;
using SmartEvent.Backend.Application.DTOs.UserDTOs.Requests;
using SmartEvent.Backend.Application.DTOs.UserDTOs.Responses;
using SmartEvent.Backend.Core.Common;

namespace SmartEvent.Backend.Application.Interfaces.IServices
{
    public interface IUserService
    {
        Task<Result<AuthorizeUserResponseDto>> AuthorizeUserAsync(LoginUserRequestDto? request);
        Task<Result<AuthorizeUserResponseDto>> RegisterUserAsync(RegisterUserRequestDto? request);
        Task<Result<PagedResult<GetUserDto>>> GetAllUsersByAdminAsync(PaginationParams paginationParams);
        Task<Result<bool>> DeleteUserSoftAsync(Guid? id); 
    }
}
