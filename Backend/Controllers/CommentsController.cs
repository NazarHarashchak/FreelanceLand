using System;
using Backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Backend.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using FreelanceLand.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Backend.Common;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
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
            await commentsService.DeleteComment(comment.Id);
            await Response.WriteAsync(JsonConvert.SerializeObject(comment, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }
    }
}