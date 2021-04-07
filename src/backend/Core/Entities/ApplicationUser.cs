

using AspNetCore.Identity.Mongo.Model;

namespace Core.Entities
{
    public class ApplicationUser : MongoUser<string>
    {
        public string Avatar_Url { get; set; }
        public int lvl { get; set; } = 1;

        public double exp { get; set; } = 10;
        
        


        
        
    }
}