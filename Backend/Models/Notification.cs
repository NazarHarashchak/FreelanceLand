using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreelanceLand.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime DateAndTime { get; set; }

        [ForeignKey("Receiver")]
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
