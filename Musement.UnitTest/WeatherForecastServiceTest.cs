using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Musement.UnitTest.Mock;
using Unity;
using WeatherForecast;

namespace Musement.UnitTest
{
    /// <summary>
    /// Test class for WeatherForecastService
    /// </summary>
    [TestClass]
    public class WeatherForecastServiceTest : BaseTestClass
    {
        private IWeatherForecastService _service;
        
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            _service = Container.Resolve<IWeatherForecastService>();
        }

        [TestMethod]
        public async Task GetCityWeatherForecastByCity_ReturnCityWeatherForecast_for2days_UnitTest()
        {
            var forecast = await _service.GetWeatherForecastAsync(CityMock.City);
            Assert.IsTrue(forecast.IsSucceed);
            Assert.IsNotNull(forecast.Result);
            Assert.AreEqual(2, forecast.Result.Forecast.ForecastDay.Count(), forecast.Result.Forecast.ToString());
        }

        [TestMethod]
        public async Task GetCityWeatherForecastFor3Day_ReturnCityWeatherForecast_for3days_UnitTest()
        {
            var forecast = await _service.GetWeatherForecastAsync(CityMock.City, 3);
            Assert.IsTrue(forecast.IsSucceed);
            Assert.IsNotNull(forecast.Result);
            Assert.AreEqual(3, forecast.Result.Forecast.ForecastDay.Count(), forecast.Result.Forecast.ToString());
        }

        [TestMethod]
        public async Task GetNotExistCityWeatherForecastByCity_ReturnErrorMessage_UnitTest()
        {
            var city = CityMock.City;
            city.Latitude = "qwe";
            city.Longitude = "qwe";
            var forecast = await _service.GetWeatherForecastAsync(city);
            Assert.IsFalse(forecast.IsSucceed);
            Assert.IsNull(forecast.Result);
            Console.WriteLine(forecast.Messages.FirstOrDefault());
        }
    }
}