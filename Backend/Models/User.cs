using System;
using System.Collections.Generic;

namespace FreelanceLand.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Sur_Name { get; set; }

        public DateTime Birth_Date { get; set; }

        public string Phone_Number { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public List<Review> UserReviews { get; set; }
        public List<TaskHistory> UserHistories { get; set; }
        public List<Message> UserMessages { get; set; }
        public List<Comment> UserComments { get; set; }

        public int? UserRoleId { get; set; }
        public UserRoles UserRole { get; set; }
    }
}
