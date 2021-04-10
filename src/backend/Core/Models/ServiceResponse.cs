namespace Core.Models
{
        public class ServiceResponse<T>
    {
        public T? Data;
        public string message = "";
        public bool isSuccess;
        public int flag;

    }
}