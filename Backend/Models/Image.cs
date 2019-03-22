using FreelanceLand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceLand.Models
{
    public class Image
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public byte[] Picture { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
