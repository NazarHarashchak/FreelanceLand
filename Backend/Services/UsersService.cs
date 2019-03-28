using AutoMapper;
using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using Backend.Pagination;
using FreelanceLand.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class UsersService : IUsersService
    {
        private readonly IEmailService _emailService;

        private readonly IMapper _mapper;
        private EFGenericRepository<User> userRepo;
        private EFGenericRepository<UserRoles> rolesRepo;
        private EFGenericRepository<Image> imageRepo;
        private readonly ApplicationContext db;

        public UsersService(IMapper mapper, ApplicationContext context, IEmailService emailService)
        {
            _mapper = mapper;
            db = context;
            rolesRepo = new EFGenericRepository<UserRoles>(context);
            userRepo  = new EFGenericRepository<User>(context);
            imageRepo = new EFGenericRepository<Image>(context);
            _emailService = emailService;
        }

        const int pageSize = 10;
        public async Task<PagedList<UserDTO>> GetUsers(int pageNumber)
        {
            var entities = await userRepo.GetAsync();
            var dtos = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(entities);
            var query = dtos.AsQueryable();

            return new PagedList<UserDTO>(
                query, pageNumber, pageSize);
        }


        public async Task<User> GetUserByLogin(string login)
        {
            var user = (await userRepo.GetAsync(u => u.Login == login)).FirstOrDefault();

            if (user == null)
                return null;

            if (user.EmailConfirmed == true)
            {
                var dto = _mapper.Map<User, UserAccountDTO>(user);
                return user;
            }
            return null;
        }

        public async Task<UserAccountDTO> Authenticate(string login, string password)
        {
            var user = await GetUserByLogin(login);

            if (user == null)
                return null;

            var dto = _mapper.Map<User, UserAccountDTO>(user);

            if (BCrypt.Net.BCrypt.Verify(password, dto.Password))
                return dto;

            return null;
        }

        public async Task<UserAccountDTO> ConfirmEmail(string confirmCode)
        {
            var user = (await userRepo.GetAsync(u => u.ConfirmCode == confirmCode)).FirstOrDefault();
            if (user == null)
                return null;

            user.EmailConfirmed = true;
            await userRepo.UpdateAsync(user);

            return _mapper.Map<User, UserAccountDTO>(user);
        }

        public async Task<UserAccountDTO> CreateUser(string email, string login, string password)
        {
            
            if (await GetUserByLogin(login) == null)
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
                user.EmailConfirmed = false;
                user.ConfirmCode = Guid.NewGuid().ToString();
                user.UserRoleId = (await rolesRepo.GetAsync(r => r.Type == "User")).FirstOrDefault().Id;
                await userRepo.CreateAsync(user);

                string MessagesRegistr = $"<h2>Dear user</h2><h3>Your registration request was successful approve</h3><a href='https://localhost:44332/Account/confirmEmail?confirmCode={user.ConfirmCode}'>Confirm registration </a>";
                _emailService.SendEmailAsync(user.Email, "Administration", MessagesRegistr);

                var dto = _mapper.Map<User, UserAccountDTO>(user);

                return dto;
            }
            return null;
        }
        public async Task<UserInformation> GetUserInformation(int id)
        {
            var entities = (await userRepo.GetWithIncludeAsync(u => u.Id == id, r => r.UserRole)).FirstOrDefault();
            var dtos = _mapper.Map<User, UserInformation>(entities);
            return dtos;
        }

        
       
        public async Task<User> UpdateUser(int id, [FromBody] UserInformation value)
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
                    if (value.UserRoleName!=null)
                        result.UserRoleId = (await rolesRepo.GetAsync(r => r.Type == value.UserRoleName)).FirstOrDefault().Id;
                db.SaveChanges();

                }

                return await userRepo.FindByIdAsync(id);
        }

        public async Task<string> CreateUserImage(ImageDTO Image)
        {
            Image im = (await imageRepo.GetAsync((el) => el.UserId == Image.UserId)).FirstOrDefault();
            if (im != null)
            {
                await imageRepo.RemoveAsync(im);
            }

            if (Image == null) { return ("empty"); };
            byte[] fileBytes = null;
            using (var fs1 = Image.Image.OpenReadStream())
            using (var memoryStream = new MemoryStream())
            {
                await fs1.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }
            Image image = new Image();
            image.UserId = Image.UserId;
            image.FileName = Image.FileName;
            image.Picture = fileBytes;
            await imageRepo.CreateAsync(image);
            return "done";
        }

        public async Task<IEnumerable<UserRolesDTO>> GetAllRolesDtos()
        {
            var entities = await rolesRepo.GetAsync();
            var dtos = _mapper.Map<IEnumerable<UserRoles>, IEnumerable<UserRolesDTO>>(entities);
            return dtos;
        }

        public async Task<UserAccountDTO> ChangePass(string login, string password)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var user = await GetUserByLogin(login);
            user.Password = passwordHash;
            await userRepo.UpdateAsync(user);
            var dto = _mapper.Map<User, UserAccountDTO>(user);
            return dto;
        }
    }
}
