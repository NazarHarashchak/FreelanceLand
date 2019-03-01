using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FreelanceLand.Models;
using FreelanceLand;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskInfoController : ControllerBase
    {
        EFGenericRepository<User> userRepo = new EFGenericRepository<User>(new ApplicationContext());
        EFGenericRepository<FreelanceLand.Models.Task> taskRepo = new EFGenericRepository<FreelanceLand.Models.Task>(new ApplicationContext());

        [HttpGet]
        public ActionResult<FreelanceLand.Models.Task> Get()
        {
            FreelanceLand.Models.Task task = taskRepo.FindById(1);
            User user = userRepo.FindById(1);
            task.Date.ToShortDateString();
            task.Deadline.ToShortDateString();
           return (task);
        }
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<string>> Get(int id)
        {
            User user = userRepo.FindById(id);
            string userStr = user.Name.ToString();
            return new string[] { userStr };
        }
    }
}