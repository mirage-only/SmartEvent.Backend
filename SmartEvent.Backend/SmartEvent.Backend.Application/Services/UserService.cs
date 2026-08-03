using System.Net;
using AutoMapper;
using SmartEvent.Backend.Application.Common.Models;
using SmartEvent.Backend.Application.Common.Validators;
using SmartEvent.Backend.Application.DTOs.UserDTOs.Requests;
using SmartEvent.Backend.Application.DTOs.UserDTOs.Responses;
using SmartEvent.Backend.Application.Interfaces.IServices;
using SmartEvent.Backend.Core.Common;
using SmartEvent.Backend.Core.Interfaces.IRepositories;
using SmartEvent.Backend.Core.Models;
using ValidationException = SmartEvent.Backend.Core.Exceptions.ValidationException;
using AutoMapper.QueryableExtensions;
using MassTransit;
using SmartEvent.Backend.Application.Common.Extensions;
using SmartEvent.Backend.Application.Interfaces.ICommon;
using SmartEvent.Backend.Shared.Contracts;
using SmartEvent.Backend.Shared.Contracts.Analytics.Enums;

namespace SmartEvent.Backend.Application.Services
{
    public class UserService(IUserRepository userRepository,
        IMapper mapper, IPasswordHasher passwordHasher,
        IJwtService jwtService, IPublishEndpoint publishEndpoint,
        IUserContext userContext, IAuthLogger authLogger) : IUserService
    {
        private const string NotFoundUserMessage = "User not found";
        private const string IncorrectIdMessage = "User id can't be null or empty";
        private const string InvalidPasswordMessage = "Check your email or  password";
        private const string UserExistMessage = "User with this  email already exists";
        
        public async Task<Result<AuthorizeUserResponseDto>> RegisterUserAsync(RegisterUserRequestDto? request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var validator = new RegisterUserRequestDtoValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.ToDictionary());
            }

            var existingUser = await userRepository.GetUserByEmail(request.Email);
            
            if (existingUser != null)
            {
                return  Result<AuthorizeUserResponseDto>.Failure(UserExistMessage,  HttpStatusCode.Conflict);
            }

            var user = mapper.Map<User>(request);
            user.PasswordHash = passwordHasher.Hash(request.Password);
            user.UserRole = Core.Enums.UserRole.Student;

            await userRepository.AddUser(user);

            await publishEndpoint.Publish(new UserRegisteredIntegrationEvent
            {
                UserId = user.Id,
                Email = user.Email,
                Name = user.FirstName
            });
            
            var token = jwtService.GenerateJwtToken(user);

            var responseDto = new AuthorizeUserResponseDto { JwtToken = token };
            
            return Result<AuthorizeUserResponseDto>.Success(responseDto);
        }
        
        public async Task<Result<AuthorizeUserResponseDto>> AuthorizeUserAsync(LoginUserRequestDto? request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var user = await userRepository.GetUserByEmail(request.Email);
            
            if(user == null)
            {
                await authLogger.LogAuthAsync(request.Email, null,
                    status: AuthStatusEnum.Failure, failureReason: NotFoundUserMessage);
                
                return Result<AuthorizeUserResponseDto>
                    .Failure(NotFoundUserMessage, HttpStatusCode.NotFound);
            }

            if (!passwordHasher.Verify(user.PasswordHash, request.Password))
            {
                await authLogger.LogAuthAsync(user.Email, user.Id,
                    AuthStatusEnum.Failure,  InvalidPasswordMessage);
                
                return Result<AuthorizeUserResponseDto>
                    .Failure(InvalidPasswordMessage, HttpStatusCode.Unauthorized);
            }
            
            var token = jwtService.GenerateJwtToken(user);
            
            await authLogger.LogAuthAsync(user.Email,  user.Id,  AuthStatusEnum.Success);

            var responseDto = new AuthorizeUserResponseDto()
            {
                JwtToken = token
            };
            
            var response = Result<AuthorizeUserResponseDto>.Success(responseDto);
            
            return response;
        }

        public async Task<Result<PagedResult<GetUserDto>>> GetAllUsersByAdminAsync(PaginationParams paginationParamsrams)
        {
            var query = userRepository.GetAllUsers();
            
            var mappedQuery = query.ProjectTo<GetUserDto>(mapper.ConfigurationProvider);
            
            var pagedResult = await mappedQuery.ToPagedResultAsync(paginationParamsrams.PageNumber, paginationParamsrams.PageSize);

            return Result<PagedResult<GetUserDto>>.Success(pagedResult);
        }

        public async Task<Result<bool>> DeleteUserSoftAsync(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return Result<bool>.Failure(IncorrectIdMessage, HttpStatusCode.BadRequest);
            
            var user = await userRepository.GetUserById(id.Value);

            if (user == null)
                return Result<bool>.Failure(NotFoundUserMessage, HttpStatusCode.NotFound);
            
            user.IsDeleted = true;
            user.DeletedAt = DateTime.Now;
            
            await userRepository.UpdateUser(user);
            return Result<bool>.Success(true);
        }
    }
}
