using AutoMapper;
using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Backend.Services
{
    //public class MocUserRepo: IGenericRepository<User>
    //{
    //    private List<User> localDB = new List<User>
    //    {
    //        new User
    //        {
    //            Name = "Vasa",
    //        }
    //    };

    //    public async System.Threading.Tasks.Task CreateAsync(User item)
    //    {
    //        localDB.Add(item);
    //    }

    //    public Task<User> FindByIdAsync(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IEnumerable<User>> GetAsync()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IEnumerable<User>> GetAsync(Expression<Func<User, bool>> predicate)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public System.Threading.Tasks.Task RemoveAsync(User item)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public System.Threading.Tasks.Task UpdateAsync(User item)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public class TopUsersService : ITopUsersService
    {
        private readonly IMapper mapper;

        private readonly IGenericRepository<User> _users;

        public TopUsersService(IMapper mapper, ApplicationContext context): this(mapper, new EFGenericRepository<User>(context))
        {
            
        }

        public TopUsersService(IMapper mapper, EFGenericRepository<User> repo)
        {
            this.mapper = mapper;
            _users = repo;
        }

        public async Task<IEnumerable<TopUserDTO>> GetTop5Users()
        {
            //TODO: change select query when rating field will be added to user table
            var entities = await _users.GetAsync(t => t.Id != 0, 5);
            var dtos = mapper.Map<IEnumerable<User>, IEnumerable<TopUserDTO>>(entities);
            return dtos;
        }
    }
}
