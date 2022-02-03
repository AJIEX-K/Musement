using System.Collections.Generic;
using System.Runtime.InteropServices;
using Musement.Model;
using WeatherForecast = Musement.Model.WeatherForecast;

namespace Musement.UnitTest.Mock
{
    public static class CityMock
    {
        public static City City57 => 
            new City()
            {
                Id = 57,
                Name = "Amsterdam",
                Latitude = "52.374",
                Longitude = "4.9"
            };

        public static City City40 =>
            new City()
            {
                Id = 40,
                Name = "Paris",
                Latitude = "48.866",
                Longitude = "2.355"
            };

        public static City City2 =>
            new City()
            {
                Id = 2,
                Name = "Rome",
                Latitude = "41.898",
                Longitude = "12.483"
            };

        public static IEnumerable<City> Cities => new List<City>(){City57, City40, City2};
    }
}