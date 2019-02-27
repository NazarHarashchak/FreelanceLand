using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelanceLand.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        EFGenericRepository<User> userRepo = new EFGenericRepository<User>(new ApplicationContext());

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            
            ApplicationContext context = new ApplicationContext();
            List<FreelanceLand.Models.User> arr1 = context.Users.ToList();
            return arr1;

        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            User user = userRepo.FindById(id);
            return user;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
