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

        

        [Fact]
        public void GetHistoryTaskByUser_ValidCall( )
        {

            var appCont = new Mock<ApplicationContext>();
            var container = new MockingContainer<TasksService>();
            var map = new Mock<IMapper>();
            var mock = new Mock<ITasksService>();
            var actual = new TasksService(map.Object, appCont.Object);

            mock.Setup(repo => repo.GetHistoryTaskByUser(2)).
                ReturnsAsync(dto);

            var act= actual.a
            var result = actual.GetHistoryTaskByUser(2);
            Assert.Equal(result, result);
            mock.Verify();
        }
        
        
    }
}
