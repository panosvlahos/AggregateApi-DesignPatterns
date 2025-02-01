using ApiDtos.Common;
using ApiDtos.Request;
using ApiDtos.Response;
using Domain.Models.Request;
using Domain.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public  interface ICollectData
    {
        Task<NewsApiResponse> FetchNewsDataAsync(NewsApiRequest newsApiRequest);
        Task<OpenWeatherMapResponse> FetchWeatherDataAsync(OpenWeatherMapRequest openWeatherMapRequest);
        Task<TwitterResponse> FetchTwitterDataAsync();
        Task<ApiResult<AggregationResponse>> DataAggregatorAsync(AggregationRequest request);
    }
}
