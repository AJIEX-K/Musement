using System.Collections.Generic;
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
    /// Test class for CityWeatherComponentBuilder
    /// </summary>
    [TestClass]
    public class CityWeatherComponentBuilderTest : BaseTestClass
    {
        private ICityService _cityService;
        private IWeatherForecastService _service;
        private IPrintResult<string> _printResult;

        private ICityService _mockCityService;
        private IWeatherForecastService _mockWeatherService;

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            _cityService = Container.Resolve<ICityService>();
            _service = Container.Resolve<IWeatherForecastService>();
            _printResult = Container.Resolve<IPrintResult<string>>();

            _mockCityService = Substitute.For<ICityService>();
            _mockWeatherService = Substitute.For<IWeatherForecastService>();
            _mockCityService.GetCitiesAsync().Returns(new RequestResult<IEnumerable<City>>(CityMock.Cities));
            _mockWeatherService.GetWeatherForecastAsync(Arg.Any<City>()).Returns(new RequestResult<Model.WeatherForecast>(WeatherForecastMock.WeatherForecast57_2days));
        }

        [TestMethod]
        public async Task BuildCityWeatherComponent_ReturnCityWeatherForecastCollection_UnitTest()
        {
            var component = new CityWeatherComponentBuilder(_mockCityService, _mockWeatherService);
            var result = await component.Build().GetRequestResultAsync();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public async Task BuildCityWeatherComponent_PrintCityWeatherForecastCollectionResult_UnitTest()
        {
            var component = new CityWeatherComponentBuilder(_mockCityService, _mockWeatherService);
            var results = await component.Build().GetRequestResultAsync();
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            _printResult.PrintResult(results);

        }

    }
}