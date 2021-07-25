using Newtonsoft.Json;

namespace Core.Utilities.Results
{
    public class DataResult<T>:Result,IDataResult<T>
    {
        public DataResult()
        {
            
        }
        [JsonConstructor]
        public DataResult(T data,bool success,string message):base(message,success)
        {
            Data = data;
        }

        public DataResult(T data,bool success):base(success)
        {
            Data = data;
        }

        public T Data { get; }
    }
}