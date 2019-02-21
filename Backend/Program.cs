using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using FreelanceLand.Models;

namespace Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            EFGenericRepository<User> userRepo = new EFGenericRepository<User>(new ApplicationContext());
            userRepo.Create(new User { Name = "User"});
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
