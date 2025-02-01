using AggregationAPI.Models;
using ApiDtos;
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
    public class TwitterController : ControllerBase
    {

        private readonly ILogger<TwitterController> _logger;
        private readonly ServicesSettings _servicesSettings;
        private readonly ICollectData _application;
        public TwitterController(ILogger<TwitterController> logger, IOptions<ServicesSettings> servicesSettings
            , ICollectData application
            )
        {
            _logger = logger;
            _servicesSettings = servicesSettings.Value;
            _application = application;
        }

        [HttpGet(Name = "GetTwitter")]
        public async Task<IActionResult> GetTwitterAsync()
        {
            var response = await _application.FetchTwitterDataAsync();

            return Ok(response);
        }
    }
}
