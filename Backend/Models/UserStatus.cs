using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class UserStatus
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public List<Ratings> Ratings { get; set; }
        
    }
}
