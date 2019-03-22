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
        Task<IEnumerable<UserDTO>> GetAllEntities();
        Task<UserAccountDTO> GetUserByLogin(string login);
        Task<UserAccountDTO> Authenticate(string login, string password);
        Task<UserAccountDTO> CreateUser(string email, string login, string password);
        Task<UserInformation> GetUserInformation(int id);
        Task<User> UpdateUser(int id, [FromBody] UserInformation value);
        Task<IEnumerable<UserRolesDTO>> GetAllRolesDtos();
    }
}
