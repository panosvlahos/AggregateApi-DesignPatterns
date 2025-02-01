using AggregationAPI.Models;
using ApiDtos;
using ApiDtos.Request;
using Application.Interface;
using Domain.Models.Request;
using FactoryDesignPattern;
using FactoryDesignPatternServices.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SingletonDesignPatternServices;
using StatisticServices;
using StrategyDesignPatternServices;
using StrategyDesignPatternServices.Services;
using System.Diagnostics;
using System.Text;
using System.Web;

namespace AggregationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DesignPatternsController : ControllerBase
    {


        private readonly ISingletonDesignPatternService _singletonService;

        private readonly FactoryDesignPatternService _factory;
        private readonly StrategyDessignPatternContext _strategy;
        private readonly IServiceProvider _serviceProvider;
        public DesignPatternsController(ISingletonDesignPatternService singletonService, FactoryDesignPatternService factory, StrategyDessignPatternContext strategy, IServiceProvider serviceProvider
            )
        {

            _singletonService = singletonService;
            _factory = factory;
            _strategy=strategy;
            _serviceProvider = serviceProvider;
        }

        [HttpGet("singleton")]
        public async Task<IActionResult> GetSingletonAsync([FromQuery] int nuumber)
        {

            try
            {

                int newCount = _singletonService.IncrementCounter();

                return Ok(newCount);
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


        [HttpGet("factory")]
        public async Task<IActionResult> GetFactoryAsync([FromQuery] string type)
        {
            try
            {
                var notification = FactoryDesignPatternService.CreateNotification(type);
                var result = notification.Send(type);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("strategy")]
        public async Task<IActionResult> GetStrategyAsync([FromQuery] string method, [FromQuery] decimal amount)
        {
            try
            {
                IStrategyDesignPatternService paymentStrategy = method.ToLower() switch
                {
                    "creditcard" => _serviceProvider.GetRequiredService<CreditCardPayment>(),
                    "paypal" => _serviceProvider.GetRequiredService<PayPalPayment>(),
                    "banktransfer" => _serviceProvider.GetRequiredService<BankTransferPayment>(),
                    _ => throw new ArgumentException("Invalid payment method")
                };

                _strategy.SetPaymentStrategy(paymentStrategy);
                var result = _strategy.ProcessPayment(amount);


                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
