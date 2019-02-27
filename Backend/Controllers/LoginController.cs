using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreelanceLand.Models;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        EFGenericRepository<User> userRepository = new EFGenericRepository<User>(new ApplicationContext());

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult ValidateUser(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                return BadRequest(new { message = "Login or password is incorrect" });
            }
            IEnumerable<User> users = userRepository.Get(u => u.Login == login);
            if (users.Count(u => u.Password == password) > 0)
            {
                return Ok();
            }

            return BadRequest(new { message = "Login or password is incorect" });
        }
    }
}
