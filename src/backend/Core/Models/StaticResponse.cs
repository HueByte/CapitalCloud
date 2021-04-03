namespace Core.Models
{
    public static class StaticResponse<T>
    {
        public static ServiceResponse<T> BadResponse(T model, string message, int flag) => new ServiceResponse<T>
        {
            Data = model,
            isSuccess = false,
            message = message,
            flag = flag
        };
         public static ServiceResponse<T> GoodResponse(T model, string message) => new ServiceResponse<T>
        {
            Data = model,
            isSuccess = true,
            message = message,
            flag = 0
        };



    }
}