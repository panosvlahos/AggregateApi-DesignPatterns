using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Request
{
    public class NewsApiRequest
    {
        public string country { get; set; } = "us";
        public string category { get; set; } = "technology";
    }
}
