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
        //TODO - MAKE MESSAGE FOR LOGS
        public IMongoCollection<TDocument> collection;
        public IMongoDatabase Database { get; }

        public MongoDbRepository(MongoClient client)
        {
            // TODO - Add it to config / appsettings.json
            Database = client.GetDatabase("RouletteTest");
            var collectionName = GetCollectionName();
            collection = Database.GetCollection<TDocument>(collectionName);
        }
        private string GetCollectionName()
        {
            return (typeof(TDocument).GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault()
                as BsonCollectionAttribute).CollectionName;
        }
        public async Task<BasicApiResponse<TDocument>> InsertOne(TDocument model)
        {
            try
            {
                await collection.InsertOneAsync(model);
                Log.Information("Inserted to DB: " + model.ToJson());

                return new BasicApiResponse<TDocument>()
                {
                    Data = model,
                    isSuccess = true,
                    errors = null,
                    flag = 0
                };
            }

            catch (Exception x)
            {
                Log.Error("Error occurred during Insert" + x);

                return new BasicApiResponse<TDocument>()
                {
                    Data = model,
                    isSuccess = false,
                    errors = new List<string>() { "Error occurred during Entity insertion" },
                    flag = 1
                };
            }

        }
        public async Task<BasicApiResponse<List<TDocument>>> InsertMany(List<TDocument> modelList)
        {
            try
            {
                await collection.InsertManyAsync(modelList);
                Log.Information("Added " + modelList.Count + " Entities type of " + collection.CollectionNamespace + " to database " + Database.DatabaseNamespace);

                return new BasicApiResponse<List<TDocument>>()
                {
                    Data = modelList,
                    isSuccess = true,
                    // message = "Added " + modelList.Count + " Entities to Database",
                    errors = null,
                    flag = 0
                };
            }
            catch (Exception x)
            {
                Log.Error("Error occured during Insert " + modelList.Count + " Entities type of " + collection.CollectionNamespace + " to database " + Database.DatabaseNamespace);

                return new BasicApiResponse<List<TDocument>>()
                {
                    Data = modelList,
                    isSuccess = false,
                    errors = new List<string>() { "Error ocured during adding " + modelList.Count + " entities to Database: " + x.Message },
                    flag = 1
                };
            }

        }

        public async Task<BasicApiResponse<DeleteResult>> DeleteById(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq("_id", id);
            var x = await collection.DeleteOneAsync(filter);

            if (x.IsAcknowledged) Log.Information("Delete Entities type of " + collection.CollectionNamespace + " with id " + id);
            else Log.Error("Error occured during deleting: " + x.DeletedCount);

            return new BasicApiResponse<DeleteResult>()
            {
                Data = x,
                isSuccess = x.IsAcknowledged,
                // message = "Deleted status: " + x.DeletedCount,
                errors = null,
                flag = x.IsAcknowledged ? 0 : 1
            };
        }

        public async Task<BasicApiResponse<TDocument>> GetById(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq("_id", id);
            var x = await collection.Find(filter).FirstOrDefaultAsync();
            if (x != null) Log.Information("Get Entity type of " + collection.CollectionNamespace + " with id " + id + " from " + Database.DatabaseNamespace);
            else Log.Error("Error Message");
            return new BasicApiResponse<TDocument>()
            {
                Data = x,
                isSuccess = x == null ? false : true,
                // message = x == null ? "Didnt find Entity with id" + id : "Get Entity with id" + id,
                errors = null,
                flag = x == null ? 1 : 0

            };
        }

        public async Task<BasicApiResponse<List<TDocument>>> GetAll()
        {
            var filter = Builders<TDocument>.Filter.Empty;
            var x = await collection.Find(filter).ToListAsync();
            return new BasicApiResponse<List<TDocument>>()
            {
                Data = x,
                isSuccess = x == null ? false : true,
                // wtf? 
                // message = x == null ? "Problem occured during loading" : "Entities List Loaded", 
                errors = x == null ? new List<string>() { "Problem occured during loading " } : null,
                flag = x == null ? 1 : 0

            };
        }

        public async Task<BasicApiResponse<ReplaceOneResult>> Update(string id, TDocument model)
        {
            var filter = Builders<TDocument>.Filter.Eq("_id", id);
            var x = await collection.ReplaceOneAsync(filter, model);
            if (x.IsAcknowledged)
                Log.Information("Positive");
            else
                Log.Error("Error Message");

            return new BasicApiResponse<ReplaceOneResult>()
            {
                Data = x,
                isSuccess = x.IsAcknowledged,
                errors = null,
                flag = x.IsAcknowledged ? 0 : 1
            };
        }
    }
}