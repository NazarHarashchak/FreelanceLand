using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class CreateChatRoomAndSendFirstMessageDTO
    {
        public int creatorId { get; set; }

        public int secondUserId { get; set; }

        public string message { get; set; }
    }
}
