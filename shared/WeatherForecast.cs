using System;
using System.Linq;
using System.Collections.Generic;

namespace Shared
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        private static readonly string[] Summaries = new[]
        {
            "Cool", "Mild", "Warm"
        };

        public static List<WeatherForecast> GetWeatherForecast()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(10, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToList();
        }

        public static WeatherForecast GetSingleWeatherForecast()
        {
            var rng = new Random();
            return new WeatherForecast
            {
                Date = DateTime.Now.AddDays(1),
                TemperatureC = rng.Next(10, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            };
        }
    }
}
