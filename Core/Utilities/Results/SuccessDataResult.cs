using Newtonsoft.Json;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T>:DataResult<T>
    {
        public SuccessDataResult()
        {
            
        }
        [JsonConstructor]
        public SuccessDataResult(T data,  string message) : base(data, true, message)
        {
        }

        public SuccessDataResult(T data) : base(data, true)
        {
        }
    }
}