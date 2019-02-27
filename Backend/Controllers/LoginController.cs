using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreelanceLand.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        EFGenericRepository<User> userRepository = new EFGenericRepository<User>(new ApplicationContext());

        [HttpGet]
        public bool ValidateUser(string login, string password)
        {
            bool authentication = false;
            IEnumerable<User> users = userRepository.Get(u => u.Login == login);
            if (users.Count(u => u.Password == password) > 0)
            {
                authentication = true;
            }
            return authentication;
        }
    }
}
