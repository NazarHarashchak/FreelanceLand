using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;

namespace Backend.Interfaces.ServiceInterfaces
{
    public interface IRolesUserService
    {
        IEnumerable<UserRolesDTO> GetAllRolesDtos();
    }
}
