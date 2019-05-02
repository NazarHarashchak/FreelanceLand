using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace FreelanceLand.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<TaskCategory> TaskCategories { get; set; }
        public DbSet<TaskHistory> TaskHistories { get; set; }
        public DbSet<TaskStatus> TaskStatuses { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Image> Images { get; set; }
<<<<<<< HEAD
        public DbSet<Ratings> Ratings { get; set; }
        public DbSet<UserStatus> UserStatuses { get; set; }
=======
        
>>>>>>> d1328d81d495adeb35ce2ffdd37e1b25b4339e7a
        public ApplicationContext() { }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

