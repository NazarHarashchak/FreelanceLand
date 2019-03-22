using AutoMapper;
using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using System.Collections.Generic;
using System;
using System.Linq;

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
            IEnumerable<Comment> myComments = commentRepo.GetWithInclude(task => task.TaskId == taskId,
                                                                            user => user.User);
            IEnumerable<CommentDTO> result = mapper.Map< IEnumerable<Comment>, IEnumerable<CommentDTO>> (myComments);

            return result;
        }
        
        public CommentDTO AddComment(CommentDTO comment)
        {
            comment.Date = DateTime.Now.ToString();
            comment.UserName = userRepo.FindById(comment.UserId).Name;

            var myComment = mapper.Map<CommentDTO, Comment>(comment);
            commentRepo.Create(myComment);

            return comment;
        }
    }
}
