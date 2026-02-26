using AutoMapper;
using SmartEvent.Backend.Application.DTOs.UserDTOs.Requests;
using SmartEvent.Backend.Application.DTOs.UserDTOs.Responses;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Application.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            // User -> UserDto
            CreateMap<User, UserDto>();
            
            CreateMap<RegisterUserRequestDto, User>()
                .ForMember(user => user.PasswordHash, passwordHash 
                    => passwordHash.Ignore());
        }
    }
}
