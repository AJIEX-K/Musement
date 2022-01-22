using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musement.Model;

namespace WeatherForecast
{
    public class CityComponent : BaseComponentResultBuilder<City, IEnumerable<City>, string>
    {
        private readonly ICityService _cityService;

        public CityComponent(IComponentResultBuilder<City> component, ICityService cityService) : base(component)
        {
            _cityService = cityService;
        }

        /// <summary>
        /// Get data from service
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override async Task<IRequestResult<IEnumerable<City>>> GetDataAsync(params string[] parameters)
        {
            return await _cityService.GetCitiesAsync();
        }

        /// <summary>
        /// Get request result collection by result from service
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<string>> GetRequestResultAsync()
        {
            var data = await GetDataAsync();
            var result = new List<string>();
            if (!data.IsSucceed)
                return data.Messages;
            
            foreach (var city in data.Result)
                result.Add(await GetRequestResultAsync(city));
            return result;
        }

        /// <summary>
        /// Get request result by city
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        protected override async Task<string> GetRequestResultAsync(City city)
        {
            return $"Processed city: {city} | {await Component.GetResultAsync(city)}";
        }
    }
}