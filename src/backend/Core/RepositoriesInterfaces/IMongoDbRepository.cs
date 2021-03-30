using System.Threading.Tasks;

namespace Core.RepositoriesInterfaces
{
    public interface IMongoDbRepository<TDocument> 
        {
        Task InsertOne(TDocument model);
    }
}