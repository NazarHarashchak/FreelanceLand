using AutoMapper;
using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace Backend.Services
{
    public class TaskInfoService : ITaskInfoService
    {
        private readonly IMapper mapper;
        EFGenericRepository<FreelanceLand.Models.Task> taskRepo;
        EFGenericRepository<TaskHistory> historyRepo; 
        EFGenericRepository<User> userRepo; 
        EFGenericRepository<Comment> commentRepo; 

        public TaskInfoService(IMapper mapper, ApplicationContext context)
        {
            taskRepo = new EFGenericRepository<FreelanceLand.Models.Task>(context);
            historyRepo = new EFGenericRepository<TaskHistory>(context);
            userRepo = new EFGenericRepository<User>(context);
            this.mapper = mapper;
        }

        public async Task<TaskPageDTO> GetTaskDescription(int id)
        {
            FreelanceLand.Models.Task myTask = (await taskRepo.GetWithIncludeAsync(task => task.Id == id,
                                     customer => customer.Customer, 
                                     category => category.TaskCategory,
                                     status => status.TaskStatus,
                                     history => history.TaskHistories)).FirstOrDefault();

            var dtos = mapper.Map<FreelanceLand.Models.Task, TaskPageDTO>(myTask);

            return dtos;
        }

        public async Task<ExcecutorDTO> AddExcecutor(ExcecutorDTO user)
        {
            int taskId = user.TaskId;
            int userId = user.ExcecutorId;

            FreelanceLand.Models.Task task = await taskRepo.FindByIdAsync(taskId);
            task.ExecutorId = userId;
            task.TaskStatusId++;
            task.UpdatedById = task.CustomerId;
            task.DateUpdated = DateTime.Now;

            await taskRepo.UpdateAsync(task);
            return user;
        }

        public async Task<TaskPageDTO> AddTask(TaskPageDTO task)
        {
            task.Date = DateTime.Now.ToString();
            CustomerDTO user = mapper.Map<User, CustomerDTO> (await userRepo.FindByIdAsync(task.CustomerId));
            task.CustomerName = user.Name;
            task.CustomerSecondName = user.Sur_Name;

            FreelanceLand.Models.Task result = mapper.Map<TaskPageDTO, FreelanceLand.Models.Task>(task);
            await taskRepo.CreateAsync(result);
            return task;
        }
    }
}
