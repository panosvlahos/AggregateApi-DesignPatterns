
using AggregationAPI.Controllers;
using ApiDtos;
using ApiDtos.Common;
using ApiDtos.Request;
using ApiDtos.Response;
using Application.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using StatisticServices;

namespace AggregationAPI.Tests.Controllers
{
    public class AggregationControllerTests
    {
        private readonly Mock<ILogger<AggregationController>> _mockLogger;
        private readonly Mock<IOptions<ServicesSettings>> _mockOptions;
        private readonly Mock<ICollectData> _mockApplication;
        private readonly Mock<IStatisticService> _mockStatisticsService;

        private readonly AggregationController _controller;

        public AggregationControllerTests()
        {
            _mockLogger = new Mock<ILogger<AggregationController>>();
            _mockOptions = new Mock<IOptions<ServicesSettings>>();
            _mockApplication = new Mock<ICollectData>();
            _mockStatisticsService = new Mock<IStatisticService>();

            // Set up mock configuration for ServicesSettings
            var mockSettings = new ServicesSettings();
            _mockOptions.Setup(o => o.Value).Returns(mockSettings);

            _controller = new AggregationController(
                _mockLogger.Object,
                _mockOptions.Object,
                _mockApplication.Object,
                _mockStatisticsService.Object
            );
        }


        [Fact]
        public async Task GetAsync_ShouldReturnOk_WhenValidRequest()
        {
            // Arrange
            var request = new AggregationRequest
            {
                newsApiRequestDTO = new NewsInfoRequest { category = "us" },
                openWeatherMapRequestDTO = new OpenWeatherRequest()
            };

            var mockResponse = new ApiResult<AggregationResponse>
            {
                Success = true, // Assuming this is a successful response
                Data = new AggregationResponse
                {
                    News = new NewsDTO { Status = "1" },
                    Weather = new WeatherDTO { name = "new york" },
                    Twitter = new TwitterDTO
                    {
                        Data = new List<TweetData>
            {
                new TweetData { Id = "1", Text = "Tweet1", CreatedAt = DateTime.Now, AuthorId = "user1" },
                new TweetData { Id = "2", Text = "Tweet2", CreatedAt = DateTime.Now, AuthorId = "user2" }
            }
                    }
                }
            };
            _mockApplication
                .Setup(a => a.DataAggregatorAsync(request))
                .ReturnsAsync(mockResponse); // Mocking the response

            // Act
            var result = await _controller.GetAsync(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // Verify result type
            Assert.Equal(200, okResult.StatusCode); // Verify HTTP status code
            Assert.Equal(mockResponse, okResult.Value); // Verify returned value
        }



        [Fact]
        public async Task GetAsync_ShouldReturnBadRequest_WhenArgumentException()
        {
            // Arrange
            var request = new AggregationRequest();

            _mockApplication
                .Setup(a => a.DataAggregatorAsync(request))
                .ThrowsAsync(new ArgumentException("Invalid request"));

            // Act
            var result = await _controller.GetAsync(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal("Invalid request", badRequestResult.Value);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnInternalServerError_WhenUnhandledException()
        {
            // Arrange
            var request = new AggregationRequest();

            _mockApplication
                .Setup(a => a.DataAggregatorAsync(request))
                .ThrowsAsync(new Exception("Unexpected error"));

            // Act
            var result = await _controller.GetAsync(request);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Internal Server Error", statusCodeResult.Value);
        }
    }
}
