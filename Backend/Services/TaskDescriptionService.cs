using AutoMapper;
using Backend.DTOs;
using Backend.Enums;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using System.Collections.Generic;

namespace Backend.Services
{
    public class TaskInfoService : ITaskInfoService
    {
        private readonly IMapper mapper;
        EFGenericRepository<Task> taskRepo = new EFGenericRepository<Task>(new ApplicationContext());
        EFGenericRepository<TaskHistory> historyRepo = new EFGenericRepository<TaskHistory>(new ApplicationContext());
        EFGenericRepository<User> userRepo = new EFGenericRepository<User>(new ApplicationContext());
        EFGenericRepository<Comment> commentRepo = new EFGenericRepository<Comment>(new ApplicationContext());

        public TaskInfoService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public TaskDescription GetTaskDescription(int id)
        {
            var entities = taskRepo.FindById(id);
            var dtos = mapper.Map<Task, TaskDescription>(entities);
            return dtos;
        }

        public Customer GetTaskCustomer(int taskId)
        {
            int userId = 0;
            IEnumerable<TaskHistory> history = historyRepo.Get();
            foreach (TaskHistory s in history)
            {
                if (s.TaskId == taskId) userId = (int)s.TaskCustomerId;
            }
            var dtos = mapper.Map<User, Customer>(userRepo.FindById(userId));
            return dtos;
        }

        public IEnumerable<CommentDTO> GetComments(int taskId)
        {
            IEnumerable<Comment> MyComments = commentRepo.Get();
            List<CommentDTO> result = new List<CommentDTO>();
            foreach (var s in MyComments)
            {
                if ((int)s.TaskId == taskId)
                {
                    var dtos = mapper.Map<Comment, CommentDTO>(s);
                    dtos.UserName = mapper.Map<User, Customer>(userRepo.FindById(dtos.UserId)).Name;
                    result.Add(dtos);
                }
            }
            return result;
        }
    }
}
