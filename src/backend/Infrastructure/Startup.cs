using System;
using System.Collections.Generic;
using Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using AspNetCore.IdentityProvider.Mongo;

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


    }
}
