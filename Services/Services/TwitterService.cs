using Domain.Models;
using ApiDtos;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenService.Interfaces;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
namespace TokenService.Services
{
    

    public class TwitterService : ITwitterServiceService
    {

        private readonly ServicesSettings _servicesSettings;

        // Inject IOptions<ServicesSettings> to access the configuration
        public TwitterService(IOptions<ServicesSettings> options)
        {
            _servicesSettings = options.Value;
        }


        public async Task<string> GetAccessTokenAsync()
        {
            string consumerKey = "8JlccUHMxVleGiPSx3hAiRwOM";
            string consumerSecret = "Z5YqTgDzXmZ77rofwJmrkdhNR2jdKn7IlJNxbVBzTP2Ib9nGea";
            string tokenUrl = "https://api.twitter.com/oauth2/token";

            using (HttpClient client = new HttpClient())
            {
                
                var byteArray = Encoding.ASCII.GetBytes($"{consumerKey}:{consumerSecret}");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                
                var requestData = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

                
                HttpResponseMessage response = await client.PostAsync(tokenUrl, requestData);

                
                string responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    

                    TwitterResponseAuth responseAuth = JsonConvert.DeserializeObject<TwitterResponseAuth>(responseContent);
                    return responseAuth.access_token;
                }

                return responseContent;
            }
        }
    }

}
