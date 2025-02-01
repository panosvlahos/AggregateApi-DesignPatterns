using Domain.Models.Request;
using Domain.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApiServices
{
    public interface INewsApiService
    {
        
            Task<NewsApiResponse> GetNewsApiDataAsync(NewsApiRequest request);
        
    }
}
