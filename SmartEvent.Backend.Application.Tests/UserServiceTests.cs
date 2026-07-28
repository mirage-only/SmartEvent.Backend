using AutoMapper;
using FluentAssertions;
using NSubstitute;
using SmartEvent.Backend.Application.DTOs.UserDTOs.Requests;
using SmartEvent.Backend.Application.Interfaces.IServices;
using SmartEvent.Backend.Application.Services;
using SmartEvent.Backend.Core.Enums;
using SmartEvent.Backend.Core.Interfaces.IRepositories;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Application.Tests;

public class UserServiceTests
{
    private readonly IUserRepository _userRepositoryMock;
    private readonly IPasswordHasher _passwordHasherMock;
    private readonly IJwtService _jwtServiceMock;
    private readonly IMapper _mapperMock;

    private readonly UserService _sut;

    public UserServiceTests()
    {
        _userRepositoryMock = Substitute.For<IUserRepository>();
        _passwordHasherMock = Substitute.For<IPasswordHasher>();
        _jwtServiceMock = Substitute.For<IJwtService>();
        _mapperMock = Substitute.For<IMapper>();

        _sut = new UserService(_userRepositoryMock, _mapperMock, _passwordHasherMock, _jwtServiceMock);
    }

    public class Authorize : UserServiceTests
    {
        [Fact]
        public async Task Authorize_ShouldReturnSuccess_WhenDtoValid()
        {
            var request = new LoginUserRequestDto()
            {
                Email = "test@gmail.com",
                Password = "password"
            };
            
            string fakeJwtToken = "fakeJwtToken";
            var fakeUser = new User()
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                UserRole = UserRole.Employee,
                PasswordHash = "fakePasswordHash"
            };
            
            _userRepositoryMock.GetUserByEmail(request.Email).Returns(fakeUser);
            _passwordHasherMock.Verify(fakeUser.PasswordHash, request.Password).Returns(true);
            _jwtServiceMock.GenerateJwtToken(fakeUser).Returns(fakeJwtToken);

            var result = await _sut.AuthorizeUserAsync(request);
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.JwtToken.Should().Be(fakeJwtToken);
        }
    }
}
