using ApiDtos;
using ApiDtos.Request;
using ApiDtos.Response;
using Application.Interface;
using AutoMapper;
using Domain.Models.Request;
using Domain.Models.Response;
using Microsoft.Extensions.Options;
using NewsApiServices;
using OpenWeatherMapServices;
using TwitterServices;
using Polly;
using Polly.CircuitBreaker;
using CacheServices;
using ApiDtos.Common;

namespace Application.Services
{
    public class CollectData : ICollectData
    {
        private readonly INewsApiService _newsApi;
        private readonly IOpenWeatherMapService _weatherApi;
        private readonly ITwitterServiceService _twitterApi;
        private readonly IOptions<ServicesSettings> _servicesSettings;
        private readonly IMapper _mapper;
        private readonly IAsyncPolicy _retryPolicy;
        private readonly IAsyncPolicy _circuitBreakerPolicy;
        private readonly ICacheService _cacheService;
        private string cacheKey = $"twitter";
        public CollectData(IMapper mapper, INewsApiService newsApi, IOptions<ServicesSettings> servicesSettings, IOpenWeatherMapService weatherApi, ITwitterServiceService twitterService, ICacheService cacheService)
        {
            _mapper = mapper;
            _newsApi = newsApi;
            _servicesSettings = servicesSettings;
            _weatherApi = weatherApi;
            _twitterApi = twitterService;
            _cacheService = cacheService;

            _retryPolicy = Policy.Handle<HttpRequestException>()
                             .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(2));
            _circuitBreakerPolicy = Policy.Handle<HttpRequestException>()
                                          .CircuitBreakerAsync(3, TimeSpan.FromSeconds(30));
        }
        public async Task<NewsApiResponse> FetchNewsDataAsync(NewsApiRequest newsApiRequest)
        {

            NewsApiResponse newsApiresponse = new();
            newsApiresponse = await _newsApi.GetNewsApiDataAsync(newsApiRequest);
            return newsApiresponse;

        }
        public async Task<OpenWeatherMapResponse> FetchWeatherDataAsync(OpenWeatherMapRequest openWeatherMapRequest)
        {

            OpenWeatherMapResponse openWeatherMapResponse = new();
            openWeatherMapResponse = await _weatherApi.GetNewsApiDataAsync(openWeatherMapRequest);
            return openWeatherMapResponse;

        }

        public async Task<TwitterResponse> FetchTwitterDataAsync()
        {

            TwitterResponse twitterResponse = new();
            await _twitterApi.GetAccessDataAsync();
            twitterResponse = await _twitterApi.GetTwitterDataAsync();
            return twitterResponse;

        }
        public async Task<ApiResult<AggregationResponse>> DataAggregatorAsync(AggregationRequest request)
        {
            var collectData = new AggregationResponse();
            var tasks = new List<Task>();
            try
            {
                
                tasks.Add(Task.Run(async () =>
                {
                    collectData.News = _mapper.Map<NewsDTO>(
                        await _circuitBreakerPolicy.ExecuteAsync(() =>
                            _retryPolicy.ExecuteAsync(() =>
                                _newsApi.GetNewsApiDataAsync(_mapper.Map<NewsApiRequest>(request.newsApiRequestDTO)))));
                }));

                tasks.Add(Task.Run(async () =>
                {
                    collectData.Weather = _mapper.Map<WeatherDTO>(
                        await _circuitBreakerPolicy.ExecuteAsync(() =>
                            _retryPolicy.ExecuteAsync(() =>
                                _weatherApi.GetNewsApiDataAsync(_mapper.Map<OpenWeatherMapRequest>(request.openWeatherMapRequestDTO)))));
                }));

                tasks.Add(Task.Run(async () =>
                {
                    if (_cacheService.GetData<TwitterDTO>(cacheKey) == null)
                    {
                        collectData.Twitter = _mapper.Map<TwitterDTO>(
                            await _circuitBreakerPolicy.ExecuteAsync(() =>
                                _retryPolicy.ExecuteAsync(() => _twitterApi.GetTwitterDataAsync())));
                        _cacheService.SetData(cacheKey, collectData.Twitter, TimeSpan.FromMinutes(30));
                    }
                    else
                    {
                        collectData.Twitter = _cacheService.GetData<TwitterDTO>(cacheKey);
                    }
                }));

                await Task.WhenAll(tasks);

                return new ApiResult<AggregationResponse>
                {
                    Success = true,
                    Data = collectData
                };
            }
            catch (BrokenCircuitException)
            {
                return new ApiResult<AggregationResponse>
                {
                    Success = false,
                    ErrorMessage = "Circuit Problem",
                    Data = new AggregationResponse
                    {
                        News = new NewsDTO(),
                        Weather = new WeatherDTO(),
                        Twitter = new TwitterDTO()
                    }
                };
            }
            catch (TimeoutException)
            {
                return new ApiResult<AggregationResponse>
                {
                    Success = false,
                    ErrorMessage = "Request timed out.",
                    Data = new AggregationResponse
                    {
                        News = new NewsDTO(),
                        Weather = new WeatherDTO(),
                        Twitter = new TwitterDTO()
                    }
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<AggregationResponse>
                {
                    Success = false,
                    ErrorMessage = $"An error occurred: {ex.Message}",
                    Data = new AggregationResponse
                    {
                        News = new NewsDTO(),
                        Weather = new WeatherDTO(),
                        Twitter = new TwitterDTO()
                    }
                };
            }
        }
    }
}
