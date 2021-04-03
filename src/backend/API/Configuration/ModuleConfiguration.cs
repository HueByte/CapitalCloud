using Core.Entities;
using Core.RepositoriesInterfaces;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            _services.AddIdentityProvider(_config);
        }
        public void ConfigureSwagger()
        {
            _services.AddSwagger();
        }
        public void ConfigureServices()
        {
            //Add services here
            _services.AddServices();
            _services.AddTransient(typeof(IMongoDbRepository<>), typeof(MongoDbRepository<>));
        }


    }
}