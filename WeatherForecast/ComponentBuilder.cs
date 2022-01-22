using Musement.Model;
using WeatherForecast;

namespace Musement.Business
{
    public class CityWeatherComponentBuilder : IComponentBuilder<City>
    {
        private readonly ICityService _cityService;
        private readonly IWeatherForecastService _weatherForecastService;

        public CityWeatherComponentBuilder(ICityService cityService, IWeatherForecastService weatherForecastService)
        {
            _cityService = cityService;
            _weatherForecastService = weatherForecastService;
        }

        /// <summary>
        /// Buid components for City weather forecast
        /// </summary>
        /// <returns></returns>
        public IComponentResultBuilder<City> Build()
        {
            IComponentResultBuilder<City> component = new WeatherForecastComponent(_weatherForecastService);
            component = new CityComponent(component, _cityService);
            return component;
        }
    }
}