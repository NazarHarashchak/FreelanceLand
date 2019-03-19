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
        public DbSet<Image> Images { get; set; }

        public ApplicationContext() 
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=freelanceland3.0db;Trusted_Connection=True;");
        }

    }
}

