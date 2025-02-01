using Domain.Models.Request;
using Domain.Models.Response;

namespace OpenWeatherMapServices
{
    public interface IOpenWeatherMapService
    {
       Task<OpenWeatherMapResponse> GetNewsApiDataAsync(OpenWeatherMapRequest request);
    }
}
