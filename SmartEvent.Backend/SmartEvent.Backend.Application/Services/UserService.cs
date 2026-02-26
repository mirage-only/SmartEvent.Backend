using AutoMapper;
using SmartEvent.Backend.Application.DTOs.UserDTOs.Requests;
using SmartEvent.Backend.Application.DTOs.UserDTOs.Responses;
using SmartEvent.Backend.Application.Interfaces.IServices;
using SmartEvent.Backend.Core.Interfaces.IRepositories;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Application.Services
{
    public class UserService(IUserRepository userRepository, IMapper mapper,
        IPasswordHasher passwordHasher, IJwtService jwtService) : IUserService
    {
        public async Task<AuthorizeUserResponseDto> RegisterUserAsync(RegisterUserRequestDto? request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var existing = await userRepository.GetUserByEmail(request.Email);
            
            if (existing != null)
            {
                throw new NullReferenceException("User with this email already exists");
            }

            var user = mapper.Map<User>(request);
            user.PasswordHash = passwordHasher.Hash(request.Password);
            user.UserRole = Core.Enums.UserRole.Student;

            await userRepository.AddUser(user);

            var token = jwtService.GenerateJwtToken(user);

            var response = new AuthorizeUserResponseDto
            {
                JwtToken = token
            };
            
            return response;
        }
        
        public async Task<AuthorizeUserResponseDto> AuthorizeUserAsync(LoginUserRequestDto? request)
        {
            const string invalidInputExсeption = "Invalid email or password";
            ArgumentNullException.ThrowIfNull(request);

            var user = await userRepository.GetUserByEmail(request.Email);

            if(user == null || !passwordHasher.Verify(user.PasswordHash, request.Password)) 
                throw new Exception(invalidInputExсeption);

            var token = jwtService.GenerateJwtToken(user);

            var responseDto = new AuthorizeUserResponseDto()
            {
                JwtToken = token
            };
                
            return responseDto;

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
