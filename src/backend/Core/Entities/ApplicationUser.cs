

using AspNetCore.Identity.Mongo.Model;

namespace Core.Entities
{
    public class ApplicationUser : MongoUser<string>
    {
        public string Avatar_Url { get; set; }
        public double exp { get; set; } = 0;
        public uint coins { get; set; }
    }
}