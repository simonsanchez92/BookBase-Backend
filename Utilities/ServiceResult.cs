namespace BookBase.Utilities
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
        public T Data { get; set; }


        public ServiceResult(bool success, List<string> messages, T data = default)
        {
            Success = success;
            Messages = messages;
            Data = data;
        }



        public static ServiceResult<T> SuccessResult(T data, string message = "") {

            return new ServiceResult<T>(true, new List<string> { message }, data);
        }


        public static ServiceResult<T> FailureResult(List<string> messages)
        {
            return new ServiceResult<T>(false, messages);

        }
    }
}
