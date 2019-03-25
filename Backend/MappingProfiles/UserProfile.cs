using AutoMapper;
using Backend.DTOs;
using FreelanceLand.Models;

namespace Backend.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, User>();

            CreateMap<User, UserDTO>();

            CreateMap<UserDTO, User>();
        }
    }

    public class UserInformationProfile : Profile
    {
        public UserInformationProfile()
        {
            CreateMap<User, UserInformation>()
                .ForMember("UserRoleName",u=>u.MapFrom(o=>o.UserRole.Type));
            CreateMap<User, User>();
            CreateMap<UserInformation, User>();
            CreateMap<User, CustomerDTO>();
        }
    }

    public class UserAccountDTO : Profile
    {
        public UserAccountDTO()
        {
            CreateMap<User, UserAccountDTO>();
            CreateMap<UserAccountDTO, User>();
        }
    }
   
}
