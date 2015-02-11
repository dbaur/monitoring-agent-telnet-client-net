// Copyright (c) 2015 University of Ulm
// 
// See the NOTICE file distributed with this work for additional information
// regarding copyright ownership.  Licensed under the Apache License, Version 2.0 (the
// "License"); you may not use this file except in compliance
// with the License.  You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, either express or implied.  See the License for the
// specific language governing permissions and limitations
// under the License.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonitoringAgentMetricClient;

namespace Test
{
    /// <summary>
    /// An example for the usage of the telnet client.
    /// 
    /// The first part reports 20 single metric, the second part reports those
    /// 20 metrics at once.
    /// </summary>
    class Example
    {
        static void Main(string[] args)
        {
            var telnetClient = new TelnetClient(9001, "localhost");
            var metricList = new HashSet<Metric>();            
            // sends a single metric to the monitoring agent (one tcp connection per metric).
            for (var i = 0; i < 20; i++)
            {
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("Reporting Metric to monitoring agent.");
                var metric =
                    Metric.Builder()
                        .ApplicationName("testApplication")
                        .MetricName("testMetric")
                        .TimeStamp(GetCurrentUnixTimestamp())
                        .Value("testValue")
                        .Build();
                metricList.Add(metric);
                telnetClient.Report(metric);
            }
            // sends multiple metrics to the monitoring agent (one tcp connection for all metrics)
            Console.WriteLine("Sending "+metricList.Count+" metrics to monitoring agent.");
            telnetClient.Report(metricList);

            Console.WriteLine("Finished sending metrics. Closing application in 10 seconds.");
            System.Threading.Thread.Sleep(10 * 1000);
        }

        protected static long GetCurrentUnixTimestamp()
        {
            return (long)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}
