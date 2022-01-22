using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherForecast
{
    public interface IComponentResultBuilder<T>
    {
        /// <summary>
        /// Get result of component
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        Task<string> GetResultAsync(T res);

        /// <summary>
        /// Get request result from component
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<string>> GetRequestResultAsync();
    }
}