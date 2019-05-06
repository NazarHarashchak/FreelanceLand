using AutoMapper;
using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Backend.Models;

namespace Backend.Services
{
    public class TaskInfoService : ITaskInfoService
    {
        private readonly IMapper mapper;
        private EFGenericRepository<FreelanceLand.Models.Task> taskRepo;
        private EFGenericRepository<TaskHistory> historyRepo;
        private EFGenericRepository<Ratings> ratingRepo;
        private EFGenericRepository<User> userRepo;
        private EFGenericRepository<TaskCategory> categoryRepo;
        private EFGenericRepository<FreelanceLand.Models.TaskStatus> statusRepo;
        private IImageService imageService; 

        public TaskInfoService(IMapper mapper, ApplicationContext context)
        {
            taskRepo = new EFGenericRepository<FreelanceLand.Models.Task>(context);
            historyRepo = new EFGenericRepository<TaskHistory>(context);
            ratingRepo = new EFGenericRepository<Ratings>(context);
            categoryRepo = new EFGenericRepository<TaskCategory>(context);
            userRepo = new EFGenericRepository<User>(context);
            statusRepo = new EFGenericRepository<FreelanceLand.Models.TaskStatus>(context);
            imageService = new ImageService(mapper, context);
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

            dtos.CustomerPhoto = await imageService.GetImageAsync(dtos.CustomerId);
            dtos.ExcecutorPhoto = await imageService.GetImageAsync(dtos.ExcecutorId);

            return dtos;
        }

        public async Task<ExcecutorDTO> AddExcecutor(ExcecutorDTO user)
        {
            var task = await taskRepo.FindByIdAsync(user.TaskId);
            TaskHistory history = new TaskHistory();

            history.DateUpdated = DateTime.Now;
            history.UpdatedByUser = task.Customer;
            history.StartTaskStatus = await statusRepo.FindByIdAsync((int)task.TaskStatusId);

            task.ExecutorId = user.ExcecutorId;
            task.UpdatedById = task.CustomerId;
            task.DateUpdated = DateTime.Now;

            var status = (await statusRepo.GetWithIncludeAsync(s => s.Type == "In progress")).FirstOrDefault();
            task.TaskStatusId = status.Id;

            history.FinalTaskStatus = await statusRepo.FindByIdAsync(status.Id);

            await taskRepo.UpdateAsync(task);
            await historyRepo.CreateAsync(history);

            return user;
        }

        public async Task<TaskPageDTO> EditTask(TaskPageDTO task)
        {
            FreelanceLand.Models.Task myTask = await taskRepo.FindByIdAsync(task.Id);
            myTask.Title = task.Title;
            myTask.Description = task.Description;
            myTask.Price = task.Price;
            myTask.TaskCategoryId = (await categoryRepo.GetWithIncludeAsync(c => c.Type == task.TaskCategory))
                .FirstOrDefault().Id;
            myTask.DateUpdated = DateTime.Now;

            await taskRepo.UpdateAsync(myTask);
            return mapper.Map<FreelanceLand.Models.Task, TaskPageDTO>(myTask);
        }

        public async Task<TaskPageDTO> FinishTask(int taskId)
        {
            var task = await taskRepo.FindByIdAsync(taskId);
            TaskHistory history = new TaskHistory();

            task.UpdatedById = task.ExecutorId;

            task.DateUpdated = DateTime.Now;
            history.DateUpdated = DateTime.Now;

            history.UpdatedByUser = task.Executor;
            history.StartTaskStatus = await statusRepo.FindByIdAsync((int)task.TaskStatusId);

            var status = (await statusRepo.GetWithIncludeAsync(s => s.Type == "Ready for verification"))
                                                            .FirstOrDefault();
            task.TaskStatusId = status.Id;
            history.FinalTaskStatus = status;

            await taskRepo.UpdateAsync(task);
            await historyRepo.CreateAsync(history);

            return (mapper.Map<FreelanceLand.Models.Task, TaskPageDTO>(task));
        }

        public async Task<TaskPageDTO> CloseTask(int taskId)
        {
            var task = await taskRepo.FindByIdAsync(taskId);
         
            //TaskHistory history = new TaskHistory();

            task.UpdatedById = task.CustomerId;

            task.DateUpdated = DateTime.Now;
            //history.DateUpdated = DateTime.Now;

           // history.UpdatedByUser = task.Customer;
           // history.StartTaskStatus = await statusRepo.FindByIdAsync((int)task.TaskStatusId);

            var status = (await statusRepo.GetWithIncludeAsync(s => s.Type == "Done")).FirstOrDefault();
            task.TaskStatusId = status.Id;
          //  history.FinalTaskStatus = await statusRepo.FindByIdAsync(status.Id);

            await taskRepo.UpdateAsync(task);
           // await historyRepo.CreateAsync(history);
      
            return (mapper.Map<FreelanceLand.Models.Task,TaskPageDTO>(task));
        }

        public async Task<TaskPageDTO> AddTask(TaskPageDTO task)
        {
            var result = mapper.Map<TaskPageDTO, FreelanceLand.Models.Task>(task);

            result.TaskCategoryId = (await categoryRepo.GetWithIncludeAsync(c => c.Type == task.TaskCategory))
                .FirstOrDefault().Id;
            result.DateCreate = DateTime.Now;
            result.DateUpdated = DateTime.Now;
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
        public async Task<Ratings> RateUser(int UserId, int RateByUser, int Mark, int UserStatusId)
        {
            var rating = new Ratings();
            rating.UserId = UserId;
            rating.RateByUser = RateByUser;
            rating.Mark = Mark;
            rating.UserStatusId = UserStatusId;
            await ratingRepo.CreateAsync(rating);
            var countUsers = (await ratingRepo.GetWithIncludeAsync(x => x.UserId == UserId)).Count();
            var rat = (await ratingRepo.GetWithIncludeAsync(x => x.UserId == UserId)).Sum(y=>y.Mark)/countUsers;
            User user = await userRepo.FindByIdAsync(UserId);
            user.Rating = (int)rat;
            await userRepo.UpdateAsync(user);

            return rating;

        }

    }
}
