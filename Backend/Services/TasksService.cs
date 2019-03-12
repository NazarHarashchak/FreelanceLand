using AutoMapper;
using Backend.DTOs;
using Backend.Enums;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using System.Collections.Generic;

namespace Backend.Services
{
    public class TasksService : ITasksService
    {
        private readonly IMapper mapper;
        EFGenericRepository<Task> taskRepo = new EFGenericRepository<Task>(new ApplicationContext());

        public TasksService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public IEnumerable<TaskDTO> GetToDoEntities()
        {
            var entities = taskRepo.GetWithInclude(o => o.TaskStatusId == (int)StatusEnum.ToDo, 
                p => p.TaskCategory, k=> k.Comments);
            var dtos = mapper.Map<IEnumerable<Task>, IEnumerable<TaskDTO>>(entities);
            return dtos;
        }
    }
}
