using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musement.Model;

namespace WeatherForecast
{
    public class WeatherForecastComponent : BaseComponentResultBuilder<City, Musement.Model.WeatherForecast, City>
    {
        private readonly IWeatherForecastService _service;
        private const string _error = "Weather tot found";
        
        public WeatherForecastComponent(IWeatherForecastService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get data from WeatherForecast service
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override async Task<IRequestResult<Musement.Model.WeatherForecast>> GetDataAsync(params City[] parameters)
        {
            return await _service.GetWeatherForecastAsync(parameters.FirstOrDefault());
        }

        /// <summary>
        /// Get request result
        /// </summary>
        /// <returns></returns>
        public override Task<IEnumerable<string>> GetRequestResultAsync()
        {
            return Task.FromResult<IEnumerable<string>>(new string[] { });
        }

        /// <summary>
        /// Get request result by City from weather forecast service
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        protected override async Task<string> GetRequestResultAsync(City city)
        {
            var data = await GetDataAsync(city);
            return !data.IsSucceed ? data.Messages.FirstOrDefault() : data.Result?.Forecast == null ? _error : data.Result.Forecast.ToString();
        }
    }
}