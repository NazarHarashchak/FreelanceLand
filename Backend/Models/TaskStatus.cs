using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreelanceLand.Models
{
    public class TaskStatus
    {
        public int Id { get; set; }

        public string Type { get; set; }
        

        public List<Task> Tasks { get; set; }

        [InverseProperty("StartTaskStatus")]
        public virtual  List<TaskHistory> StarTaskHistories { get; set; }

        [InverseProperty("FinalTaskStatus")]
        public virtual List<TaskHistory> FinalTaskHistories { get; set; }
    }
}
