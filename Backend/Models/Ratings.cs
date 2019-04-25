using FreelanceLand.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Ratings
    {
        public int Id { get; set; }
        [ForeignKey("UsersRate")]
        public int UserId { get; set; }
        public int RateByUser { get; set; }
        [ForeignKey("UserStatuses")]
        public int UserStatusId { get; set; }
        public int? Mark { get; set; }
        public UserStatus UserStatuses { get; set; }
        public User UsersRate { get; set; }
    }
}
