using System.Threading.Tasks;
using Core.Entities;
using Core.RepositoriesInterfaces;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class EmailConfirmationTokenRepository : MongoDbRepository<EmailConfirmationToken>, IEmailConfirmationTokenRepository
    {
        private readonly MongoClient _client;
        public EmailConfirmationTokenRepository(MongoClient client) : base(client)
        {
            _client = client;

        }
    }
}