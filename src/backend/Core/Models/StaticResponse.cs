namespace Core.Models
{
    public static class StaticResponse<T>
    {
        public static BasicApiResponse<T> BadResponse(T model, string message, int flag) => new BasicApiResponse<T>
        {
            Data = model,
            isSuccess = false,
            message = message,
            flag = flag
        };
         public static BasicApiResponse<T> GoodResponse(T model, string message) => new BasicApiResponse<T>
        {
            Data = model,
            isSuccess = true,
            message = message,
            flag = 0
        };



    }
}