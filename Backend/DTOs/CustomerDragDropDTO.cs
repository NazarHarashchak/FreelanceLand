using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class CustomerDragDropDTO
    {
        public int TaskId { get; set; }

        public int CustomerId { get; set; }

        public string FinalStatus { get; set; }
    }
}
