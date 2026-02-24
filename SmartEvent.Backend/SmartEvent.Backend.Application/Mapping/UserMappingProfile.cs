using AutoMapper;
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

            // User -> AuthorizeUserResponseDto
            CreateMap<User, AuthorizeUserResponseDto>()
                .ForMember(dest => dest.Token, opt => opt.Ignore());
        }
    }
}
