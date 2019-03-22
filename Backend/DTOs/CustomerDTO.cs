using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Sur_Name { get; set; }
    }

    public class ExcecutorDTO
    {
        public int Id { get; set; }

        public int ExcecutorId { get; set; }

        public int TaskId { get; set; }
    }
}
