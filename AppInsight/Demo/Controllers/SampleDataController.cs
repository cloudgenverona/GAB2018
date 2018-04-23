using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IActionResult RaiseError()
        {
            int number = 5;
            string s = null;

            int n = s.Length + number;

            return Ok(n);
        }

        [HttpGet("[action]")]
        public IActionResult MyMetric()
        {
            double value = new Random(Environment.TickCount).NextDouble();
            var client = new TelemetryClient();
            client.TrackMetric("myMetric", value);

            return Ok(value);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> HttpCalls()
        {
            string[] uris = new[] {"https://www.google.com", "https://global.azurebootcamp.net/"};
            var client = new HttpClient();
            string[] htmls = await Task.WhenAll(uris.Select(client.GetStringAsync));

            return Ok();
        }

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
