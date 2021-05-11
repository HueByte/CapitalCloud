namespace Core.Models
{
    public class UpdatedUser
    {
        public string UserName { get; set; }
        public double exp { get; set; }
        public string avatar_url { get; set; }
        public uint coins { get; set; }
        public bool isSuccess { get; set; }
    }
}