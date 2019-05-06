using Backend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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

        public string ConfirmCode { get; set; }

        public bool EmailConfirmed { get; set; }

        public int Rating { get; set; }
        public List<Review> UserReviews { get; set; }
        public List<TaskHistory> UserHistories { get; set; }
        public List<Message> UserMessages { get; set; }
        public List<Comment> UserComments { get; set; }
        public List<Image> Images { get; set; }
        public List<Notification> UserNotifications { get; set; }

        [InverseProperty("Customer")]
        public  virtual List<Task> CustomerTasks { get; set; }

        [InverseProperty("Executor")]
        public  virtual  List<Task> UserTasks { get; set; }

        [InverseProperty("UpdatedBy")]
        public virtual  List<Task> UpdateTasks { get; set; }

        [InverseProperty("UpdatedByUser")]
        public  virtual List<TaskHistory> UpdatedTaskHistories { get; set; }
        public int? UserRoleId { get; set; }
        public UserRoles UserRole { get; set; }

        [InverseProperty("UsersRate")]
        public virtual List<Ratings> UserRatings { get; set; }

        [InverseProperty("UsersRateBy")]
        public virtual List<Ratings> ByUserRatings { get; set; }

    }
}
