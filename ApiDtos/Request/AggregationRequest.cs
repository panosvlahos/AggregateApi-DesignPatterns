using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDtos.Request
{
    public class AggregationRequest
    {
        public NewsInfoRequest newsApiRequestDTO { get; set; } = new NewsInfoRequest
        {
            category = "technology",
            country="us"
        };
        
        public OpenWeatherRequest openWeatherMapRequestDTO { get; set;} = new OpenWeatherRequest
        {
          city = "NEW YORK"
        };
    }
}
