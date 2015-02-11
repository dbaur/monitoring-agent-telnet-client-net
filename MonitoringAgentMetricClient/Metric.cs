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
    /// <summary>
    /// The metric class.
    /// Represents a simple metric that will be reported to the monitoring agent.
    /// </summary>
    public class Metric
    {
        private readonly string _applicationName;
        private readonly string _metricName;
        private readonly Dictionary<string, string> _tags;
        private readonly long _timeStamp;
        private readonly string _value;

        /// <summary>
        /// Constructor for the class.
        /// Use the Builder() method to get a builder for the metric class,
        /// and create metric objects using this class.
        /// </summary>
        /// <param name="applicationName">The name of the application.</param>
        /// <param name="metricName">The name of the metric you want to report.</param>
        /// <param name="timeStamp">The timestamp the measurement for the metric was taken.</param>
        /// <param name="value">The value of the measurement.</param>
        /// <param name="tags">A key => value collection of tags. Currently not reported to the agent.</param>
        Metric(string applicationName, string metricName, long timeStamp, string value,
            Dictionary<string, string> tags)
        {
            Contract.Requires<ArgumentNullException>(applicationName != null);
            Contract.Requires<ArgumentException>(applicationName.Length != 0);
            Contract.Requires<ArgumentNullException>(metricName != null);
            Contract.Requires<ArgumentException>(metricName.Length != 0);
            Contract.Requires<ArgumentException>(timeStamp > 0);
            Contract.Requires<ArgumentNullException>(value != null);
            Contract.Requires<ArgumentException>(value.Length != 0);
            Contract.Requires<ArgumentNullException>(tags != null);

            _applicationName = applicationName;
            _metricName = metricName;
            _timeStamp = timeStamp;
            _value = value;
            _tags = tags;
        }

        /// <summary>
        /// The name of the application.
        /// </summary>
        public String ApplicationName
        {
            get { return _applicationName; }
        }

        /// <summary>
        /// The name of the metric.
        /// </summary>
        public string MetricName
        {
            get { return _metricName; }
        }

        /// <summary>
        /// Unix timestamp when the measurement for the metric was taken.
        /// </summary>
        public long TimeStamp
        {
            get { return _timeStamp; }
        }

        /// <summary>
        /// The value of the measurement.
        /// </summary>
        public string Value
        {
            get { return _value; }
        }

        /// <summary>
        /// A key=>value collection of tags.
        /// </summary>
        public Dictionary<string, string> Tags
        {
            get { return _tags; }
        }

        /// <summary>
        /// Used to get a new builder for a metric.
        /// </summary>
        /// <returns>A new builder for the metric.</returns>
        public static MetricBuilder Builder()
        {
            return new MetricBuilder();
        }

        /// <summary>
        /// Builder for metric objects.
        /// </summary>
        public class MetricBuilder
        {
            private readonly Dictionary<string, string> _tags;
            private string _applicationName;
            private string _metricName;
            private long _timeStamp;
            private Object _value;

            /// <summary>
            /// Initializes the builder.
            /// </summary>
            public MetricBuilder()
            {
                _tags = new Dictionary<string, string>();
            }

            /// <summary>
            /// Add a tag to metric builder.
            /// </summary>
            /// <param name="name">The name of the tag.</param>
            /// <param name="value">The value for the tag.</param>
            /// <returns>Fluent Interface</returns>
            public MetricBuilder AddTag(string name, string value)
            {
                _tags.Add(name, value);
                return this;
            }

            /// <summary>
            /// Sets the measurement value of the metric.
            /// </summary>
            /// <param name="value">The value of the measurement.</param>
            /// <returns>Fluent Interface</returns>
            public MetricBuilder Value(Object value)
            {
                _value = value;
                return this;
            }

            /// <summary>
            /// Sets the name for the metric to the builder.
            /// </summary>
            /// <param name="metricName">The name of the metric.</param>
            /// <returns>Fluent Interface</returns>
            public MetricBuilder MetricName(string metricName)
            {
                _metricName = metricName;
                return this;
            }

            /// <summary>
            /// Sets the application name, that reports the metric.
            /// </summary>
            /// <param name="applicationName">The application name that reports the metric.</param>
            /// <returns>Fluent Interface</returns>
            public MetricBuilder ApplicationName(string applicationName)
            {
                _applicationName = applicationName;
                return this;
            }

            /// <summary>
            /// Sets the unix timestamp when the measurement value was taken.
            /// </summary>
            /// <param name="timestamp">A unix timestamp.</param>
            /// <returns>Fluent Interface</returns>
            public MetricBuilder TimeStamp(long timestamp)
            {
                _timeStamp = timestamp;
                return this;
            }

            /// <summary>
            /// Creates the metric object.
            /// </summary>
            /// <returns>A metric object.</returns>
            public Metric Build()
            {
                return new Metric(_applicationName, _metricName, _timeStamp, _value as string, _tags);
            }
        }
    }
}