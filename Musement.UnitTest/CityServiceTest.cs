using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Musement.Business;
using Musement.Model;
using Musement.UnitTest.Mock;
using NSubstitute;
using WeatherForecast;
using Unity;

namespace Musement.UnitTest
{
    /// <summary>
    /// Test class for CityService
    /// </summary>
    [TestClass]
    public class CityServiceTest : BaseTestClass
    {
        private ICityService _cityService;
        private ICityService _mockCityService;

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            _cityService = Container.Resolve<ICityService>();
            _mockCityService = Substitute.For<ICityService>();
        }

        [TestMethod]
        public async Task GetCities_ReturnCityCollection_UnitTest()
        {
            _mockCityService.GetCitiesAsync().Returns(new RequestResult<IEnumerable<City>>(CityMock.Cities));
            var cities = await _mockCityService.GetCitiesAsync();
            Assert.IsTrue(cities.IsSucceed);
            Assert.IsTrue(cities.Result.Any());
        }

        [TestMethod]
        public async Task GetCityById_ReturnOneCity_UnitTest()
        {
            _mockCityService.GetCityAsync(Arg.Any<int>()).Returns(new RequestResult<City>(CityMock.City57));
            var city = await _mockCityService.GetCityAsync(57);
            Assert.IsTrue(city.IsSucceed);
            Assert.IsNotNull(city.Result);
            Assert.AreEqual(57,city.Result.Id);
        }

        [TestMethod]
        public async Task GetNotExistCityById_ReturnError_UnitTest()
        {
            _mockCityService.GetCityAsync(Arg.Any<int>()).Returns(new RequestResult<City>("Response status code does not indicate success: 404 (Not Found)."));
            var city = await _mockCityService.GetCityAsync(0);
            Assert.IsFalse(city.IsSucceed);
            Assert.IsNull(city.Result);
            Console.WriteLine(city.Messages.FirstOrDefault());
        }
    }
}
