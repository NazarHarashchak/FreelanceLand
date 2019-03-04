using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using FreelanceLand.Models;

namespace Backend.Interfaces.ServiceInterfaces
{
    public interface IUsersService
    {
        IEnumerable<UserDTO> GetAllEntities();
        UserLoginDTO GetUserByLogin(string login);
        UserLoginDTO Authenticate(string login, string password);
        void CreateUser(string login, string password);
    }
}
