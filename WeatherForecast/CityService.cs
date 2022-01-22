using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Musement;
using Musement.Business;
using Musement.Model;

namespace WeatherForecast
{
    public class CityService : ICityService
    {
        private readonly IConfiguration _config;
        private readonly IConfigurationSection _settings;
        private readonly string _endPoint;

        public CityService(IConfiguration config)
        {
            _config = config;
            _settings = _config.GetSection("musement");
            if (_settings != null)
                _endPoint = _settings["Endpoint"];
        }

        /// <summary>
        /// Get cities from musement endpoint
        /// </summary>
        /// <returns></returns>
        public async Task<IRequestResult<IEnumerable<City>>> GetCitiesAsync()
        {
            if (_endPoint == null)
                return new RequestResult<IEnumerable<City>>("Musement endpoint not found");
            try
            {
                return new RequestResult<IEnumerable<City>>(await HttpHelper.Get<List<City>>(_endPoint, $"cities"));
            }
            catch (HttpRequestException e)
            {
               return new RequestResult<IEnumerable<City>>(e.Message);
            }
            
        }

        /// <summary>
        /// Get city by cityId from musement endpoint
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public async Task<IRequestResult<City>> GetCityAsync(int cityId)
        {
            if (_endPoint == null)
                return new RequestResult<City>("Musement api endpoint not found");
            try
            {
                return new RequestResult<City>(await HttpHelper.Get<City>(_endPoint, $"{HttpHelper.CityUrl(cityId)}"));
            }
            catch (HttpRequestException e)
            {
                return new RequestResult<City>(e.Message);
            }
            
        }
    }
}
