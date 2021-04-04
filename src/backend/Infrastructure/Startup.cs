

using System;
using System.Text;
using AspNetCore.Identity.Mongo;
using AspNetCore.Identity.Mongo.Model;
using Core.Entities;
using Core.RepositoriesInterfaces;
using Infrastructure.Repositories;
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

    }
}
