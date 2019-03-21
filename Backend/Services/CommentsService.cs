using AutoMapper;
using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly IMapper mapper;
        private EFGenericRepository<Comment> commentRepo;
        private EFGenericRepository<User> userRepo;

        public CommentsService(IMapper mapper, ApplicationContext context)
        {
            commentRepo = new EFGenericRepository<Comment>(context);
            userRepo = new EFGenericRepository<User>(context);
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CommentDTO>> GetComments(int taskId)
        {
            IEnumerable<Comment> myComments =  await commentRepo.GetAsync();
            List<CommentDTO> result = new List<CommentDTO>();
            foreach (var s in myComments)
            {
                if (s.TaskId == taskId)
                {
                    var dtos = mapper.Map<Comment, CommentDTO>(s);
                    dtos.UserName = mapper.Map<User, CustomerDTO>(await userRepo.FindByIdAsync(dtos.UserId)).Name;
                    result.Add(dtos);
                }
            }
            return result;
        }

        public async Task<CommentDTO> AddComment(CommentDTO comment)
        {
            comment.Date = DateTime.Now.ToString();
            User user = await userRepo.FindByIdAsync(comment.UserId);
            comment.UserName = user.Name;

            var myComment = mapper.Map<CommentDTO, Comment>(comment);
            await commentRepo.CreateAsync(myComment);

            return comment;
        }

        public async System.Threading.Tasks.Task DeleteComment(int id)
        {
            var comment = await commentRepo.FindByIdAsync(id);
            await commentRepo.RemoveAsync(comment);
        }
    }
}
