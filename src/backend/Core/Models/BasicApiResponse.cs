namespace Core.Models
{
        public class BasicApiResponse<T>
    {
        public T? Data;
        public string message = "";
        public bool isSuccess;
        public int flag;

    }
}