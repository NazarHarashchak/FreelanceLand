using AutoMapper;
using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Services
{
    public class UsersService : IUsersService
    {
        private readonly IMapper _mapper;
        private EFGenericRepository<User> userRepo;
        private EFGenericRepository<UserRoles> rolesRepo;
        private readonly ApplicationContext db;

        public UsersService(IMapper mapper, ApplicationContext context)
        {
            _mapper = mapper;
            db = context;
            rolesRepo = new EFGenericRepository<UserRoles>(context);
            userRepo  = new EFGenericRepository<User>(context);
        }

        public IEnumerable<UserDTO> GetAllEntities()
        {
            var entities = userRepo.Get();
            var dtos = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(entities);
            return dtos;
        }


        public UserAccountDTO GetUserByLogin(string login)
        {
            var user = userRepo.Get(u => u.Login == login).FirstOrDefault();

            if (user == null)
                return null;


            var dto = _mapper.Map<User, UserAccountDTO >(user);
            return dto;
        }

        public UserAccountDTO Authenticate(string login, string password)
        {
            var dto = GetUserByLogin(login);

            if (dto == null)
                return null;


            if (BCrypt.Net.BCrypt.Verify(password, dto.Password))
                return dto;

            return null;
        }
        
        public UserAccountDTO CreateUser(string email, string login, string password)
        {
            if (GetUserByLogin(login) == null)
            {
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
                User user = new User();
                user.Name = "";
                user.Sur_Name = "";
                user.Birth_Date = new DateTime();
                user.Phone_Number = "+380-*";
                user.Email = email;
                user.Login = login;
                user.Password = passwordHash;
                userRepo.Create(user);
                var dto = _mapper.Map<User, UserAccountDTO>(user);

                return dto;
            }
            return null;
        }
        public UserInformation GetUserInformation(int id)
        {
            var entities = userRepo.FindById(id);
            var dtos = _mapper.Map<User, UserInformation>(entities);
            return dtos;
        }

        public User UpdateUser(int id, [FromBody] UserInformation value)
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
