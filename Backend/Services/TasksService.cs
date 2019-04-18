using AutoMapper;
using Backend.DTOs;
using Backend.Enums;
using Backend.Interfaces.ServiceInterfaces;
using Backend.Pagination;
using FreelanceLand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class TasksService : ITasksService
    {
        private readonly IMapper mapper;
        private readonly EFGenericRepository<Comment> commentRepo;
        private readonly EFGenericRepository<FreelanceLand.Models.Task> taskRepo;
        private readonly EFGenericRepository<TaskHistory> historyRepo;
        private readonly ApplicationContext db;

        public TasksService(IMapper mapper, ApplicationContext context)
        {
            db = context;
            this.mapper = mapper;
            taskRepo = new EFGenericRepository<FreelanceLand.Models.Task>(context);
            historyRepo = new EFGenericRepository<TaskHistory>(context);
            commentRepo = new EFGenericRepository<Comment>(context);
        }
        public TasksService() { }

        public async Task<PagedList<TaskDTO>> GetHistoryTaskByUser(int id, int page, string search, int priceTo, int priceFrom, string[] categ)
        {
            search = search ?? "";
            if (priceTo == 0) priceTo = 999999;
            if (categ.Length == 0) categ = new string[] { "" };
            var entities = await taskRepo.GetWithIncludeAsync(
                    o => o.TaskStatusId == (int)StatusEnum.Done  &&
                    o.Title.Contains(search) &&
                    o.Price <= priceTo &&
                    o.Price >= priceFrom &&
                    o.ExecutorId == id &&
                    categ.Any(s => o.TaskCategory.Type.Contains(s)),

                    p => p.TaskCategory, k => k.Comments
            );
            var dtos = mapper.Map<IEnumerable<FreelanceLand.Models.Task>, IEnumerable<TaskDTO>>(entities);
            var query = dtos.AsQueryable();

            return new PagedList<TaskDTO>(
                query, page, pageSize);
        }
        const int pageSize = 10;
        public async Task<PagedList<TaskDTO>> GetTasks(int page, string search, int priceTo, int priceFrom, string[] categ)
        {
            search = search ?? "";
            if (priceTo == 0) priceTo = 999999;
            if (categ.Length == 0) categ = new string[] { "" };
            var entities = await taskRepo.GetWithIncludeAsync(
                    o => o.TaskStatusId == (int)StatusEnum.ToDo && 
                    o.Title.Contains(search) &&
                    o.Price <= priceTo && 
                    o.Price >= priceFrom&&
                    categ.Any(s=> o.TaskCategory.Type.Contains(s)),
                    
                    p => p.TaskCategory, k => k.Comments
            );
            var dtos = mapper.Map<IEnumerable<FreelanceLand.Models.Task>, IEnumerable<TaskDTO>>(entities);
            var query = dtos.AsQueryable();

            return new PagedList<TaskDTO>(
                query, page, pageSize);
        }

        public async System.Threading.Tasks.Task DeleteTask(int id)
        {
            var task = await taskRepo.FindByIdAsync(id);
            var comment = await commentRepo.GetAsync(c => c.TaskId == task.Id);
            var history = await historyRepo.GetAsync(h => h.Task.Id == task.Id);
            foreach (var c in comment)
            {
                await commentRepo.RemoveAsync(c);
            }
            foreach (var h in history)
            {
                await historyRepo.RemoveAsync(h);
            }
            await taskRepo.RemoveAsync(task);
        }

        public async Task<PagedList<TaskDTO>> GetActiveTaskByUser( int id, int page, string search, int priceTo, int priceFrom, string[] categ)
        {
            search = search ?? "";
            if (priceTo == 0) priceTo = 999999;
            if (categ.Length == 0) categ = new string[] { "" };
            var entities = await taskRepo.GetWithIncludeAsync(
                    o => o.TaskStatusId == (int)StatusEnum.InProgress &&
                    o.Title.Contains(search) &&
                    o.Price <= priceTo &&
                    o.Price >= priceFrom &&
                    o.ExecutorId == id &&
                    categ.Any(s => o.TaskCategory.Type.Contains(s)),

                    p => p.TaskCategory, k => k.Comments
            );
            var dtos = mapper.Map<IEnumerable<FreelanceLand.Models.Task>, IEnumerable<TaskDTO>>(entities);
            var query = dtos.AsQueryable();

            return new PagedList<TaskDTO>(
                query, page, pageSize);
        }

        public async Task<int?> GetCustomerAsync(int id)
        {
            var executor = (await taskRepo.GetAsync(x => x.Id == id)).FirstOrDefault().CustomerId;
            return executor;
        }
        public async Task<PagedList<TaskDTO>> GetCreatedTaskByUser(int id, int page, string search, int priceTo, int priceFrom, string[] categ)
        {
            search = search ?? "";
            if (priceTo == 0) priceTo = 999999;
            if (categ.Length == 0) categ = new string[] { "" };
            var entities = await taskRepo.GetWithIncludeAsync(
                    o => o.Title.Contains(search) &&
                    o.Price <= priceTo &&
                    o.Price >= priceFrom &&
                    o.CustomerId == id &&
                    categ.Any(s => o.TaskCategory.Type.Contains(s)),

                    p => p.TaskCategory, k => k.Comments
            );
            var dtos = mapper.Map<IEnumerable<FreelanceLand.Models.Task>, IEnumerable<TaskDTO>>(entities);
            var query = dtos.AsQueryable();

            return new PagedList<TaskDTO>(
                query, page, pageSize);
        }
    }
}
