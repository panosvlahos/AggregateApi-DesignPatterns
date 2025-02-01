using AggregationAPI.Models;
using ApiDtos;
using ApiDtos.Request;
using Application.Interface;
using Domain.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SingletonDesignPatternServices;
using StatisticServices;
using System.Diagnostics;
using System.Text;
using System.Web;

namespace AggregationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AggregationController : ControllerBase
    {

        private readonly ILogger<AggregationController> _logger;
        private readonly ServicesSettings _servicesSettings;
        private readonly ICollectData _application;
        private readonly IStatisticService _requestStatisticsService;
        public AggregationController(ILogger<AggregationController> logger, IOptions<ServicesSettings> servicesSettings
            , ICollectData application, IStatisticService requestStatisticsService
            )
        {
            _logger = logger;
            _servicesSettings = servicesSettings.Value;
            _application = application;
            _requestStatisticsService = requestStatisticsService;
        }

        [HttpGet(Name = "GetDataApi")]
        public async Task<IActionResult> GetAsync([FromQuery] AggregationRequest request)
        {
            //var stopwatch = Stopwatch.StartNew();
            try
            {

                var response = await _application.DataAggregatorAsync(request);
                //stopwatch.Stop();
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);  // Return 400 Bad Request with the error message
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");  // Return 500 Internal Server Error
            }
        }
    }
}
