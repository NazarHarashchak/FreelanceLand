using Backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Interfaces.ServiceInterfaces
{
    public interface IUserTokensService
    {
        Task<string> CreateToken(UserAccountDTO user);
    }
}
