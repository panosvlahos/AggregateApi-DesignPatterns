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
    public class NewsApiController : ControllerBase
    {

        private readonly ILogger<NewsApiController> _logger;
        private readonly ServicesSettings _servicesSettings;
        private readonly ICollectData _application;
        public NewsApiController(ILogger<NewsApiController> logger, IOptions<ServicesSettings> servicesSettings
            , ICollectData application
            )
        {
            _logger = logger;
            _servicesSettings = servicesSettings.Value;
            _application = application;
        }

        [HttpGet(Name = "GetDataNewsApi")]
        public async Task<IActionResult> GetAsync([FromQuery] NewsApiRequest newsApiRequest)
        {
            var response=await _application.FetchNewsDataAsync(newsApiRequest);
        
            return Ok(response);
        }
    }
}
