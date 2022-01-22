using System.Collections.Generic;

namespace WeatherForecast
{
    public interface IPrintResult<T>
    {
        void PrintResult(IEnumerable<T> results);
    }
}