using Domain.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDtos.Response
{
    public class AggregationResponse
    {
        public NewsDTO News { get; set; }
        public WeatherDTO Weather { get; set; }
        public TwitterDTO Twitter { get; set; }
    }
}
