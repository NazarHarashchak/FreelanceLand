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
        EFGenericRepository<TaskCategory> taskCategoryRepo = new EFGenericRepository<TaskCategory>(new ApplicationContext());

        public TaskCategoriesService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public IEnumerable<TaskCategoryDTO> GetAllEntities()
        {
            var entities = taskCategoryRepo.Get();
            var dtos = mapper.Map<IEnumerable<TaskCategory>, IEnumerable<TaskCategoryDTO>>(entities);
            return dtos;
        }
    }
}
