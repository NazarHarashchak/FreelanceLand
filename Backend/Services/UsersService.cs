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
        EFGenericRepository<User> userRepo;
        private readonly ApplicationContext db;

        public UsersService(IMapper mapper, ApplicationContext context)
        {
            _mapper = mapper;
            db = context;
           userRepo  = new EFGenericRepository<User>(context);
        }

        public IEnumerable<UserDTO> GetAllEntities()
        {
            var entities = userRepo.Get();
            var dtos = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(entities);
            return dtos;
        }

        public UserLoginDTO GetUserByLogin(string login)
        {
            var user = userRepo.Get(u => u.Login == login).FirstOrDefault();

            if (user == null)
                return null;

            var dto = _mapper.Map<User, UserLoginDTO >(user);
            return dto;
        }

        public UserLoginDTO Authenticate(string login, string password)
        {
            var dto = GetUserByLogin(login);

            if (dto == null)
                return null;

            if (dto.Password == password)
                return dto;

            return null;
        }

        public void CreateUser(string login, string password)
        {
            if (GetUserByLogin(login) == null)
            {
                UserDTO dto = new UserDTO();
                dto.Name = " ";
                dto.Sur_Name = " ";
                dto.Birth_Date = new DateTime();
                dto.Phone_Number = null;
                dto.Email = " ";
                dto.Login = login;
                dto.Password = password;
                dto.UserRoleId = 1;
                var user = _mapper.Map<UserDTO, User>(dto);
                userRepo.Create(user);
            }
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
