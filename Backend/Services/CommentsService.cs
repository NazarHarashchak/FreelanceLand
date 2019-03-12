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
        EFGenericRepository<Comment> commentRepo = new EFGenericRepository<Comment>(new ApplicationContext());
        EFGenericRepository<User> userRepo = new EFGenericRepository<User>(new ApplicationContext());

        public CommentsService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public IEnumerable<CommentDTO> GetComments(int taskId)
        {
            IEnumerable<Comment> MyComments = commentRepo.Get();
            List<CommentDTO> result = new List<CommentDTO>();
            foreach (var s in MyComments)
            {
                if (s.TaskId == taskId)
                {
                    var dtos = mapper.Map<Comment, CommentDTO>(s);
                    dtos.UserName = mapper.Map<User, Customer>(userRepo.FindById(dtos.UserId)).Name;
                    result.Add(dtos);
                }
            }
            return result;
        }

        public CommentDTO AddComment(CommentDTO comment)
        {
            int commentId = 0;
            using (var db = new ApplicationContext())
            {
                var myComment = mapper.Map<CommentDTO, Comment>(comment);

                db.Comments.Add(myComment);
            }
            return comment;
        }
    }
}
