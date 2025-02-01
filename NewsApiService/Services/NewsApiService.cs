
using System.Diagnostics.Metrics;
using System.Net.Http;
using System.Runtime;
using System.Text;
using ApiDtos;
using ApiDtos.Request;
using Domain.Models.Request;
using Domain.Models.Response;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
namespace NewsApiServices
{
    public class NewsApiService : INewsApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ServicesSettings _servicesSettings;
        public NewsApiService(IOptions<ServicesSettings> servicesSettings, HttpClient httpClient)
        {
            _servicesSettings = servicesSettings.Value;
            _httpClient = httpClient;
        }
        public async Task<NewsApiResponse> GetNewsApiDataAsync(NewsApiRequest request)
        {
                //var requestUrl = QueryHelpers.AddQueryString(_servicesSettings.NewsApi.Url, queryParams);
                var requestUrl = $"{_servicesSettings.NewsApi.Url}?country={request.country}&category={request.category}&apiKey={_servicesSettings.NewsApi.ApiKey}";
                try
                {
                _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("AggregationAPI/1.0");
                
                    var response = await _httpClient.GetAsync(requestUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        
                        var content = await response.Content.ReadAsStringAsync();
                        var responseApi = JsonConvert.DeserializeObject<NewsApiResponse>(content);
                        return responseApi;
                    }
                    else
                    {
                        
                        var errorContent = await response.Content.ReadAsStringAsync();
                        
                        throw new HttpRequestException($"Error: {response.StatusCode} - {errorContent}");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error occurred while fetching data from News API: {ex.Message}");

                }
                return new NewsApiResponse();
            }
        
    }
}
