using AutoMapper;
using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace Backend.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly IMapper mapper;
        private EFGenericRepository<Comment> commentRepo;
        private EFGenericRepository<User> userRepo;
        private IImageService imageService;

        public CommentsService(IMapper mapper, ApplicationContext context)
        {
            commentRepo = new EFGenericRepository<Comment>(context);
            userRepo = new EFGenericRepository<User>(context);
            imageService = new ImageService(mapper, context);
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CommentDTO>> GetComments(int taskId)
        {
            List<Comment> myComments = (await commentRepo.GetWithIncludeAsync(task => task.TaskId == taskId,
                                                                            user => user.User)).ToList();
            List<CommentDTO> result = mapper.Map< List<Comment>, List<CommentDTO>> (myComments);

            for (int i = 0; i < result.Count(); i++) 
            {
                result[i].Photo = await imageService.GetImageAsync(result[i].UserId);
            }

            return result;
        }
        
        public async Task<IEnumerable<CommentDTO>> AddComment(CommentDTO comment)
        {
            comment.Date = DateTime.Now.ToString();
            comment.UserName = (await userRepo.FindByIdAsync(comment.UserId)).Name;

            var myComment = mapper.Map<CommentDTO, Comment>(comment);
            await commentRepo.CreateAsync(myComment);

            var result = await GetComments(comment.TaskId);
            return result;
        }

        public async System.Threading.Tasks.Task DeleteComment(int id)
        {
            var comment = await commentRepo.FindByIdAsync(id);
            await commentRepo.RemoveAsync(comment);
        }
    }
}
