using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class MessageToUserDTO
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string SenderLogin { get; set; }

        public string DateAndTime { get; set; }
    }
}
