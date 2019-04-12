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

        public async Task<IEnumerable<TaskDTO>> GetHistoryTaskByUser(int id)
        {
            var entities = (await taskRepo.GetWithIncludeAsync(o => o.TaskStatusId == (int)StatusEnum.Done,
                p => p.TaskCategory, k => k.Comments))
                .Where(o => o.ExecutorId == id);
            var dtos = mapper.Map<IEnumerable<FreelanceLand.Models.Task>, IEnumerable<TaskDTO>>(entities);
            return dtos;
        }
        const int pageSize = 10;
        public async Task<PagedList<TaskDTO>> GetTasks(int page, string searchText, int priceTo, int priceFrom, string[] categ)
        {
            searchText = searchText ?? "";
            if (priceTo == 0 && priceFrom == 0) priceTo = 999999;
            if (categ.Length == 0) categ = new string[] { "" };
            var entities = await taskRepo.GetWithIncludeAsync(
                    o => o.TaskStatusId == (int)StatusEnum.ToDo && 
                    o.Title.Contains(searchText) &&
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

        public async Task<IEnumerable<TaskDTO>> GetActiveTaskByUser(int id)
        {
            var entities = (await taskRepo.GetWithIncludeAsync(o => o.TaskStatusId == (int)StatusEnum.InProgress, p => p.TaskCategory, k => k.Comments))
                .Where(o => o.ExecutorId==id);
            var dtos = mapper.Map<IEnumerable<FreelanceLand.Models.Task>, IEnumerable<TaskDTO>>(entities);
            return dtos;
        }
    }
}
