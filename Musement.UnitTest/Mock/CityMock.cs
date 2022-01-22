using Musement.Model;

namespace Musement.UnitTest.Mock
{
    public static class CityMock
    {
        public static City City => 
            new City()
            {
                Id = 57,
                Name = "Amsterdam",
                Latitude = "52.374",
                Longitude = "4.9"
            };
        
    }
}