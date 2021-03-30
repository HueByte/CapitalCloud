using System.Linq;
using System.Threading.Tasks;
using Core.Data;
using Core.RepositoriesInterfaces;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class MongoDbRepository<T> : IMongoDbRepository<T>
    {
        public IMongoDatabase Database { get; }
        public MongoDbRepository(IMongoClient client)
        {
            Database = client.GetDatabase("admin");
        }
        public async Task InsertOne(T model)
        {
            var collectionName = GetCollectionName();
            var collection = Database.GetCollection<T>(collectionName);
            await collection.InsertOneAsync(model);
        }

        private string GetCollectionName()
        {
            return (typeof(T).GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault()
                as BsonCollectionAttribute).CollectionName;
        }
    }
}