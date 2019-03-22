using AutoMapper;
using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Backend.Services
{
    public class TaskInfoService : ITaskInfoService
    {
        private readonly IMapper mapper;
        private EFGenericRepository<Task> taskRepo;
        private EFGenericRepository<TaskHistory> historyRepo;
        private EFGenericRepository<User> userRepo;

        public TaskInfoService(IMapper mapper, ApplicationContext context)
        {
            taskRepo = new EFGenericRepository<Task>(context);
            historyRepo = new EFGenericRepository<TaskHistory>(context);
            userRepo = new EFGenericRepository<User>(context);
            this.mapper = mapper;
        }

        public TaskPageDTO GetTaskDescription(int id)
        {
            TaskHistory history = historyRepo.GetWithInclude(task => task.TaskId == id,
                                     customer => customer.TaskCustomer, 
                                     taskDesc => taskDesc.Task, 
                                     status => status.Task.TaskStatus).FirstOrDefault();

            var dtos = mapper.Map<TaskHistory, TaskPageDTO>(history);

            return dtos;
        }

        public CustomerDTO AddExcecutor(CustomerDTO user)
        {

            return user;
        }
        public TaskPageDTO AddTask(TaskPageDTO task)
        {
            task.Date = DateTime.Now.ToString();
            CustomerDTO user = mapper.Map<User, CustomerDTO> (userRepo.FindById(task.CustomerId));
            task.CustomerName = user.Name;
            task.CustomerSecondName = user.Sur_Name;

            Task result = mapper.Map<TaskPageDTO, Task>(task);
            taskRepo.Create(result);
            return task;
        }
    }
}
