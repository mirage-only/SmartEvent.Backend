using AutoMapper;
using SmartEvent.Backend.Application.DTOs.UserDTOs.Requests;
using SmartEvent.Backend.Application.DTOs.UserDTOs.Responses;
using SmartEvent.Backend.Application.Interfaces.IServices;
using SmartEvent.Backend.Core.Interfaces.IRepositories;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Application.Services
{
    public class UserService(IUserRepository userRepository, IMapper mapper, IPasswordHasher passwordHasher) : IUserService
    {
        public Task<AssignRoleResponseDto> AssignRoleAsync(AssignRoleRequestDto request)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthorizeUserResponseDto> AuthorizeUserAsync(LoginUserRequestDto? request)
        {
            string invalidIpnutExeption = "Invalid email or password";
            if (request == null) throw new ArgumentNullException(nameof(request));

            var user = await userRepository.GetUserByEmail(request.Email);

            if(user == null) 
                throw new Exception(invalidIpnutExeption);
            if(!passwordHasher.Verify(user.PasswordHash, request.Password))
                throw new Exception(invalidIpnutExeption);

            var response = mapper.Map<AuthorizeUserResponseDto>(user);
            response.Token = "jwt will be here";
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
        
        public async Task<AuthorizeUserResponseDto> RegisterUserAsync(RegisterUserRequestDto? request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var existing = await userRepository.GetUserByEmail(request.Email);
            if (existing != null)
            {
                throw new Exception("User with this email already exists");
            }

            var user = mapper.Map<User>(request);
            user.PasswordHash = passwordHasher.Hash(request.Password);
            user.UserRole = Core.Enums.UserRole.Student;
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            await userRepository.AddUser(user);

            var response = mapper.Map<AuthorizeUserResponseDto>(user);
            response.Token = "jwt will be here";
            return response;
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
    }
}
