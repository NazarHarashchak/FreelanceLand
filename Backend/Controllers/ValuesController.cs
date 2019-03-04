using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreelanceLand.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
   
    public class ValuesController : ControllerBase
    {
        EFGenericRepository<User> userRepo = new EFGenericRepository<User>(new ApplicationContext());

        [HttpGet]
        public ActionResult<IEnumerable<string>> GetAllUsers()
        {
            ApplicationContext context = new ApplicationContext();
            List<FreelanceLand.Models.User> arr1 = context.Users.ToList();
            string [] arr = new String[arr1.Count];
            int i = 0;
            foreach (var val in arr1)
            {
                arr[i] = val.Name;
                i++;
            }

            return arr;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            User user = userRepo.FindById(id);

            return user;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
