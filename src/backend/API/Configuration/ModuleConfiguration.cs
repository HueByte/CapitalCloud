using System;
using System.IO;
using System.Reflection;
using System.Text;
using API.Authentication;
using AspNetCore.Identity.Mongo;
using Core.Entities;
using Core.RepositoriesInterfaces;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace API.Configuration
{
    public class ModuleConfiguration
    {
        private IServiceCollection _services;
        private IConfiguration _config;
        public ModuleConfiguration(IServiceCollection services, IConfiguration config)
        {
            _services = services;
            _config = config;
        }
        public void ConfigureDataBase()
        {
            var connectionString = _config.GetConnectionString("MongoDbTest");
            _services.AddDbClient(connectionString);
        }
        public void ConfigureSecurity()
        {
            _services.AddIdentityMongoDbProvider<ApplicationUser, ApplicationRole, string>(identity =>
             {
                 identity.Password.RequireDigit = true;
                 identity.Password.RequireUppercase = true;
                 identity.Password.RequiredLength = 6;
                 identity.Password.RequireLowercase = true;
                 identity.Password.RequireNonAlphanumeric = false;
                 identity.User.RequireUniqueEmail = true;
                 identity.SignIn.RequireConfirmedEmail = false;
             },
               mongo =>
             {
                 mongo.ConnectionString = _config.GetConnectionString("MongoDbTest");
             });


            _services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _config["JWT:Issuer"],
                    ValidAudience = _config["JWT:Audience"],
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"])),
                    ValidateIssuerSigningKey = true
                };
            });
        }
        public void ConfigureSwagger()
        {
            _services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v0.2A", new OpenApiInfo
                {
                    Version = "v0.2A",
                    Title = "Roulette Api",
                    Description = "Api for Roulette Web Applicaton",
                    Contact = new OpenApiContact
                    {
                        Name = "Hue̾Byte#3539/OldSinner#8420",
                        Email = String.Empty,
                        Url = new Uri("https://github.com/HueByte/Huelette")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under MIT",
                        Url = new Uri("https://en.wikipedia.org/wiki/MIT_License"),
                    }
                });
                var xmlFile = $"API.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
        public void ConfigureServices()
        {

            _services.AddTransient(typeof(IMongoDbRepository<>), typeof(MongoDbRepository<>));
            _services.AddScoped<LevelRepository>();
            _services.AddScoped<IUserService, UserService>();
            _services.AddScoped<IJwtAuthentication, JwtAuthentication>();
            _services.AddScoped<IUserManagmentService, UserManagmentService>();
        }

        public void ConfigureCors() => _services.AddCors(o => o.AddDefaultPolicy(builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        }));

        public void ConfigureForwardedHeaders() => _services.Configure<ForwardedHeadersOptions>(options => {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        });
    }
}