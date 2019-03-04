using System;

namespace Backend.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Sur_Name { get; set; }

        public DateTime Birth_Date { get; set; }

        public string Phone_Number { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public int? UserRoleId { get; set; }
    }
}

