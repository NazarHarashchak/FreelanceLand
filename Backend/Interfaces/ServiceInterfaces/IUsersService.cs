using Backend.DTOs;
using System.Collections.Generic;
using FreelanceLand.Models;
using Microsoft.AspNetCore.Mvc;

using Backend.Pagination;

using System.Threading.Tasks;


namespace Backend.Interfaces.ServiceInterfaces
{
    public interface IUsersService
    {
        Task<UserAccountDTO> ConfirmEmail(string confirmCode);

       // Task<IEnumerable<UserDTO>> GetAllEntities();
       

        //Task<IEnumerable<UserDTO>> GetAllEntities();
        Task<User> GetUserByLogin(string login);

        Task<UserAccountDTO> Authenticate(string login, string password);
        Task<UserAccountDTO> CreateUser(string email, string login, string password);
        Task<UserInformation> GetUserInformation(int id);
        Task<User> UpdateUser(int id, [FromBody] UserInformation value);
        Task<string> CreateUserImage(ImageDTO Image);
        Task<IEnumerable<UserRolesDTO>> GetAllRolesDtos();


        //  Task<PagedList<UserDTO>> GetUsers([FromQuery] PagingParams pagingParams);
        Task<PagedList<UserDTO>> GetUsers(TextDTO text);


        Task<UserAccountDTO> ChangePass(string login, string password);

    }
}
