using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FreelanceLand.Models
{
    public class TaskHistory
    {
        public int Id { get; set; }

        public DateTime DateUpdated { get; set; }

        public virtual TaskStatus StartTaskStatus { get; set; }

        public virtual  TaskStatus FinalTaskStatus { get; set; }
        
        public virtual User UpdatedByUser { get; set; }

        [InverseProperty("UserHistories")]
        public User TaskCustomer { get; set; }

        public virtual Task Task { get; set; }
    }
}

