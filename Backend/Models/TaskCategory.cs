using System.Collections.Generic;

namespace FreelanceLand.Models
{
    public class TaskCategory
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public List<Task> Tasks { get; set; }
    }
}

