using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class TaskDescription
    {
        public int Id { get; set; }

        public int Price { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string DateAdded { get; set; }

        public string Deadline { get; set; }
    }
}
