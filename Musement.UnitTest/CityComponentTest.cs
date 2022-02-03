using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Musement.Business;
using Musement.Model;
using Musement.UnitTest.Mock;
using NSubstitute;
using WeatherForecast;

namespace Musement.UnitTest
{
    [TestClass]
    public class CityComponentTest : BaseTestClass
    {
        private ICityService _mockCityService;

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            _mockCityService = Substitute.For<ICityService>();
        }

        [TestMethod]
        public async Task CityComponent_GetDataAsync_ReturnCityCollection_UnitTest()
        {
            _mockCityService.GetCitiesAsync().Returns(await Task.FromResult(new RequestResult<IEnumerable<City>>(CityMock.Cities)));
            var component = new CityComponent(Substitute.For<IComponentResultBuilder<City>>(), _mockCityService);
            var result = await component.GetDataAsync();
            Assert.IsTrue(result.IsSucceed);
            Assert.IsTrue(result.Result.Any());
        }

        [TestMethod]
        public async Task CityComponent_GetRequestResultAsync_ReturnResultCollection_UnitTest()
        {
            _mockCityService.GetCitiesAsync().Returns(await Task.FromResult(new RequestResult<IEnumerable<City>>(CityMock.Cities)));
            var component = new CityComponent(Substitute.For<IComponentResultBuilder<City>>(), _mockCityService);
            var result = await component.GetRequestResultAsync();
            Assert.IsTrue(result.Any());
            Assert.IsTrue(result.All(x => !string.IsNullOrEmpty(x)));
        }
    }
}