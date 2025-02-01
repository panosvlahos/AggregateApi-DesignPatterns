using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AggregationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RabbitMqController : ControllerBase
    {
        private readonly ConnectionFactory _factory;

        public RabbitMqController()
        {
            _factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/"
            };
        }

        
        [HttpPost]
        public async Task<IActionResult> PostRabbitMqAsync()
        {
            try
            {
                await using var connection = await _factory.CreateConnectionAsync();
                await using var channel = await connection.CreateChannelAsync();

                string routingKey = "hello";

                await channel.QueueDeclareAsync(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

                string message = "Hello, RabbitMQ!";
                var body = Encoding.UTF8.GetBytes(message);

                await channel.BasicPublishAsync<BasicProperties>(
                    exchange: "",
                    routingKey: routingKey,
                    mandatory: false,
                    basicProperties: new BasicProperties(),
                    body: body,
                    cancellationToken: CancellationToken.None
                );

                return Ok("Message sent successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAndDeleteRabbitMqAsync()
        {
            try
            {
                await using var connection = await _factory.CreateConnectionAsync();
                await using var channel = await connection.CreateChannelAsync();

        
                var result = await channel.BasicGetAsync("hello", autoAck: false);

                if (result == null)
                {
                    return NotFound("No messages in the queue.");
                }

        
                string message = Encoding.UTF8.GetString(result.Body.ToArray());

        
                await channel.BasicAckAsync(result.DeliveryTag, multiple: false);

                return Ok(new { message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
