using Autofac.Extras.Moq;
using AutoMapper;
using Backend.DTOs;
using Backend.Models;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Threading.Tasks;
using Moq;
using Microsoft.EntityFrameworkCore;
using Backend.Services;
using Newtonsoft.Json.Linq;
using Backend.Controllers;

namespace Tests
{
    public class TaskServiceTests
    {
        List<FreelanceLand.Models.Task> expectedTaskDTO;
        Mock<ITasksService> mockTaskRepository;
        IEnumerable<TaskDTO> dto = new List<TaskDTO> { new TaskDTO { Id = 2, Description = "Test one" } };

    
        
        
    }
}
