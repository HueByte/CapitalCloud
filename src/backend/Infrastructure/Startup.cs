

using System;
using System.Text;
using AspNetCore.Identity.Mongo;
using AspNetCore.Identity.Mongo.Model;
using Core.Entities;
using Core.ServiceInterfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

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
            services.AddIdentityMongoDbProvider<ApplicationUser, ApplicationRole, string>(identity =>
            {
                // identity.SignIn.RequireConfirmedEmail = true;

            },
              mongo =>
            {
                mongo.ConnectionString = "mongodb://127.0.0.1:27017/identity";
            });


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
