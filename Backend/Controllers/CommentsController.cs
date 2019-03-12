﻿using System;
using Backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using System.Collections.Generic;

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
        public ActionResult Post([FromBody] CommentDTO comment)
        {
            var result = commentsService.AddComment(comment);
            return Ok(result);
        }
    }
}