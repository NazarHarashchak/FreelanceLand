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
                    .ForMember("TaskCategory", o => o.MapFrom(c => c.TaskCategory.Type.ToString()))
                    .ForMember("CustomerId", o => o.MapFrom(c => c.CustomerId))
                    .ForMember("CustomerName", o => o.MapFrom(c => c.Customer.Name))
                    .ForMember("CustomerSecondName", o => o.MapFrom(c => c.Customer.Sur_Name))
                    .ForMember("TaskStatus", o => o.MapFrom(c => c.TaskStatus.Type))
                    .ForMember("ExcecutorId", o => o.MapFrom(c => c.ExecutorId))
                    .ForMember("ExcecutorName", o => o.MapFrom(c => c.Executor.Name))
                    .ForMember("ExcecutorSecondName", o => o.MapFrom(c => c.Executor.Sur_Name));

            CreateMap<TaskPageDTO, Task>()
                    .ForMember("CustomerId", o => o.MapFrom(c => c.CustomerId))
                    .ForMember("Title", o => o.MapFrom(c => c.Title))
                    .ForMember("Description", o => o.MapFrom(c => c.Description))
                    .ForMember("TaskStatus", o=> o.Ignore())
                    .ForMember("TaskCategory", o => o.Ignore());

            CreateMap<TaskCategory, TaskCategoryDTO>();
            CreateMap<TaskPageDTO, TaskHistory>();
        }
    }
}
