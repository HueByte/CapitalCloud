using System.Collections.Generic;

namespace Common.lib.ApiResponse
{
    public class RegisterResponse
    {
        public bool isSuccess { get; set; } = false;

        public List<string> Errors { get; set; }      
        
        
    }
}