using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using Backend.DTOs;

namespace Backend.Interfaces.ServiceInterfaces
{
    public interface IImageService
    {
        Task<string> CreateUserImage(ImageDTO Image);

        string GetImage(int id);

    }
}
