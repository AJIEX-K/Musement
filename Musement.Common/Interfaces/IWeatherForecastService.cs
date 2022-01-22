using System.Threading.Tasks;
using Musement.Model;

namespace WeatherForecast
{
    public interface IWeatherForecastService
    {
        Task<IRequestResult<Musement.Model.WeatherForecast>> GetWeatherForecastAsync(City city, int day = 2);
    }
}