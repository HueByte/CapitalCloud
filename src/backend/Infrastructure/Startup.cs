using System;
using System.Collections.Generic;
using Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using AspNetCore.IdentityProvider.Mongo;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infrastructure.Services;
using Core.ServiceInterfaces;

namespace Infrastructure
{
    public static class Startup
    {
        public static void AddDbClient(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton(new MongoClient(connectionString));
        }

        public static void AddIdentityProvider(this IServiceCollection services, IConfiguration _config)
        {
            services.AddIdentityMongoDbProvider<ApplicationUser, ApplicationRole>(options =>
         {
             options.Password.RequiredLength = 6;
             options.Password.RequireDigit = true;
             options.Password.RequireNonAlphanumeric = false;
             options.Password.RequireUppercase = false;
             options.SignIn.RequireConfirmedEmail = false;
             options.User.RequireUniqueEmail = true;

         }, mongoOptions =>
         {
             mongoOptions.ConnectionString = _config.GetConnectionString("MongoDbTest");
         })
          .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
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

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v0.1A", new OpenApiInfo
                {
                    Version = "v0.1A",
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
            });
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJwtAuthentication, JwtAuthentication>();
        }


    }
}
