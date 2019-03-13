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
            CreateMap<User, Customer>();
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
