using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Musement.Business;
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

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            _cityService = Container.Resolve<ICityService>();
            _service = Container.Resolve<IWeatherForecastService>();
            _printResult = Container.Resolve<IPrintResult<string>>();
        }

        [TestMethod]
        public async Task BuildCityWeatherComponent_ReturnCityWeatherForecastCollection_UnitTest()
        {
            var component = new CityWeatherComponentBuilder(_cityService, _service);
            var result = await component.Build().GetRequestResultAsync();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public async Task BuildCityWeatherComponent_PrintCityWeatherForecastCollectionResult_UnitTest()
        {
            var component = new CityWeatherComponentBuilder(_cityService, _service);
            var results = await component.Build().GetRequestResultAsync();
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
            _printResult.PrintResult(results);

        }

    }
}