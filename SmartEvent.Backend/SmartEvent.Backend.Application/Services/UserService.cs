using System.Net;
using AutoMapper;
using SmartEvent.Backend.Application.DTOs.UserDTOs.Requests;
using SmartEvent.Backend.Application.DTOs.UserDTOs.Responses;
using SmartEvent.Backend.Application.Interfaces.IServices;
using SmartEvent.Backend.Core.Common;
using SmartEvent.Backend.Core.Interfaces.IRepositories;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Application.Services
{
    public class UserService(IUserRepository userRepository, IMapper mapper,
        IPasswordHasher passwordHasher, IJwtService jwtService) : IUserService
    {
        public async Task<Result<AuthorizeUserResponseDto>> RegisterUserAsync(RegisterUserRequestDto? request)
        {
            const string userExistMessage = "User with this  email already exists";
            ArgumentNullException.ThrowIfNull(request);

            var existingUser = await userRepository.GetUserByEmail(request.Email);
            
            if (existingUser != null)
            {
                return  Result<AuthorizeUserResponseDto>.Failure(userExistMessage,  HttpStatusCode.Conflict);
            }

            var user = mapper.Map<User>(request);
            user.PasswordHash = passwordHasher.Hash(request.Password);
            user.UserRole = Core.Enums.UserRole.Student;

            await userRepository.AddUser(user);

            var token = jwtService.GenerateJwtToken(user);

            var responseDto = new AuthorizeUserResponseDto
            {
                JwtToken = token
            };
            
            var response = Result<AuthorizeUserResponseDto>.Success(responseDto);
            
            return response;
        }
        
        public async Task<Result<AuthorizeUserResponseDto>> AuthorizeUserAsync(LoginUserRequestDto? request)
        {
            const string notFoundUserMessage = "User not found";
            const string invalidPasswordMessage = "Check your email or  password";
            
            ArgumentNullException.ThrowIfNull(request);

            var user = await userRepository.GetUserByEmail(request.Email);
            
            if(user == null) return Result<AuthorizeUserResponseDto>
                .Failure(notFoundUserMessage, HttpStatusCode.NotFound);

            if (!passwordHasher.Verify(user.PasswordHash, request.Password))
            {
                return Result<AuthorizeUserResponseDto>
                    .Failure(invalidPasswordMessage, HttpStatusCode.Unauthorized);
            }
            
            var token = jwtService.GenerateJwtToken(user);

            var responseDto = new AuthorizeUserResponseDto()
            {
                JwtToken = token
            };
            
            var response = Result<AuthorizeUserResponseDto>.Success(responseDto);
            
            return response;
        }

        public Task DeleteUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserDto?> GetUserByEmailAsync(string? email)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto?> GetUserByIdAsync(Guid? id)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateUserResponseDto> UpdateUserAsync(UpdateUserRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserExistsByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserExistsByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        
        public Task<AssignRoleResponseDto> AssignRoleAsync(AssignRoleRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
}
