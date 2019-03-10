using Backend.DTOs;
using FreelanceLand.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Backend.Services;
using Backend.Interfaces.ServiceInterfaces;

namespace Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        EFGenericRepository<User> userRepo = new EFGenericRepository<User>(new ApplicationContext());

        private IUserTokensService _userTokensService;

        public AccountController(IUserTokensService userTokensService)
        {
            _userTokensService = userTokensService;
        }

        [HttpPost("token")]
        public async System.Threading.Tasks.Task Token([FromBody] UserLoginDTO user)
        {
            var username = user.Login;
            var password = user.Password;

            UserLoginDTO _user = user;

            int id = userRepo.Get(u => u.Login == user.Login).FirstOrDefault().Id;
            
            await Response.WriteAsync(_userTokensService.CreateToken(_user, id));

        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            User person = userRepo.Get(u => u.Login == username).FirstOrDefault();
            
            string role = "User";
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}