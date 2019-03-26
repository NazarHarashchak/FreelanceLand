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
            CreateMap<Task, TaskPageDTO>()
                    .ForMember("Date", o => o.MapFrom(c => c.DateCreate.ToString("d")))
                    .ForMember("Deadline", o => o.MapFrom(c => c.DateUpdated.ToString("d")))
                    .ForMember("Title", o => o.MapFrom(c => c.Title))
                    .ForMember("Price", o => o.MapFrom(c => c.Price))
                    .ForMember("Description", o => o.MapFrom(c => c.Description))
                    .ForMember("TaskCategory", o => o.MapFrom(c => c.TaskCategory.Type.ToString()))
                    .ForMember("CustomerId", o => o.MapFrom(c => c.CustomerId))
                    .ForMember("CustomerName", o => o.MapFrom(c => c.Customer.Name))
                    .ForMember("CustomerSecondName", o => o.MapFrom(c => c.Customer.Sur_Name))
                    .ForMember("TaskStatus", o => o.MapFrom(c => c.TaskStatus.Type));
            CreateMap<TaskPageDTO, Task>();
            CreateMap<TaskCategory, TaskCategoryDTO>();
            CreateMap<TaskPageDTO, TaskHistory>();
        }
    }
}
