using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Musement.Model;

namespace WeatherForecast
{
    public interface ICityService
    {
        /// <summary>
        /// Get cities collection
        /// </summary>
        /// <returns></returns>
        Task<IRequestResult<IEnumerable<City>>> GetCitiesAsync();

        /// <summary>
        /// Get city by cityId
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        Task<IRequestResult<City>> GetCityAsync(int cityId);
    }
}