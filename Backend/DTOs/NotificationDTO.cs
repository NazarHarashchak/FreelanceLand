using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class NotificationDTO
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime DateAndTime { get; set; }

        public int UserId { get; set; }
    }
}
