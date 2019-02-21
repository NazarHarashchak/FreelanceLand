using System.Collections.Generic;

namespace FreelanceLand.Models
{
    public class UserRoles
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public List<User> Users { get; set; }
    }
}

