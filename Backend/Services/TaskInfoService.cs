using AutoMapper;
using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            this.mapper = mapper;
            taskRepo = new EFGenericRepository<FreelanceLand.Models.Task>(context);
            historyRepo = new EFGenericRepository<TaskHistory>(context);
            userRepo = new EFGenericRepository<User>(context);
            commentRepo = new EFGenericRepository<Comment>(context);
        }

        public async Task<TaskDescription> GetTaskDescription(int id)
        {
            var entities = await taskRepo.FindByIdAsync(id);
            var dtos = mapper.Map<FreelanceLand.Models.Task, TaskDescription>(entities);
            return dtos;
        }

        public async Task<CustomerDTO> GetTaskCustomer(int taskId)
        {
            int userId = 0;
            IEnumerable<TaskHistory> history = await historyRepo.GetAsync();
            foreach (TaskHistory s in history)
            {
                if (s.Id == taskId) userId = (int)s.Id;
            }
            var dtos = mapper.Map<User, CustomerDTO>(await userRepo.FindByIdAsync(userId));
            return dtos;
        }
    }
}
