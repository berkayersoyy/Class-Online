using Newtonsoft.Json;

namespace Core.Utilities.Results
{
    public class Result:IResult
    {
        public Result()
        {
            
        }
        [JsonConstructor]
        public Result(string message, bool success):this(success)
        {
            Message = message;
        }

        public Result(bool success)
        {
            Success = success;
        }

        public bool Success { get; }
        public string Message { get; }
    }
}