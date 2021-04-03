using System;
using System.Collections.Generic;
using Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using AspNetCore.IdentityProvider.Mongo;
using Microsoft.OpenApi.Models;

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
            Console.WriteLine("iok");
            services.AddIdentityMongoDbProvider<ApplicationUser, ApplicationRole>(options =>
         {
             options.Password.RequiredLength = 0;
         }, mongoOptions =>
         {
             mongoOptions.ConnectionString = _config.GetConnectionString("MongoDbTest");
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


    }
}
