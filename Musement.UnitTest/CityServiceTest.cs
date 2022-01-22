using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            _cityService = Container.Resolve<ICityService>();
        }

        [TestMethod]
        public async Task GetCities_ReturnCityCollection_UnitTest()
        {
            var cities = await _cityService.GetCitiesAsync();
            Assert.IsTrue(cities.IsSucceed);
            Assert.IsTrue(cities.Result.Any());
        }

        [TestMethod]
        public async Task GetCityById_ReturnOneCity_UnitTest()
        {
            var city = await _cityService.GetCityAsync(57);
            Assert.IsTrue(city.IsSucceed);
            Assert.IsNotNull(city.Result);
            Assert.AreEqual(57,city.Result.Id);
        }

        [TestMethod]
        public async Task GetNotExistCityById_ReturnError_UnitTest()
        {
            var city = await _cityService.GetCityAsync(0);
            Assert.IsFalse(city.IsSucceed);
            Assert.IsNull(city.Result);
            Console.WriteLine(city.Messages.FirstOrDefault());
        }
    }
}
