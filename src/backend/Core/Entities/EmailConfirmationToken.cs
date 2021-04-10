using System;
using Core.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities
{
    [BsonCollection("EmailConfirmationToken")]
    public class EmailConfirmationToken
    {
        [BsonId]
        [BsonElement("id")]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonElement("userId")]
        public string userId { get; set; }

        [BsonElement("token")]
        public string token { get; set; }
        
        [BsonElement("expiredAt")]
        public DateTime expiredAt { get; set; }
    }
}