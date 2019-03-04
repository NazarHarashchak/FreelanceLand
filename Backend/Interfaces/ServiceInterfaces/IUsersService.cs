using Backend.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelanceLand.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Interfaces.ServiceInterfaces
{
    public interface IUsersService
    {
        IEnumerable<UserDTO> GetAllEntities();
        UserLoginDTO GetUserByLogin(string login);
        UserLoginDTO Authenticate(string login, string password);
        void CreateUser(string login, string password);
        UserInformation GetUserInformation(int id);
        User UpdateUser(int id, [FromBody] UserInformation value);
    }
}
