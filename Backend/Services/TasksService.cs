using AutoMapper;
using Backend.DTOs;
using Backend.Enums;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class TasksService : ITasksService
    {
        private readonly IMapper mapper;
        private readonly EFGenericRepository<FreelanceLand.Models.Task> taskRepo;
        private readonly EFGenericRepository<TaskHistory> historyRepo;
        private readonly ApplicationContext db;

        public TasksService(IMapper mapper, ApplicationContext context)
        {
            db = context;
            this.mapper = mapper;
            taskRepo = new EFGenericRepository<FreelanceLand.Models.Task>(context);
            historyRepo = new EFGenericRepository<TaskHistory>(context);
        }

        public IEnumerable<TaskDTO> GetHistoryTaskByUser(int id)
        {
            var taskHist = from h in db.TaskHistories
                           where h.TaskExecutorId == (int)id
                           select h.TaskId;


            var entities = from t in db.Tasks
                           where taskHist.Contains(t.Id)
                           select t;

            var dtos = mapper.Map<IEnumerable<FreelanceLand.Models.Task>, IEnumerable<TaskDTO>>(entities);
            return dtos;
        }

        public async Task<IEnumerable<TaskDTO>> GetToDoEntities()
        {
            var entities = await taskRepo.GetWithIncludeAsync(o => o.TaskStatusId == (int)StatusEnum.ToDo, p => p.TaskCategory, k => k.Comments);
            var dtos = mapper.Map<IEnumerable<FreelanceLand.Models.Task>, IEnumerable<TaskDTO>>(entities);
            return dtos;
        }

        public async System.Threading.Tasks.Task DeleteTask(int id)
        {
            var task = await taskRepo.FindByIdAsync(id);
            await taskRepo.RemoveAsync(task);
        }
    }
}
