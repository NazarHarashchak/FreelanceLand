using AutoMapper;
using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class TopUsersService : ITopUsersService
    {
        private readonly IMapper mapper;

        private readonly EFGenericRepository<User> _users;

        public TopUsersService(IMapper mapper, ApplicationContext context)
        {
            this.mapper = mapper;
            _users = new EFGenericRepository<User>(context);
        }

        public IEnumerable<TopUserDTO> GetTop5Users()
        {
            //TODO: change select query when rating field will be added to user table
            var entities = _users.Get(t => t.Id != 0, 5);
            var dtos = mapper.Map<IEnumerable<User>, IEnumerable<TopUserDTO>>(entities);
            return dtos;
        }
    }
}
