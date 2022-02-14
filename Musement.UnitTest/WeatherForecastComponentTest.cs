using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Musement.Business;
using Musement.Model;
using Musement.UnitTest.Mock;
using NSubstitute;
using WeatherForecast;

namespace Musement.UnitTest
{
    [TestClass]
    public class WeatherForecastComponentTest : BaseTestClass
    {
        private IWeatherForecastService _mockWeatherService;

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            _mockWeatherService = Substitute.For<IWeatherForecastService>();
        }

        [TestMethod]
        public async Task WeatherForecastComponent_GetDataByCityAsync_ReturnCityWeatherForecast_UnitTest()
        {
            _mockWeatherService.GetWeatherForecastAsync(Arg.Any<City>()).Returns(new RequestResult<Model.WeatherForecast>(WeatherForecastMock.WeatherForecast57_2days));
            var component = new WeatherForecastComponent(_mockWeatherService);
            var result = await component.GetDataAsync(CityMock.City57);
            Assert.IsTrue(result.IsSucceed);
            Assert.IsNotNull(result.Result);
            Assert.IsNotNull(result.Result.Forecast);
            Assert.IsTrue(result.Result.Forecast.ForecastDay.Any());
        }

        [TestMethod]
        public async Task WeatherForecastComponent_GetRequestResultAsync_ReturnCityWeatherForecast_UnitTest()
        {
            var component = new WeatherForecastComponent(Substitute.For<IWeatherForecastService>());
            var result = await component.GetRequestResultAsync();
            Assert.IsTrue(result.IsNullOrEmpty());
        }
    }
}