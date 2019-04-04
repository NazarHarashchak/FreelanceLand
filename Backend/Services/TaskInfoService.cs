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
        private EFGenericRepository<FreelanceLand.Models.TaskStatus> statusRepo;

        public TaskInfoService(IMapper mapper, ApplicationContext context)
        {
            taskRepo = new EFGenericRepository<FreelanceLand.Models.Task>(context);
            historyRepo = new EFGenericRepository<TaskHistory>(context);
            userRepo = new EFGenericRepository<User>(context);
            categoryRepo = new EFGenericRepository<TaskCategory>(context);
            statusRepo = new EFGenericRepository<FreelanceLand.Models.TaskStatus>(context);
            this.mapper = mapper;
        }

        public async Task<TaskPageDTO> GetTaskDescription(int id)
        {
            var myTask = (await taskRepo.GetWithIncludeAsync(task => task.Id == id,
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
            var task = await taskRepo.FindByIdAsync(user.TaskId);
            task.ExecutorId = user.ExcecutorId;
            task.UpdatedById = task.CustomerId;
            task.DateUpdated = DateTime.Now;
            var status = (await statusRepo.GetWithIncludeAsync(s => s.Type == "In progress")).FirstOrDefault();
            task.TaskStatusId = status.Id;

            await taskRepo.UpdateAsync(task);
            return user;
        }


        public async Task<TaskPageDTO> CloseTask(int taskId)
        {
            var task = await taskRepo.FindByIdAsync(taskId);

            task.UpdatedById = task.CustomerId;
            task.DateUpdated = DateTime.Now;
            var status = (await statusRepo.GetWithIncludeAsync(s => s.Type == "Done")).FirstOrDefault();
            task.TaskStatusId = status.Id;

            await taskRepo.UpdateAsync(task);
      
            return (mapper.Map<FreelanceLand.Models.Task,TaskPageDTO>(task));
        }

        public async Task<TaskPageDTO> AddTask(TaskPageDTO task)
        {
            var result = mapper.Map<TaskPageDTO, FreelanceLand.Models.Task>(task);

            result.TaskCategoryId = (await categoryRepo.GetWithIncludeAsync(c => c.Type == task.TaskCategory))
                .FirstOrDefault().Id;
            var status = (await statusRepo.GetWithIncludeAsync(s => s.Type == "To do")).FirstOrDefault();
            result.TaskStatusId = status.Id;
            result.UpdatedById = task.CustomerId;
            result.DateUpdated = DateTime.Now;
            await taskRepo.CreateAsync(result);
            return task;
        }

        public async Task<IEnumerable<TaskCategoryDTO>> GetCategories()
        {
            IEnumerable<TaskCategoryDTO> result = mapper.Map<IEnumerable<TaskCategory>, IEnumerable<TaskCategoryDTO>>
                                        (await categoryRepo.GetAsync());
            return result;
        }
    }
}
