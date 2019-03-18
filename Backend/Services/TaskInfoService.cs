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
        EFGenericRepository<Task> taskRepo = new EFGenericRepository<Task>(new ApplicationContext());
        EFGenericRepository<TaskHistory> historyRepo = new EFGenericRepository<TaskHistory>(new ApplicationContext());

        public TaskInfoService(IMapper mapper)
        {
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
