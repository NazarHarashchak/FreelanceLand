using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.DTOs;

namespace Backend.MappingProfiles
{
    public class UserRole: Profile
    {
        public UserRole()
        {
            CreateMap<UserRole, UserRolesDTO>();
            CreateMap<UserRolesDTO, UserRole>();
            CreateMap<UserRole, UserRole>();
        }
    }
}
