using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        
        [ForeignKey("Customer")]
        public  int? CustomerId { get; set; }
        public  User Customer { get; set; }


        [ForeignKey("Executor")]
        public int? ExecutorId { get; set; }
        public  User Executor { get; set; }

        [ForeignKey("UpdatedBy")]
        public int? UpdatedById { get; set; }
        public  User UpdatedBy { get; set; }


        public virtual List<TaskHistory> TaskHistories { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
