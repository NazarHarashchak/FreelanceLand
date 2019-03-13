using AutoMapper;
using Backend.DTOs;
using FreelanceLand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.MappingProfiles
{
    public class TopFiveProfile : Profile
    {
        public TopFiveProfile()
        {
            CreateMap<User, User>();

            CreateMap<User, TopUserDTO>();
               
            CreateMap<TopUserDTO, User>();
        }
    }
}
