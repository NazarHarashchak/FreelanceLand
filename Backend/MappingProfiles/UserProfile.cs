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

    public class UserInformation : Profile
    {
        public UserInformation()
        {
            CreateMap<User, UserInformation>();
            CreateMap<User, User>();
            CreateMap<UserInformation, User>();
        }
    }
    public class UserInformationUpdate : Profile
    {
        public UserInformationUpdate()
        {
            CreateMap<User, UserInformationUpdate>();
            CreateMap<User, User>();
            CreateMap<UserInformationUpdate, User>();
        }
    }
}
