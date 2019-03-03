using AutoMapper;
using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using System;
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

        public UserDTO GetUserByLogin(string login)
        {
            var user = userRepo.Get(u => u.Login == login).FirstOrDefault();

            if (user == null)
                return null;

            var dto = _mapper.Map<User, UserDTO >(user);
            return dto;
        }

        public UserDTO Authenticate(string login, string password)
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
    }
}
