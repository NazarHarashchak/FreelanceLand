﻿using Backend.DTOs;
using System.Threading.Tasks;

namespace Backend.Interfaces.ServiceInterfaces
{
    public interface IImageService
    {
        Task<string> CreateUserImage(ImageDTO Image);

        Task<string> GetImageAsync(int id);

    }
}
