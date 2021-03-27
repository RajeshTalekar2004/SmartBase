using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartBase.BusinessLayer.Controllers
{
    /// <summary>
    /// Weather Controller responsible for GET/POST for managing weather
    /// </summary>
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// This GET method return weather
        /// </summary>
        /// <returns>Array of weather forcast</returns>
        [HttpGet]
        public ServiceResponseModel<WeatherForecast[]> Get()
        {
            ServiceResponseModel<WeatherForecast[]> serviceResponse = new ServiceResponseModel<WeatherForecast[]>();
            var rng = new Random();
            var value = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                        {
                            Date = DateTime.Now.AddDays(index),
                            TemperatureC = rng.Next(-20, 55),
                            Summary = Summaries[rng.Next(Summaries.Length)]
                        })
                        .ToArray();

            serviceResponse.Data = value;
            return serviceResponse;
        }
    }
}
