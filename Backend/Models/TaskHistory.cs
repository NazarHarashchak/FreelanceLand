using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreelanceLand.Models
{
    public class TaskHistory
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int? TaskExecutorId { get; set; }
        [ForeignKey("TaskExecutorId")]
        public User TaskExecutor { get; set; }

        public int? TaskCustomerId { get; set; }
        [ForeignKey("TaskCustomerId")]
        [InverseProperty("UserHistories")]
        public User TaskCustomer { get; set; }

        public int TaskId { get; set; }
        public Task Task { get; set; }
    }
}

