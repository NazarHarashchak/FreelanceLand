using AutoMapper;
using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using FreelanceLand.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class ImageService : IImageService
    {
        private readonly IMapper _mapper;
        private EFGenericRepository<User> userRepo;
        private EFGenericRepository<Image> imageRepo;
        private readonly ApplicationContext db;

        public ImageService(IMapper mapper, ApplicationContext context)
        {
            _mapper = mapper;
            db = context;
            userRepo = new EFGenericRepository<User>(context);
            imageRepo = new EFGenericRepository<Image>(context);
        }

        public async Task<string> CreateUserImage(ImageDTO Image)
        {
            Image im = imageRepo.Get((el) => el.UserId == Image.UserId).FirstOrDefault();
            if (im != null)
            {
                imageRepo.Remove(im);
            }

            if (Image == null) { return ("empty"); };
            byte[] fileBytes = null;
            using (var fs1 = Image.Image.OpenReadStream())
            using (var memoryStream = new MemoryStream())
            {
                await fs1.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }
            Image image = new Image();
            image.UserId = Image.UserId;
            image.FileName = Image.FileName;
            image.Picture = fileBytes;
            imageRepo.Create(image);
            return "done";
        }

        public  string GetImage(int id)
        {
            Image image = imageRepo.Get(im => im.UserId == id).FirstOrDefault();
            if (image == null)
            {
                return "empty";
            }
            else
            {
                byte[] fileBytes = image.Picture;
                string imgBase64Data = Convert.ToBase64String(fileBytes);
                string imgDataURL = string.Format("data:image/png;base64,{0}", imgBase64Data);
                ImageToClientDTO imgDTO = new ImageToClientDTO();
                imgDTO.Image = imgDataURL;
                imgDTO.FileName = image.FileName;
                return imgDataURL; }
        }

    }
}
