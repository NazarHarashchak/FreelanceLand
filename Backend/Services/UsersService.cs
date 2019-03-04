using AutoMapper;
using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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

        public UserInformation GetUserInformation(int id)
        {
            var entities = userRepo.FindById(id);
            var dtos = _mapper.Map<User, UserInformation>(entities);
            return dtos;
        }

        public User UpdateUser(int id, [FromBody] UserInformation value)
        {
            using (var db = new ApplicationContext())
            {
                var result = db.Users.SingleOrDefault(b => b.Id == id);
                if (result != null)
                {
                    result.Name = value.Name;
                    result.Birth_Date = value.Birth_Date;
                    result.Email = value.Email;
                    result.Sur_Name = value.Sur_Name;
                    result.Phone_Number = value.Phone_Number;
                    result.Login = value.Login;
                    db.SaveChanges();

                }

                return userRepo.FindById(id);

            }

        }
    }
}
