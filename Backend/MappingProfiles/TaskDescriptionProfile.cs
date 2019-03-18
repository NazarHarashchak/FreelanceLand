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
            CreateMap<TaskHistory, TaskPageDTO>()
                    .ForMember("Date", o => o.MapFrom(c => c.Task.Date.ToString("d")))
                    .ForMember("Deadline", o => o.MapFrom(c => c.Task.Deadline.ToString("d")))
                    .ForMember("Title", o => o.MapFrom(c => c.Task.Title))
                    .ForMember("Price", o => o.MapFrom(c => c.Task.Price))
                    .ForMember("Description", o => o.MapFrom(c => c.Task.Description))
                    .ForMember("TaskCategory", o => o.MapFrom(c => c.Task.TaskCategory.Type))
                    .ForMember("CustomerId", o => o.MapFrom(c => c.TaskCustomerId))
                    .ForMember("CustomerName", o => o.MapFrom(c => c.TaskCustomer.Name))
                    .ForMember("CustomerSecondName", o => o.MapFrom(c => c.TaskCustomer.Sur_Name));
        }
    }
}
