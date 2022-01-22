using System;
using System.Collections.Generic;

namespace Musement.Model
{
    public class WeatherForecast
    {
        public Forecast Forecast { get; set; }
    }

    public class Forecast
    {
        public IEnumerable<ForecastDay> ForecastDay { get; set; }

        public override string ToString()
        {
            return string.Join(" - ", ForecastDay);
        }
    }

    public class ForecastDay
    {
        public DateTime Date { get; set; }

        public Day Day { get; set; }

        public override string ToString()
        {
            return Day?.Condition?.ToString();
        }
    }

    public class Day
    {
        public Condition Condition { get; set; }
    }

    public class Condition
    {
        public string Text { get; set; }
        public int Code { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

}