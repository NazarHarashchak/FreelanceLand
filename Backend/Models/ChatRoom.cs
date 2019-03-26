using FreelanceLand.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class ChatRoom
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("Creator")]
        public int? CreatorId { get; set; }
        public User Creator { get; set; }

        [ForeignKey("SecondUser")]
        public int? SecondUserId { get; set; }
        public User SecondUser { get; set; }

        public List<Message> Messages { get; set; }
    }
}
