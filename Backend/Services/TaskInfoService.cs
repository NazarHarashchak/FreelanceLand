using AutoMapper;
using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Services
{
    public class TaskInfoService : ITaskInfoService
    {
        private readonly IMapper mapper;
        private EFGenericRepository<Task> taskRepo;
        private EFGenericRepository<TaskHistory> historyRepo;

        public TaskInfoService(IMapper mapper, ApplicationContext context)
        {
            taskRepo = new EFGenericRepository<Task>(context);
            historyRepo = new EFGenericRepository<TaskHistory>(context);
            this.mapper = mapper;
        }

        public TaskPageDTO GetTaskDescription(int id)
        {
            List<TaskHistory> history = historyRepo.GetWithInclude(task => task.TaskId == id,
                                     customer => customer.TaskCustomer, 
                                     taskDesc => taskDesc.Task, 
                                     status => status.Task.TaskStatus).ToList();

            var dtos = mapper.Map<TaskHistory, TaskPageDTO>(history[0]);

            return dtos;
        }
    }
}
