using AutoMapper;
using SmartEvent.Backend.Application.DTOs.UserDTOs.Requests;
using SmartEvent.Backend.Application.DTOs.UserDTOs.Responses;
using SmartEvent.Backend.Application.Interfaces.IServices;
using SmartEvent.Backend.Core.Interfaces.IRepositories;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public Task<AssignRoleResponseDto> AssignRoleAsync(AssignRoleRequestDto request)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthorizeUserResponseDto> AuthorizeUserAsync(LoginUserRequestDto? request)
        {
            string invalidIpnutExeption = "Invalid email or password";
            if (request == null) throw new ArgumentNullException(nameof(request));

            var user = await _userRepository.GetUserByEmail(request.Email);

            if(user == null) 
                throw new Exception(invalidIpnutExeption);
            if(!_passwordHasher.Verify(user.PasswordHash, request.Password))
                throw new Exception(invalidIpnutExeption);

            var response = _mapper.Map<AuthorizeUserResponseDto>(user);
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

            var existing = await _userRepository.GetUserByEmail(request.Email);
            if (existing != null)
            {
                throw new Exception("User with this email already exists");
            }

            var user = _mapper.Map<User>(request);
            user.PasswordHash = _passwordHasher.Hash(request.Password);
            user.UserRole = Core.Enums.UserRole.Student;
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.AddUser(user);

            var response = _mapper.Map<AuthorizeUserResponseDto>(user);
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
