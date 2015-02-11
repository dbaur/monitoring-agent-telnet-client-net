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
using System.CodeDom;
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
        private const int DefaultPort = 9001;
        private const string DefaultHost = "localhost";
        private TcpClient _tcpClient;
        private readonly int _port;
        private readonly string _ip;
        private readonly MetricToLine _metricToLine;
        
        public TelnetClient(int port, string ip)
        {
            _port = port;
            _ip = ip;
            _metricToLine = new MetricToLine();
        }

        public TelnetClient() : this(DefaultPort,DefaultHost)
        {
        }

        private TcpClient GetConnection()
        {
            if (_tcpClient == null || !_tcpClient.Connected)
            {
                _tcpClient = new TcpClient(_ip,_port);
            }
            return _tcpClient;
        }

        public void Report(IEnumerable<Metric> metrics)
        {
            try
            {
                var tcpClient = GetConnection();
                foreach (var data in metrics.Select(this.MetricToBytes))
                {
                    tcpClient.GetStream().Write(data,0,data.Length);
                }
            }
            finally
            {
                Close();
            }
        }

        private Byte[] MetricToBytes(Metric metric)
        {
            return System.Text.Encoding.UTF8.GetBytes(_metricToLine.Apply(metric));
        }

        public void Report(Metric metric)
        {
            try
            {
                var tcpClient = GetConnection();
                var data = this.MetricToBytes(metric);
                tcpClient.GetStream().Write(data, 0, data.Length);
            }
            finally
            {
                Close();
            }
        }

        private void Close()
        {
            _tcpClient.GetStream().Close();
            _tcpClient.Close();
        }
    }
}
