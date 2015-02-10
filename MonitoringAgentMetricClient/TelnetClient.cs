using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringAgentMetricClient
{
    public class TelnetClient
    {
        private TcpClient _tcpClient;
        private readonly int _port;
        private readonly string _ip;
        private readonly MetricToLine _metricToLine;
        
        public TelnetClient(int port, string ip)
        {
            _port = port;
            _ip = ip;
            _metricToLine = new MetricToLine();
            _tcpClient = new TcpClient(ip,port);
        }

        private TcpClient _GetConnection()
        {
            if (_tcpClient == null || !_tcpClient.Connected)
            {
                _tcpClient = new TcpClient(_ip,_port);
            }
            return _tcpClient;
        }

        public void Report(Metric metric)
        {
            var data = System.Text.Encoding.UTF8.GetBytes(_metricToLine.Apply(metric));
            _GetConnection().GetStream().Write(data,0,data.Length);    
        }
    }
}
