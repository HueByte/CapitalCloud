using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Data;
using Core.Models;
using Core.RepositoriesInterfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using Serilog;
using Serilog.Core;

namespace Infrastructure.Repositories
{
    public class MongoDbRepository<TDocument> : IMongoDbRepository<TDocument>
    {
        Logger log;
        private IMongoCollection<TDocument> collection;
        public IMongoDatabase Database { get; }
        public MongoDbRepository(MongoClient client)
        {
            Database = client.GetDatabase("RouletteTest");
            var collectionName = GetCollectionName();
            collection = Database.GetCollection<TDocument>(collectionName);
        }
        private string GetCollectionName()
        {
            return (typeof(TDocument).GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault()
                as BsonCollectionAttribute).CollectionName;
        }
        public async Task<ServiceResponse<TDocument>> InsertOne(TDocument model)
        {
            try
            {
                await collection.InsertOneAsync(model);
                Log.Information("Inserted to DB: " + model.ToJson());
                return new ServiceResponse<TDocument>()
                {
                    Data = model,
                    isSuccess = true,
                    message = "Successfully insert Entity to DB",
                    flag = 0
                };
            }

            catch (Exception x)
            {
                Log.Error("Error occurred during Insert" + x);
                return new ServiceResponse<TDocument>()
                {
                    Data = model,
                    isSuccess = false,
                    message = "SError occurred during Insert Entity",
                    flag = 1
                };
            }

        }
        public async Task InsertMany(List<TDocument> modelList)
        {
            await collection.InsertManyAsync(modelList);
        }

        public async Task DeleteById(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq("_id", id);
            await collection.DeleteOneAsync(filter);
        }

        public async Task<TDocument> GetById(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq("_id", id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<TDocument>> GetAll()
        {
            var filter = Builders<TDocument>.Filter.Empty;
            return await collection.Find(filter).ToListAsync();
        }

        public async Task Update(string id, TDocument model)
        {
            var filter = Builders<TDocument>.Filter.Eq("_id", id);
            await collection.ReplaceOneAsync(filter, model);
        }
    }
}