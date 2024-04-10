using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(ILoggerManager logger) : ControllerBase
    {
        private static readonly string[] Summaries =
            [ "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" ];

        private readonly ILoggerManager _logger = logger;

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInfo("Here is info message from Weather Forecast controller.");
            _logger.LogDebug("Here is debug message from Weather Forecast controller.");
            _logger.LogWarn("Here is warn message from Weather Forecast controller.");
            _logger.LogError("Here is an error message from Weather Forecast controller.");


            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
