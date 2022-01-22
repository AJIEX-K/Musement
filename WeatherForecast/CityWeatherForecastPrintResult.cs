using System;
using System.Collections.Generic;
using WeatherForecast;

namespace Musement.Business
{
    public class CityWeatherForecastPrintResult : IPrintResult<string>
    {
        /// <summary>
        /// Print results in console
        /// </summary>
        /// <param name="results"></param>
        public void PrintResult(IEnumerable<string> results)
        {
            foreach (var result in results)
                Console.WriteLine(result);
        }
    }
}