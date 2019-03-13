﻿using AutoMapper;
using Backend.Interfaces.ServiceInterfaces;
using Backend.MappingProfiles;
using Backend.Services;
using FreelanceLand.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddCors();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<ITasksService, TasksService>();
            services.AddTransient<ITopUsersService, TopUsersService>();
            services.AddTransient<ITaskCategoriesService, TaskCategoriesService>();
            services.AddTransient<ITaskInfoService, TaskInfoService>();
            services.AddTransient<ApplicationContext, ApplicationContext>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            InitializeAutomapper(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors(MyAllowSpecificOrigins);
            app.UseCors(builder => builder.AllowAnyOrigin());
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        public virtual IServiceCollection InitializeAutomapper(IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<TaskProfile>();
            });

            return services;
        }
    }
}