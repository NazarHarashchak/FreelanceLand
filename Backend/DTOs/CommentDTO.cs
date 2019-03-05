using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Date { get; set; }

        public int UserId { get; set; }

        public int TaskId { get; set; }

        public string UserName { get; set; }
    }
}
