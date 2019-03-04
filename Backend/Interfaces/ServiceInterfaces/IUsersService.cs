﻿using Backend.DTOs;
using System.Collections.Generic;

namespace Backend.Interfaces.ServiceInterfaces
{
    public interface IUsersService
    {
        IEnumerable<UserDTO> GetAllEntities();
    }
}
