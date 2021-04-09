using Core.Entities;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class EmailConfirmationTokenRepository : MongoDbRepository<EmailConfirmationToken>
    {
        private readonly MongoClient _client;
        public EmailConfirmationTokenRepository(MongoClient client) : base(client)
        {
            _client = client;

        }


    }
}