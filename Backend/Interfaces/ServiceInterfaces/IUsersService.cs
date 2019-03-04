using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using FreelanceLand.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Interfaces.ServiceInterfaces
{
    public interface IUsersService
    {
        IEnumerable<UserDTO> GetAllEntities();
        UserInformation GetUserInformation(int id);
        User UpdateUser(int id, [FromBody] UserInformation value);
    }
}
