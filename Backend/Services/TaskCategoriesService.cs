using AutoMapper;
using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using System;
using System.Collections.Generic;

namespace Backend.Services
{
    public class TaskCategoriesService : ITaskCategoriesService
    {
        private readonly IMapper mapper;
        EFGenericRepository<TaskCategory> taskCategoryRepo;

        public TaskCategoriesService(IMapper mapper, ApplicationContext context)
        {
            this.mapper = mapper;
            taskCategoryRepo = new EFGenericRepository<TaskCategory>(context);
        }

        public IEnumerable<TaskCategoryDTO> GetAllEntities()
        {
            var entities = taskCategoryRepo.Get();
            var dtos = mapper.Map<IEnumerable<TaskCategory>, IEnumerable<TaskCategoryDTO>>(entities);
            return dtos;
        }
    }
}
