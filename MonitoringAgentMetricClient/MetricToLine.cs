using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringAgentMetricClient
{
    class MetricToLine
    {
        public String Apply(Metric metric)
        {
            // TODO: implement tags
            var sb = new StringBuilder();
            sb.Append(metric.ApplicationName).Append(" ");
            sb.Append(metric.MetricName).Append(" ");
            sb.Append(metric.Value).Append(" ");
            sb.Append(metric.TimeStamp);
            sb.Append("\n");
            return sb.ToString();
        }
    }
}
