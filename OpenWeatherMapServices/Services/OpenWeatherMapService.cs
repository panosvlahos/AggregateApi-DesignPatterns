using ApiDtos;
using ApiDtos.Request;
using Domain.Models.Request;
using Domain.Models.Response;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace OpenWeatherMapServices
{
    public class OpenWeatherMapService : IOpenWeatherMapService
    {
        private readonly HttpClient _httpClient;
        private readonly ServicesSettings _servicesSettings;
        public OpenWeatherMapService(IOptions<ServicesSettings> servicesSettings, HttpClient httpClient)
        {
            _servicesSettings = servicesSettings.Value;
            _httpClient = httpClient;
        }
        public async Task<OpenWeatherMapResponse> GetNewsApiDataAsync(OpenWeatherMapRequest request)
        {
            var requestUrl = $"{_servicesSettings.OpenWeatherApi.Url}?q={request.city}&appid={_servicesSettings.OpenWeatherApi.ApiKey}";
            try
            {


                var response = await _httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {

                    var content = await response.Content.ReadAsStringAsync();
                    var responseWeather = JsonConvert.DeserializeObject<OpenWeatherMapResponse>(content);
                    return responseWeather;
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
                return new OpenWeatherMapResponse();
            }
            return new OpenWeatherMapResponse();
        }
            
    }
}
