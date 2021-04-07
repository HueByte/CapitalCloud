using Core.Models;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class LevelRepository : MongoDbRepository<LevelModel>
    {
        public LevelRepository(MongoClient client) : base(client)
        {
            
        }
        
    }
}