using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonitoringAgentMetricClient;

namespace Test
{
    class Example
    {
        static void Main(string[] args)
        {
            var telnetClient = new TelnetClient(9001, "localhost");
            for (var i = 0; i < 100; i++)
            {
                System.Threading.Thread.Sleep(2000);
                telnetClient.Report(Metric.Builder().ApplicationName("testApplication").MetricName("testMetric").TimeStamp(GetCurrentUnixTimestamp()).Value("testValue").Build());
            }
        }

        protected static long GetCurrentUnixTimestamp()
        {
            return (long)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}
