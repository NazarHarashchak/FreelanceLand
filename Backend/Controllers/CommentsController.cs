using System;
using Backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Backend.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using FreelanceLand.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

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
        public ActionResult<IEnumerable<CommentDTO>> Get(int id)
        {
            var dtos = commentsService.GetComments(id);
            return Ok(dtos);
        }

        [HttpPost]
        public ActionResult<CommentDTO> Post([FromBody] CommentDTO comment)
        {
            var result = commentsService.AddComment(comment);
            return Ok(comment);
        }

        [Authorize(Roles = "Moderator")]
        [HttpGet("{id}")]
        void DeleteComment(int id)
        {
            commentsService.DeleteComment(id);
        }
    }
}