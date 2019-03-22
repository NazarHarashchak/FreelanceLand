using AutoMapper;
using Backend.DTOs;
using FreelanceLand.Models;

namespace Backend.MappingProfiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Task, Task>();

            CreateMap<Task, TaskDTO>()
                .ForMember("TaskCategoryName", o => o.MapFrom(c => c.TaskCategory.Type))
                .ForMember("DateAdded", o => o.MapFrom(c => c.DateCreate.ToString("d")))
                .ForMember("CommentsCount", o => o.MapFrom(c => c.Comments.Count));

            CreateMap<TaskDTO, Task>();
        }
    }
}
