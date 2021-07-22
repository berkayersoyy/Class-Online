using Newtonsoft.Json;

namespace Core.Utilities.Results
{
    public class SuccessResult:Result
    {
        [JsonConstructor]
        public SuccessResult(string message) : base(message,true)
        {
        }
        public SuccessResult():base(true){}
    }
}