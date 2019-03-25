using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class UserAccountDTO
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public bool EmailConfirmed { get; set; }

        public string ConfirmCode { get; set; }
    }
}
