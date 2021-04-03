using System;

namespace Core.Models
{
    public class LoginResponse
    {
        public string token { get; set; }
        public string tokenType { get; set; }

        public DateTime? expiresDate { get; set; }
        
        
        
        
        
        
    }
}