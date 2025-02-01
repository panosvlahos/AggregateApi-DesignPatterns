using Newtonsoft.Json;
using System.Text;
using Domain.Models.Response;
using ApiDtos;
using Microsoft.Extensions.Options;
using Domain.Models.Request;
using System.Net;
namespace TwitterServices
{
    public class TwitterService : ITwitterServiceService
    {
        private readonly HttpClient _httpClient;
        private readonly ServicesSettings _servicesSettings;
        private string bearerToken;

        public TwitterService(IOptions<ServicesSettings> servicesSettings, HttpClient httpClient)
        {
            _servicesSettings = servicesSettings.Value;
            _httpClient = httpClient;
        }

        public async Task GetAccessDataAsync()
        {
            string consumerKey = _servicesSettings.Twitter.ClientId;
            string consumerSecret = _servicesSettings.Twitter.ClientSecret;
            string tokenUrl = _servicesSettings.Twitter.UrlAuth;

            var byteArray = Encoding.ASCII.GetBytes($"{consumerKey}:{consumerSecret}");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var requestData = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync(tokenUrl, requestData);

                string responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    TwitterResponseAuth responseAuth = JsonConvert.DeserializeObject<TwitterResponseAuth>(responseContent);
                    bearerToken = responseAuth.access_token;
                }
                else
                {
                    throw new Exception($"Error obtaining bearer token: {responseContent}");
                }
            }
            catch (Exception ex)
            {
            }

        }

        public async Task<TwitterResponse> GetTwitterDataAsync()
        {
            if (string.IsNullOrEmpty(bearerToken))
            {
                await GetAccessDataAsync();
            }

            string apiUrl = _servicesSettings.Twitter.Url;

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                string responseContent = await response.Content.ReadAsStringAsync();
                

                if (response.IsSuccessStatusCode)
                {
                    var  twitterResponse = JsonConvert.DeserializeObject<TwitterResponse>(responseContent);
                    return twitterResponse;
                }
                else if (response.StatusCode == (HttpStatusCode)429)  // Too Many Requests
                {
                    // GetData only for debugging
                    responseContent = GetData();
                    var twitterResponse = JsonConvert.DeserializeObject<TwitterResponse>(responseContent);
                    return twitterResponse;
                }
                else
                {
                    return new TwitterResponse();
                }
            }
            catch (Exception ex)
            {
                return new TwitterResponse();
            }
            
        }

        private string GetData()
        {
           return @"
{
    ""data"": [
        {
            ""id"": ""1234567890123456789"",
            ""text"": ""This is a sample tweet text."",
            ""created_at"": ""2023-12-01T12:00:00.000Z""
        },
        {
            ""id"": ""9876543210987654321"",
            ""text"": ""Another example tweet."",
            ""created_at"": ""2023-12-02T15:30:00.000Z""
        }
    ],
    ""meta"": {
        ""result_count"": 2,
        ""next_token"": ""b26v89c19zqg8o3fosaf8lf62""
    }
}";
        }
    }
}
