using AutoMapper;
using Backend.DTOs;
using Backend.Enums;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Services
{
    public class TasksService : ITasksService
    {
        private readonly IMapper mapper;
        private readonly EFGenericRepository<Task> taskRepo;
        private readonly EFGenericRepository<TaskHistory> historyRepo;
        private readonly ApplicationContext db;

        public TasksService(IMapper mapper, ApplicationContext context)
        {
            db = context;
            this.mapper = mapper;
            taskRepo = new EFGenericRepository<Task>(context);
            historyRepo = new EFGenericRepository<TaskHistory>(context);
        }

        public IEnumerable<TaskDTO> GetHistoryTaskByUser(int id)
        {
            var entities = taskRepo.GetWithInclude(o => o.TaskStatusId == (int)StatusEnum.Done, p => p.TaskCategory, k => k.Comments).Where(o => o.ExecutorId == id);


            var dtos = mapper.Map<IEnumerable<Task>, IEnumerable<TaskDTO>>(entities);
            return dtos;
        }

        public IEnumerable<TaskDTO> GetToDoEntities()
        {
            var entities = taskRepo.GetWithInclude(o => o.TaskStatusId == (int)StatusEnum.ToDo, p => p.TaskCategory, k => k.Comments);
            var dtos = mapper.Map<IEnumerable<Task>, IEnumerable<TaskDTO>>(entities);
            return dtos;
        }

        public void DeleteTask(int id)
        {
            var task = taskRepo.FindById(id);
            taskRepo.Remove(task);
        }

        public IEnumerable<TaskDTO> GetActiveTaskByUser(int id)
        {
            var entities = taskRepo.GetWithInclude(o => o.TaskStatusId == (int)StatusEnum.InProgress, p => p.TaskCategory, k => k.Comments).Where(o => o.ExecutorId==id);


            var dtos = mapper.Map<IEnumerable<Task>, IEnumerable<TaskDTO>>(entities);
            return dtos;
        }
    }
}
