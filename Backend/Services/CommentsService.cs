using AutoMapper;
using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using System.Collections.Generic;
using System;

namespace Backend.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly IMapper mapper;
        private EFGenericRepository<Comment> commentRepo;
        private EFGenericRepository<User> userRepo;

        public CommentsService(IMapper mapper,ApplicationContext context)
        {
            commentRepo = new EFGenericRepository<Comment>(context);
            userRepo = new EFGenericRepository<User>(context);
            this.mapper = mapper;
        }

        public IEnumerable<CommentDTO> GetComments(int taskId)
        {
            IEnumerable<Comment> myComments = commentRepo.Get();
            List<CommentDTO> result = new List<CommentDTO>();

            // myComments = commentRepo.GetWithInclude(t => t.TaskId == taskId);
            //result = mapper.Map<Comment, CommentDTO>(myComments);
            foreach (var s in myComments)
            {
                if (s.TaskId == taskId)
                {
                    var dtos = mapper.Map<Comment, CommentDTO>(s);
                    dtos.UserName = mapper.Map<User, CustomerDTO>(userRepo.FindById(dtos.UserId)).Name;
                    result.Add(dtos);
                }
            }
            return result;
        }

        public CommentDTO AddComment(CommentDTO comment)
        {
            comment.Date = DateTime.Now.ToString();
            User user = userRepo.FindById(comment.UserId);
            comment.UserName = user.Name;

            var myComment = mapper.Map<CommentDTO, Comment>(comment);
            commentRepo.Create(myComment);

            return comment;
        }
    }
}
