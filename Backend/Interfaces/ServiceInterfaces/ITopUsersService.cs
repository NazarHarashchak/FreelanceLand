using Backend.DTOs;
using FreelanceLand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Interfaces.ServiceInterfaces
{
    public interface ITopUsersService
    {
        Task<IEnumerable<TopUserDTO>> GetTop5Users();
    }
}
