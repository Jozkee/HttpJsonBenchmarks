using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace WeatherForecast.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        [Route("getCollection")]
        public IEnumerable<Shared.WeatherForecast> GetCollection() => Shared.WeatherForecast.GetWeatherForecast();

        [HttpGet]
        [Route("getObject")]
        public Shared.WeatherForecast GetObject() => Shared.WeatherForecast.GetSingleWeatherForecast();

        [HttpPost]
        [Route("postCollection")]
        public void PostCollection(IEnumerable<Shared.WeatherForecast> _) { }

        [HttpPost]
        [Route("postObject")]
        public void PostObject(Shared.WeatherForecast _) { }
    }
}
