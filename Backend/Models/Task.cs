using System;
using System.Collections.Generic;

namespace FreelanceLand.Models
{
    public class Task
    {
        public int Id { get; set; }

        public int Price { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime DateUpdated { get; set; }

        public int? TaskStatusId { get; set; }
        public TaskStatus TaskStatus { get; set; }

        public int? TaskCategoryId { get; set; }
        public TaskCategory TaskCategory { get; set; }
        
        public virtual User Customer { get; set; }

        public virtual User Executor { get; set; }

        public virtual User UpdatedBy { get; set; }


        public virtual List<TaskHistory> TaskHistories { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
