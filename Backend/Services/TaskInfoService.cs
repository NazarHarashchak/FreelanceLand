using AutoMapper;
using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using System.Collections.Generic;

namespace Backend.Services
{
    public class TaskInfoService : ITaskInfoService
    {
        private readonly IMapper mapper;
        EFGenericRepository<Task> taskRepo;
        EFGenericRepository<TaskHistory> historyRepo; 
        EFGenericRepository<User> userRepo; 
        EFGenericRepository<Comment> commentRepo; 


        public TaskInfoService(IMapper mapper, ApplicationContext context)
        {
            this.mapper = mapper;
            taskRepo = new EFGenericRepository<Task>(context);
            historyRepo = new EFGenericRepository<TaskHistory>(context);
            userRepo = new EFGenericRepository<User>(context);
            commentRepo = new EFGenericRepository<Comment>(context);
        }

        public TaskDescription GetTaskDescription(int id)
        {
            var entities = taskRepo.FindById(id);
            var dtos = mapper.Map<Task, TaskDescription>(entities);
            return dtos;
        }

        public CustomerDTO GetTaskCustomer(int taskId)
        {
            int userId = 0;
            IEnumerable<TaskHistory> history = historyRepo.Get();
            foreach (TaskHistory s in history)
            {
                if (s.TaskId == taskId) userId = (int)s.TaskCustomerId;
            }
            var dtos = mapper.Map<User, CustomerDTO>(userRepo.FindById(userId));
            return dtos;
        }
    }
}
