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

        public DateTime Date { get; set; }

        public DateTime Deadline { get; set; }

        public int? TaskStatusId { get; set; }
        public TaskStatus TaskStatus { get; set; }

        public int? TaskCategoryId { get; set; }
        public TaskCategory TaskCategory { get; set; }

        public List<TaskHistory> TaskHistories { get; set; }
    }
}
