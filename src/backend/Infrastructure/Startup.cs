using System;
using Microsoft.Extensions.DependencyInjection;
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
