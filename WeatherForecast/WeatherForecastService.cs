using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Musement;
using Musement.Business;
using Musement.Model;

namespace WeatherForecast
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IConfiguration _config;
        private readonly IConfigurationSection _settings;

        public WeatherForecastService(IConfiguration config)
        {
            _config = config;
            _settings = _config.GetSection("weatherForecast");
        }
        
        /// <summary>
        /// Get weather forecast by City from weather api endpoint
        /// </summary>
        /// <param name="city"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public async Task<IRequestResult<Musement.Model.WeatherForecast>> GetWeatherForecastAsync(City city, int day = 2)
        {
            if (_settings == null)
                return new RequestResult<Musement.Model.WeatherForecast>("Application settings no defined");
            var endpoint = _settings["Endpoint"];
            if(endpoint == null)
                return new RequestResult<Musement.Model.WeatherForecast>("Weather api endpoint not found. Please check application settings");
            var key = _settings["key"];
            if (key == null)
                return new RequestResult<Musement.Model.WeatherForecast>("Weather api key not found. Please check application settings");
            try
            {
                return new RequestResult<Musement.Model.WeatherForecast>(await HttpHelper.Get<Musement.Model.WeatherForecast>(endpoint, $"{HttpHelper.ApiWeatherUrl(key, city.Latitude, city.Longitude, day)}"));
            }
            catch (HttpRequestException e)
            {
                return new RequestResult<Musement.Model.WeatherForecast>(e.Message);
            }
            
        }
    }
}