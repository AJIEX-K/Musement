using System.Collections.Generic;
using WeatherForecast;

namespace Musement.Business
{
    public class RequestResult<T> : IRequestResult<T>
    {
        public RequestResult(T result)
        {
            IsSucceed = true;
            Result = result;
            Messages = new List<string>();
        }

        public RequestResult(string message)
        {
            IsSucceed = false;
            if(Messages == null)
                Messages = new List<string>();
            Messages.Add(message);
        }
        public T Result { get; set; }
        public bool IsSucceed { get; set; }
        public IList<string> Messages { get; set; }
    }
}