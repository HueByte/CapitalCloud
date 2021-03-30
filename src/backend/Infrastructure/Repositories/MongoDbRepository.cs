using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Data;
using Core.RepositoriesInterfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class MongoDbRepository<TDocument> : IMongoDbRepository<TDocument>
    {
        private IMongoCollection<TDocument> collection;
        public IMongoDatabase Database { get; }
        public MongoDbRepository(MongoClient client)
        {
            Database = client.GetDatabase("123");
            var collectionName = GetCollectionName();
            collection = Database.GetCollection<TDocument>(collectionName);
        }
        public async Task InsertOne(TDocument model)
        {
            await collection.InsertOneAsync(model);
        }

        private string GetCollectionName()
        {
            return (typeof(TDocument).GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault()
                as BsonCollectionAttribute).CollectionName;
        }
    }
}