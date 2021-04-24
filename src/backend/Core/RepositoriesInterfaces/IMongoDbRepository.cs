using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using MongoDB.Driver;

namespace Core.RepositoriesInterfaces
{
    public interface IMongoDbRepository<TDocument> 
        {
        Task<BasicApiResponse<TDocument>> InsertOne(TDocument model);
        Task<BasicApiResponse<List<TDocument>>> InsertMany(List<TDocument> modelList);

        Task<BasicApiResponse<DeleteResult>> DeleteById(string id);
        
        Task<BasicApiResponse<TDocument>> GetById(string id);

        Task<BasicApiResponse<List<TDocument>>> GetAll();

        Task<BasicApiResponse<ReplaceOneResult>> Update(string id, TDocument model);
    }
}