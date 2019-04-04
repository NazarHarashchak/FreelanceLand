using AutoMapper;
using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using Backend.Services;
using FreelanceLand.Models;
using Xunit;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Tests
{
    public class CommentServiceUnitTest
    {
        private readonly IMapper _mapper;
        private readonly ApplicationContext db;

        [Fact]
        public void GetAllComments()
        {
            //Arange
            List<CommentDTO> comments = new List<CommentDTO>();
            comments.Add(new CommentDTO
            {
                Content = "I can do it!",
                Id = 2,
                UserId = 8,
                TaskId = 3,
                Date = "2018-10-04 22:30:25.0000000"
            });
            comments.Add(new CommentDTO
            {
                Content = "i want do this task",
                Id = 6,
                UserId = 3,
                TaskId = 3,
                Date = "2019-03-26 11:27:33.0000000"
            });
            comments.Add(new CommentDTO
            {
                Content = "I can start working",
                Id = 7,
                UserId = 3,
                TaskId = 3,
                Date = "2019-03-26 11:48:33.0000000"
            });
            comments.Add(new CommentDTO
            {
                Content = "i want do this task",
                Id = 8,
                UserId = 3,
                TaskId = 3,
                Date = "2019-03-26 11:49:20.0000000"
            });
            comments.Add(new CommentDTO
            {
                Content = "i can do this for 200$",
                Id = 9,
                UserId = 3,
                TaskId = 3,
                Date = "2019-03-26 11:51:48.0000000"
            });
            ICommentsService commentService;

            //Act
            var result = (IEnumerable<CommentDTO>)commentService.GetComments(3);

            //Assert
            Assert.Empty(result);
        }
    }
}
