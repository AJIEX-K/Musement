using System;
using System.Collections.Generic;
using Musement.Model;

namespace Musement.UnitTest.Mock
{
    public static class WeatherForecastMock
    {
        private static ForecastDay forecastDay1009 => new ForecastDay(){Date = DateTime.Now, Day = new Day(){Condition = new Condition(){Code = 1009, Text = "Overcast" } }};
        private static ForecastDay forecastDay1063 => new ForecastDay() { Date = DateTime.Now, Day = new Day() { Condition = new Condition() { Code = 1063, Text = "Patchy rain possible" } } };
        private static ForecastDay forecastDay1075 => new ForecastDay() { Date = DateTime.Now, Day = new Day() { Condition = new Condition() { Code = 1075, Text = "Overcast" } } };

        private static IEnumerable<ForecastDay> forecastDays_2days = new List<ForecastDay>(){forecastDay1009, forecastDay1063};

        private static IEnumerable<ForecastDay> forecastDays_3days = new List<ForecastDay>() { forecastDay1009, forecastDay1063, forecastDay1075 };
        public static Model.WeatherForecast WeatherForecast57_2days => new Model.WeatherForecast(){Forecast = new Forecast(){ForecastDay = forecastDays_2days}};
        public static Model.WeatherForecast WeatherForecast57_3days => new Model.WeatherForecast() { Forecast = new Forecast() { ForecastDay = forecastDays_3days } };
    }
}