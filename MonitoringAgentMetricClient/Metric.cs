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
using System.Diagnostics.Contracts;

namespace MonitoringAgentMetricClient
{
    public class Metric
    {
        private readonly string _applicationName;
        private readonly string _metricName;
        private readonly Dictionary<string, string> _tags;
        private readonly long _timeStamp;
        private readonly string _value;

        public Metric(string applicationName, string metricName, long timeStamp, string value,
            Dictionary<string, string> tags)
        {
            Contract.Requires(applicationName != null);
            Contract.Requires(applicationName.Length != 0);
            Contract.Requires(metricName != null);
            Contract.Requires(metricName.Length != 0);
            Contract.Requires(timeStamp > 0);
            Contract.Requires(value != null);
            Contract.Requires(value.Length != 0);
            Contract.Requires(tags != null);

            _applicationName = applicationName;
            _metricName = metricName;
            _timeStamp = timeStamp;
            _value = value;
            _tags = tags;
        }

        public String ApplicationName
        {
            get { return _applicationName; }
        }

        public string MetricName
        {
            get { return _metricName; }
        }

        public long TimeStamp
        {
            get { return _timeStamp; }
        }

        public string Value
        {
            get { return _value; }
        }

        public Dictionary<string, string> Tags
        {
            get { return _tags; }
        }

        public static MetricBuilder Builder()
        {
            return new MetricBuilder();
        }

        public class MetricBuilder
        {
            private readonly Dictionary<string, string> _tags;
            private string _applicationName;
            private string _metricName;
            private long _timeStamp;
            private Object _value;

            public MetricBuilder()
            {
                _tags = new Dictionary<string, string>();
            }

            public MetricBuilder AddTag(string name, string value)
            {
                _tags.Add(name, value);
                return this;
            }

            public MetricBuilder Value(Object value)
            {
                _value = value;
                return this;
            }

            public MetricBuilder MetricName(string metricName)
            {
                _metricName = metricName;
                return this;
            }

            public MetricBuilder ApplicationName(string applicationName)
            {
                _applicationName = applicationName;
                return this;
            }

            public MetricBuilder TimeStamp(long timestamp)
            {
                _timeStamp = timestamp;
                return this;
            }

            public Metric Build()
            {
                return new Metric(_applicationName, _metricName, _timeStamp, _value as string, _tags);
            }
        }
    }
}