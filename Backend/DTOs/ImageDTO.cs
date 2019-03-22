using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class ImageDTO
    {
        public string FileName { get; set; }

        public IFormFile Image { get; set; }

        public int UserId { get; set; }
    }
}
