using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.RepositoriesInterfaces
{
    public interface IMongoDbRepository<TDocument> 
        {
        Task InsertOne(TDocument model);
        Task InsertMany(List<TDocument> modelList);

        Task DeleteById(string id);
        
        Task<TDocument> GetById(string id);

        Task<List<TDocument>> GetAll();

        Task Update(string id, TDocument model);
    }
}