using Backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Backend.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Backend.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private ICommentsService commentsService;
        private INotificationService notificationService;
        private IUsersService usersService;

        public CommentsController(ICommentsService commentsService, INotificationService notificationService,
            IHubContext<NotificationHub> hubContext, IUsersService usersService)
        {
            this.commentsService = commentsService;
            this.notificationService = notificationService;
            this.usersService = usersService;
            _hubContext = hubContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> Get(int id)
        {
            var dtos = await commentsService.GetComments(id);
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> Post([FromBody] CommentDTO comment)
        {
            var result = await commentsService.AddComment(comment);
            return Ok(result);
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost("DeleteComment")]
        public async System.Threading.Tasks.Task DeleteComment([FromBody] CommentDTO comment)
        {
            string msg = "Your comment was deleted by moderator";
            var com = await commentsService.GetComment(comment.Id);
            await notificationService.AddNotification(msg, com.UserId);
            await _hubContext.Clients.All.SendAsync("sendMessage", com.UserId, msg);
            await commentsService.DeleteComment(comment.Id);
            await Response.WriteAsync(JsonConvert.SerializeObject(comment, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }
    }
}