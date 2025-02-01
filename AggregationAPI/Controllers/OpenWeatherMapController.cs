using AggregationAPI.Models;
using ApiDtos;
using ApiDtos.Request;
using Application.Interface;
using Domain.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text;
using System.Web;

namespace AggregationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpenWeatherMapController : ControllerBase
    {

        private readonly ILogger<OpenWeatherMapController> _logger;
        private readonly ServicesSettings _servicesSettings;
        private readonly ICollectData _application;
        public OpenWeatherMapController(ILogger<OpenWeatherMapController> logger, IOptions<ServicesSettings> servicesSettings
            , ICollectData application
            )
        {
            _logger = logger;
            _servicesSettings = servicesSettings.Value;
            _application = application;
        }

        [HttpGet(Name = "GetWeather")]
        public async Task<IActionResult> GetWeatherAsync([FromQuery] OpenWeatherMapRequest openWeatherMapRequest)
        {
            var response = await _application.FetchWeatherDataAsync(openWeatherMapRequest);

            return Ok(response);
        }
       
    }
}
