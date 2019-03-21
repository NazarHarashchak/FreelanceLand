using Backend.Interfaces.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FreelanceLand.Models;
using Backend.DTOs;

namespace Backend.Services
{
    public class RolesService: IRolesUserService
    {
        private readonly IMapper mapper;
        private EFGenericRepository<UserRoles> userRolesRepo;

        public RolesService(IMapper mapper, ApplicationContext context)
        {
            userRolesRepo = new EFGenericRepository<UserRoles>(context);
            this.mapper = mapper;
        }

        public async Task<IEnumerable<UserRolesDTO>> GetAllRolesDtos()
        {
            var entities = await userRolesRepo.GetAsync();
            var dtos = mapper.Map<IEnumerable<UserRoles>, IEnumerable<UserRolesDTO>>(entities);
            return dtos;
        }
    }
}
