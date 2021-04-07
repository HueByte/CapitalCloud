using Core.Data;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Models
{
    [BsonCollection("Level")]
    public class LevelModel
    {
        [BsonId]
        [BsonElement("lvl")]
        public int lvl { get; set; }
        [BsonElement("expToGrant")]
        public float expToGrant { get; set; }


    }
}