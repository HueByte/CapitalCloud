using System;
using System.Collections.Generic;

namespace Common.ApiResponse
{
    public class LoginResponse
    {
        public string token { get; set; }
        public string tokenType { get; set; }
        public DateTime? expiresDate { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int level { get; set; }
        public double exp { get; set; }
        public string avatar_url { get; set; }
        public List<string> Errors { get; set; }
        public bool isSuccess { get; set; } = false;

            











    }
}