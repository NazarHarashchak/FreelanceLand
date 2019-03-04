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
                .ForMember("DateAdded", o => o.MapFrom(c => c.Date.ToString("d")))
                .ForMember("Deadline", o => o.MapFrom(c => c.Deadline.ToString("d")));
        }
    }
}
