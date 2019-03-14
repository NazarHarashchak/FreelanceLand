using AutoMapper;
using Backend.DTOs;
using Backend.Enums;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Services
{
    public class TasksService : ITasksService
    {
        private readonly IMapper mapper;
        EFGenericRepository<Task> taskRepo = new EFGenericRepository<Task>(new ApplicationContext());
        EFGenericRepository<TaskHistory> historyRepo = new EFGenericRepository<TaskHistory>(new ApplicationContext());

        public TasksService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public IEnumerable<TaskDTO> GetHistoryTaskByUser(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var taskHist = from h in db.TaskHistories
                    where h.TaskExecutorId==(int)id
                    select h.TaskId;


                var entities = from t in db.Tasks
                            where taskHist.Contains(t.Id)
                            select t;

                var dtos = mapper.Map<IEnumerable<Task>, IEnumerable<TaskDTO>>(entities);
                return dtos;


            }
        }

        public IEnumerable<TaskDTO> GetToDoEntities()
        {
            var entities = taskRepo.GetWithInclude(o => o.TaskStatusId == (int)StatusEnum.ToDo, p => p.TaskCategory, k=> k.Comments);
            var dtos = mapper.Map<IEnumerable<Task>, IEnumerable<TaskDTO>>(entities);
            return dtos;
        }
    }
}
