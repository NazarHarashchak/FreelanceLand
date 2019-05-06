using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class RatingDTO
    {
       public int Id { get; set; }
        public int UserId { get; set; }
        public int RateByUser { get; set; }
        public int Mark { get; set; }
        public int UserStatusId { get; set; }
    }
}
