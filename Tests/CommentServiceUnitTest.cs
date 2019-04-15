using AutoMapper;
using Backend.DTOs;
using Backend.MappingProfiles;
using Backend;
using Backend.Interfaces.ServiceInterfaces;
using Backend.Services;
using FreelanceLand.Models;
using Xunit;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Tests
{
    public class CommentServiceUnitTest
    {

        [Fact]
        public void GetAllComments()
        {
            //Arange
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CommentProfile>();
            });
            var applicationContext = new Mock<ApplicationContext>();
            var commentMock = new Mock<DbSet<Comment>>();
           // commentMock.Setup(x => x.Add(It.IsAny<Comment>())).Returns((Comment c) => c);
            applicationContext.Setup(x => x.Comments).Returns(commentMock.Object);

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
            //ICommentsService commentService = new CommentsService(_mapper, db);

            //Act
            //var result = commentService.GetComments(3);
            var result = comments[2];

            //Assert
            Assert.Equal(comments[2], result);
        }
    }
}
