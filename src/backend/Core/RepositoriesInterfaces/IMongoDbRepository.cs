using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using MongoDB.Driver;

namespace Core.RepositoriesInterfaces
{
    public interface IMongoDbRepository<TDocument> 
        {
        Task<ServiceResponse<TDocument>> InsertOne(TDocument model);
        Task<ServiceResponse<List<TDocument>>> InsertMany(List<TDocument> modelList);

        Task<ServiceResponse<DeleteResult>> DeleteById(string id);
        
        Task<ServiceResponse<TDocument>> GetById(string id);

        Task<ServiceResponse<List<TDocument>>> GetAll();

        Task<ServiceResponse<ReplaceOneResult>> Update(string id, TDocument model);
    }
}