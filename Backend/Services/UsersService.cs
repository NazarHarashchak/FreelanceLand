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
        private readonly IEmailService _emailService;

        private readonly IMapper _mapper;
        private EFGenericRepository<User> userRepo;
        private EFGenericRepository<UserRoles> rolesRepo;
        private readonly ApplicationContext db;

<<<<<<< HEAD
        public UsersService(IMapper mapper, IEmailService emailService, ApplicationContext context)
=======
        public UsersService(IMapper mapper, ApplicationContext context, IEmailService emailService)
>>>>>>> 0bf6d7dca81b1c1af9b803f3a39d9778b23f79c7
        {
            _mapper = mapper;
            db = context;
            rolesRepo = new EFGenericRepository<UserRoles>(context);
<<<<<<< HEAD
            userRepo = new EFGenericRepository<User>(context);
            _emailService = emailService;
=======
            userRepo  = new EFGenericRepository<User>(context);
             _emailService = emailService;
>>>>>>> 0bf6d7dca81b1c1af9b803f3a39d9778b23f79c7
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
            const string MessagesRegistr = ("<h2>Dear user</h2><h3>Your registration request was successful approve</h3>");
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

                _emailService.SendEmailAsync(user.Email, "Administration", MessagesRegistr);

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
