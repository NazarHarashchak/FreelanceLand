using AutoMapper;
using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Backend.Enums;

namespace Backend.Services
{
    public class TaskInfoService : ITaskInfoService
    {
        private readonly IMapper mapper;
        private EFGenericRepository<FreelanceLand.Models.Task> taskRepo;
        private EFGenericRepository<TaskHistory> historyRepo;
        private EFGenericRepository<User> userRepo;
        private EFGenericRepository<TaskCategory> categoryRepo;

        public TaskInfoService(IMapper mapper, ApplicationContext context)
        {
            taskRepo = new EFGenericRepository<FreelanceLand.Models.Task>(context);
            historyRepo = new EFGenericRepository<TaskHistory>(context);
            userRepo = new EFGenericRepository<User>(context);
            categoryRepo = new EFGenericRepository<TaskCategory>(context);
            this.mapper = mapper;
        }

        public async Task<TaskPageDTO> GetTaskDescription(int id)
        {
            FreelanceLand.Models.Task myTask = (await taskRepo.GetWithIncludeAsync(task => task.Id == id,
                                     customer => customer.Customer, 
                                     category => category.TaskCategory,
                                     status => status.TaskStatus,
                                     history => history.TaskHistories,
                                     excecutor => excecutor.Executor)).Where(o => o.Id==id).FirstOrDefault();

            var dtos = mapper.Map<FreelanceLand.Models.Task, TaskPageDTO>(myTask);

            return dtos;
        }

        public async Task<ExcecutorDTO> AddExcecutor(ExcecutorDTO user)
        {
            int taskId = user.TaskId;
            int userId = user.ExcecutorId;

            FreelanceLand.Models.Task task = await taskRepo.FindByIdAsync(taskId);
            task.ExecutorId = userId;
            task.UpdatedById = task.CustomerId;
            task.DateUpdated = DateTime.Now;
            StatusEnum status = StatusEnum.InProgress;
            task.TaskStatusId = (int)status;

            await taskRepo.UpdateAsync(task);
            return user;
        }


        public async Task<TaskPageDTO> CloseTask(int taskId)
        {
            FreelanceLand.Models.Task task = await taskRepo.FindByIdAsync(taskId);

            task.UpdatedById = task.CustomerId;
            task.DateUpdated = DateTime.Now;
            StatusEnum status = StatusEnum.Done;
            task.TaskStatusId = (int)status;

            await taskRepo.UpdateAsync(task);
      
            return (mapper.Map<FreelanceLand.Models.Task,TaskPageDTO>(task));
        }

        public async Task<TaskPageDTO> AddTask(TaskPageDTO task)
        {
            task.Date = DateTime.Now.ToString();
            StatusEnum status = StatusEnum.ToDo;

            var result = mapper.Map<TaskPageDTO, FreelanceLand.Models.Task>(task);

            result.TaskCategoryId = (await categoryRepo.GetWithIncludeAsync(c => c.Type == task.TaskCategory))
                .FirstOrDefault().Id;
            result.TaskStatusId = (int)status;
            result.UpdatedById = task.CustomerId;
            result.DateUpdated = DateTime.Now;
            await taskRepo.CreateAsync(result);
            return task;
        }

        public async Task<List<TaskCategoryDTO>> GetCategories()
        {
            List<TaskCategoryDTO> result = mapper.Map<IEnumerable<TaskCategory>, IEnumerable<TaskCategoryDTO>>
                                        (await categoryRepo.GetAsync()).ToList();
            return result;
        }
    }
}
