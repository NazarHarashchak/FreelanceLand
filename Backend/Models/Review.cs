using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreelanceLand.Models
{
    public class Review
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public int? ExecutorUserId { get; set; }
        [ForeignKey("ExecutorUserId")]
        public User ExecutorUser { get; set; }

        public int? CustomerUserId { get; set; }
        [ForeignKey("CustomerUserId")]
        [InverseProperty("UserReviews")]
        public User CustomerUser { get; set; }
    }
}
