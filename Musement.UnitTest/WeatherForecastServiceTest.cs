using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Musement.Business;
using Musement.Model;
using Musement.UnitTest.Mock;
using NSubstitute;
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
        private IWeatherForecastService _mockWeatherService;

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            _service = Container.Resolve<IWeatherForecastService>();
            _mockWeatherService = Substitute.For<IWeatherForecastService>();
        }

        [TestMethod]
        public async Task GetCityWeatherForecastByCity_ReturnCityWeatherForecast_for2days_UnitTest()
        {
            _mockWeatherService.GetWeatherForecastAsync(Arg.Any<City>()).Returns(await Task.FromResult(new RequestResult<Model.WeatherForecast>(WeatherForecastMock.WeatherForecast57_2days)));
            var forecast = await _mockWeatherService.GetWeatherForecastAsync(CityMock.City57);
            Assert.IsTrue(forecast.IsSucceed);
            Assert.IsNotNull(forecast.Result);
            Assert.AreEqual(2, forecast.Result.Forecast.ForecastDay.Count(), forecast.Result.Forecast.ToString());
        }

        [TestMethod]
        public async Task GetCityWeatherForecastFor3Day_ReturnCityWeatherForecast_for3days_UnitTest()
        {
            _mockWeatherService.GetWeatherForecastAsync(Arg.Any<City>(), 3).Returns(await Task.FromResult(new RequestResult<Model.WeatherForecast>(WeatherForecastMock.WeatherForecast57_3days)));
            var forecast = await _mockWeatherService.GetWeatherForecastAsync(CityMock.City57, 3);
            Assert.IsTrue(forecast.IsSucceed);
            Assert.IsNotNull(forecast.Result);
            Assert.AreEqual(3, forecast.Result.Forecast.ForecastDay.Count(), forecast.Result.Forecast.ToString());
        }

        [TestMethod]
        public async Task GetNotExistCityWeatherForecastByCity_ReturnErrorMessage_UnitTest()
        {
            var city = CityMock.City57;
            city.Latitude = "qwe";
            city.Longitude = "qwe";
            _mockWeatherService.GetWeatherForecastAsync(city).Returns(await Task.FromResult(new RequestResult<Model.WeatherForecast>("Response status code does not indicate success: 400 (Bad Request).")));
            var forecast = await _mockWeatherService.GetWeatherForecastAsync(city);
            Assert.IsFalse(forecast.IsSucceed);
            Assert.IsNull(forecast.Result);
            Console.WriteLine(forecast.Messages.FirstOrDefault());
        }
    }
}