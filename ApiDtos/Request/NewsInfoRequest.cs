using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDtos.Request
{
    public class NewsInfoRequest
    {
        public string country { get; set; } = "us";
        public string category { get; set; } = "technology";
    }
}
