﻿using AutoMapper;
using Backend.Hubs;
using Backend.Interfaces.ServiceInterfaces;
using Backend.MappingProfiles;
using Backend.Services;
using FreelanceLand.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;

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
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IChatRoomService, ChatRoomService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IUserTokensService, UserTokensService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<ITasksService, TasksService>();
            services.AddTransient<ITopUsersService, TopUsersService>();
            services.AddTransient<ITaskInfoService, TaskInfoService>();
            services.AddTransient<ICommentsService, CommentsService>();
            services.AddTransient<ApplicationContext, ApplicationContext>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
             
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = AuthOptions.ISSUER,
                            ValidateAudience = true,
                            ValidAudience = AuthOptions.AUDIENCE,
                            ValidateLifetime = true,
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true,
                        };
                        options.Events = new JwtBearerEvents
                        {
                            OnMessageReceived = context =>
                            {
                                var accessToken = context.Request.Query["access_token"];
                                
                                var path = context.HttpContext.Request.Path;
                                if (!string.IsNullOrEmpty(accessToken) &&
                                    ((path.StartsWithSegments("/chat")) || (path.StartsWithSegments("/notification"))))
                                {
                                    context.Token = accessToken;
                                }
                                return System.Threading.Tasks.Task.CompletedTask;
                            }
                        };
                    });

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000",
                                "https://localhost:44338",
                                "https://freelanceland.azurewebsites.net",
                                "https://freelancelandback.azurewebsites.net").AllowAnyHeader()
                            .AllowAnyMethod().AllowCredentials();
                    });
            });

            services.AddSignalR(o =>
                {
                o.EnableDetailedErrors = true;
                }
            );
            services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_0);
            services.AddMvc()
                    .AddJsonOptions(options =>
                    {
                        options.SerializerSettings.ContractResolver =
                            new CamelCasePropertyNamesContractResolver();
                    });


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
           
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chat");
                routes.MapHub<NotificationHub>("/notification");
            });

            app.UseMvcWithDefaultRoute();
            
            app.UseMvc();
        }

        public virtual IServiceCollection InitializeAutomapper(IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<TaskProfile>();
                cfg.AddProfile<UserInformationProfile>();
                cfg.AddProfile<TaskDescriptionProfile>();
                cfg.AddProfile<NotificationProfile>();
            });

            return services;
        }
    }
}