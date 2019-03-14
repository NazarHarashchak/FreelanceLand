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
        EFGenericRepository<UserRoles> userRolesRepo=new EFGenericRepository<UserRoles>(new ApplicationContext());

        public RolesService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public IEnumerable<UserRolesDTO> GetAllRolesDtos()
        {
            var entities = userRolesRepo.Get();
            var dtos = mapper.Map<IEnumerable<UserRoles>, IEnumerable<UserRolesDTO>>(entities);
            return dtos;
        }
    }
}
