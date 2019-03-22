using System;
using AutoMapper;
using Backend.DTOs;
using FreelanceLand.Models;

namespace Backend.MappingProfiles
{
    public class TaskDescriptionProfile : Profile
    {
        public TaskDescriptionProfile()
        {
            CreateMap<Task, Task>();
            CreateMap<Task, TaskDescription>()
                .ForMember("DateAdded", o => o.MapFrom(c => c.DateCreate.ToString("d")))
                .ForMember("Deadline", o => o.MapFrom(c => c.DateUpdated.ToString("d")))
                .ForMember("TaskStatus", o => o.MapFrom(c => Convert.ToInt32(c.TaskStatusId)));
        }
    }
}
