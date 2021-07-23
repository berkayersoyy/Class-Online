﻿namespace Core.Utilities.Results
{
    public class DataResult<T>:Result,IDataResult<T>
    {
        public DataResult()
        {
            
        }
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