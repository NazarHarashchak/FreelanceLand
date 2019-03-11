using AutoMapper;
using Backend.Interfaces.ServiceInterfaces;
using Backend.MappingProfiles;
using Backend.Services;
using FreelanceLand.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>();
            services.AddCors();

            services.AddTransient<IUserTokensService, UserTokensService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<ITasksService, TasksService>();
            services.AddTransient<ITaskCategoriesService, TaskCategoriesService>();
            services.AddMvc();

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
                    });

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader()
                            .AllowAnyMethod();

                    });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_0);

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
            app.UseAuthentication();

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