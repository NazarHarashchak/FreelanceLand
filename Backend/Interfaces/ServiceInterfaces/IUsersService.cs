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
<<<<<<< HEAD
        UserLoginDTO GetUserByLogin(string login);
        UserLoginDTO Authenticate(string login, string password);
        UserRegistrationDTO CreateUser(string email, string login, string password);
=======
        UserAccountDTO GetUserByLogin(string login);
        UserAccountDTO Authenticate(string login, string password);
        UserAccountDTO CreateUser(string email, string login, string password);
>>>>>>> 4473fed5c46f4ebea5691932450d0c6acb236151
        UserInformation GetUserInformation(int id);
        User UpdateUser(int id, [FromBody] UserInformation value);
    }
}
