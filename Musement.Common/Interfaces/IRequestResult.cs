using System.Collections;
using System.Collections.Generic;

namespace WeatherForecast
{
    public interface IRequestResult<T>
    {
        T Result { get; set; }

        bool IsSucceed { get; set; }

        IList<string> Messages { get; set; }
    }
}