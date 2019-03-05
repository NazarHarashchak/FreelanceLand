using System;
using AutoMapper;
using Backend.DTOs;
using FreelanceLand.Models;

namespace Backend.MappingProfiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, Comment>();
            CreateMap<Comment, CommentDTO>()
                .ForMember("Date", o => o.MapFrom(c => c.DateAndTime.ToString()));
        }
    }
}
