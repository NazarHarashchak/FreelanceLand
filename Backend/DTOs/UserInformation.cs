using FreelanceLand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class UserInformation
    {

        public string Name { get; set; }

        public string Sur_Name { get; set; }

        public DateTime Birth_Date { get; set; }

        public string Phone_Number { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string UserRoleName { get; set; }
    }
}
