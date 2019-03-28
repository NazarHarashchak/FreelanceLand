using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class TaskPageDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

        public string Date { get; set; }

        public string Deadline { get; set; }

        public string TaskCategory { get; set; }

        public string TaskStatus { get; set; }
        
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerSecondName { get; set; }

        public int ExcecutorId { get; set; }

        public string ExcecutorName { get; set; }

        public string ExcecutorSecondName { get; set; }

    }
}

