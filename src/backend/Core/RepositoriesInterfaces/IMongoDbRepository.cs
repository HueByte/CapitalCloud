using System.Threading.Tasks;

namespace Core.RepositoriesInterfaces
{
    public interface IMongoDbRepository<T> 
        {
        Task InsertOne(T model);
    }
}