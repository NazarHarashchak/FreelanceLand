using AutoMapper;
using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using System.Collections.Generic;
namespace Backend.Services
{
    public class UsersService : IUsersService
    {
        private readonly IMapper _mapper;
        EFGenericRepository<User> userRepo = new EFGenericRepository<User>(new ApplicationContext());

        public UsersService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<UserDTO> GetAllEntities()
        {
            var entities = userRepo.Get();
            var dtos = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(entities);
            return dtos;
        }
    }
}
