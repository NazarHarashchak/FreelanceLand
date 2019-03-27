using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class ExceptionLog
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public int? UserId { get; set; }
        public string Exception { get; set; }
        public string StackTrace { get; set; }
    }
}

