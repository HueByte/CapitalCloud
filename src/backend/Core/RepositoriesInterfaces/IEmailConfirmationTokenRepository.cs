using System.Threading.Tasks;
using Core.Entities;

namespace Core.RepositoriesInterfaces
{
    public interface IEmailConfirmationTokenRepository :IMongoDbRepository<EmailConfirmationToken> { }
}