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
        UserAccountDTO GetUserByLogin(string login);
        UserAccountDTO Authenticate(string login, string password);
        UserAccountDTO CreateUser(string email, string login, string password);
        UserInformation GetUserInformation(int id);
        User UpdateUser(int id, [FromBody] UserInformation value);
        Task<string> CreateUserImage(ImageDTO Image);
    }
}
