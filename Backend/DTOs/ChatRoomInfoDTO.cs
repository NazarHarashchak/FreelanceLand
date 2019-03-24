using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class ChatRoomInfoDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FirstUserLogin { get; set; }

        public string SecondUserLogin { get; set; }
    }
}
