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
<<<<<<< HEAD
        Task<string> CreateUserImage(ImageDTO Image);
=======
        IEnumerable<UserRolesDTO> GetAllRolesDtos();

>>>>>>> a0b846781af7bc71c28963c90c175cc181d50a2b
    }
}
