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
        EFGenericRepository<FreelanceLand.Models.Task> taskRepo = new EFGenericRepository<FreelanceLand.Models.Task>
                                                                                    (new ApplicationContext());

        [HttpGet("{id}")]
        public ActionResult<FreelanceLand.Models.Task> Get(int id)
        {
            ApplicationContext context = new ApplicationContext();
            FreelanceLand.Models.Task task = taskRepo.FindById(id);
            return task;
        }

        [HttpGet("{number},{id}")]
        public ActionResult<User> Get(int number, int id)
        {
            ApplicationContext context = new ApplicationContext();
            FreelanceLand.Models.Task task = taskRepo.FindById(id);
            User user = new User();
            foreach (TaskHistory history in context.TaskHistories)
            {
                if (history.TaskId == id)
                {
                    int userId = Convert.ToInt32(history.TaskCustomerId);
                    user = userRepo.FindById(userId);
                    break;
                }
            }
            return user;
        }
    }
}