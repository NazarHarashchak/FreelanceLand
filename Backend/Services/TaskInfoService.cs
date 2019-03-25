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
        private EFGenericRepository<TaskCategory> categoryRepo;

        public TaskInfoService(IMapper mapper, ApplicationContext context)
        {
            taskRepo = new EFGenericRepository<Task>(context);
            historyRepo = new EFGenericRepository<TaskHistory>(context);
            userRepo = new EFGenericRepository<User>(context);
            categoryRepo = new EFGenericRepository<TaskCategory>(context);
            this.mapper = mapper;
        }

        public TaskPageDTO GetTaskDescription(int id)
        {
            Task myTask = taskRepo.GetWithInclude(task => task.Id == id,
                                     customer => customer.Customer, 
                                     category => category.TaskCategory,
                                     status => status.TaskStatus,
                                     history => history.TaskHistories).FirstOrDefault();

            var dtos = mapper.Map<Task, TaskPageDTO>(myTask);

            return dtos;
        }

        public ExcecutorDTO AddExcecutor(ExcecutorDTO user)
        {
            int taskId = user.TaskId;
            int userId = user.ExcecutorId;

            Task task = taskRepo.FindById(taskId);
            task.ExecutorId = userId;
            task.TaskStatusId++;
            task.UpdatedById = task.CustomerId;
            task.DateUpdated = DateTime.Now;

            taskRepo.Update(task);
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

        public List<TaskCategoryDTO> GetCategories()
        {
            List<TaskCategoryDTO> result = mapper.Map<IEnumerable<TaskCategory>, IEnumerable<TaskCategoryDTO>>
                                        (categoryRepo.Get()).ToList();
            return result;
        }
    }
}
