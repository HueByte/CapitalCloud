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
        private string GetCollectionName()
        {
            return (typeof(TDocument).GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault()
                as BsonCollectionAttribute).CollectionName;
        }
        public async Task InsertOne(TDocument model)
        {
            await collection.InsertOneAsync(model);
        }
        public async Task InsertMany(List<TDocument> modelList)
        {
            await collection.InsertManyAsync(modelList);
        }

        public async Task DeleteById(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq("_id",id);
            await collection.DeleteOneAsync(filter);
        }

        public async Task<TDocument> GetById(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq("_id",id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<TDocument>> GetAll()
        {
            var filter = Builders<TDocument>.Filter.Empty;
            return await collection.Find(filter).ToListAsync();
        }

        public async Task Update(string id, TDocument model)
        {
            var filter = Builders<TDocument>.Filter.Eq("_id",id);
            await collection.ReplaceOneAsync(filter,model);
        }
    }
}