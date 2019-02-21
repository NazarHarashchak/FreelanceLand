using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace FreelanceLand.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime DateAndTime { get; set; }

        public int? SenderUserId { get; set; }
        [ForeignKey("SenderUserId")]
        public User SenderUser { get; set; }

        public int? GetterUserId { get; set; }
        [ForeignKey("GetterUserId")]
        [InverseProperty("UserMessages")]
        public User GetterUser { get; set; }

    }
}
