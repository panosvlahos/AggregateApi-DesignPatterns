using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticServices
{
    public class ApiRequestStats
    {
        public int TotalRequests { get; set; }
        public double TotalResponseTime { get; set; }
        public int FastRequests { get; set; }
        public int AverageRequests { get; set; }
        public int SlowRequests { get; set; }
    }

    public interface IStatisticService
    {
        void TrackRequest(double responseTime);
        RequestStatisticsDto GetStatistics();
    }

    public class StatisticService : IStatisticService
    {
        private ApiRequestStats _stats = new ApiRequestStats();

        public void TrackRequest(double responseTime)
        {
            _stats.TotalRequests++;
            _stats.TotalResponseTime += responseTime;

            if (responseTime < 100)
                _stats.FastRequests++;
            else if (responseTime >= 100 && responseTime <= 200)
                _stats.AverageRequests++;
            else
                _stats.SlowRequests++;
        }

        public RequestStatisticsDto GetStatistics()
        {
            double avgResponseTime = _stats.TotalRequests > 0 ? _stats.TotalResponseTime / _stats.TotalRequests : 0;
            return new RequestStatisticsDto
            {
                TotalRequests = _stats.TotalRequests,
                AverageResponseTime = avgResponseTime,
                FastRequests = _stats.FastRequests,
                AverageRequests = _stats.AverageRequests,
                SlowRequests = _stats.SlowRequests
            };
        }
    }

    public class RequestStatisticsDto
    {
        public int TotalRequests { get; set; }
        public double AverageResponseTime { get; set; }
        public int FastRequests { get; set; }
        public int AverageRequests { get; set; }
        public int SlowRequests { get; set; }
    }

}
