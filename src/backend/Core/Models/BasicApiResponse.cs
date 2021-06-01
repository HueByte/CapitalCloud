using System.Collections.Generic;

namespace Core.Models
{
    public class BasicApiResponse<T>
    {
        public BasicApiResponse(T _data, List<string> _errors, bool _isSuccess, int _flag)
        {
            Data = _data;
            errors = _errors;
            isSuccess = _isSuccess;
            flag = _flag;
        }

        public BasicApiResponse() { }
        public T? Data;
        public List<string> errors;
        public bool isSuccess;
        public int flag;

    }
}