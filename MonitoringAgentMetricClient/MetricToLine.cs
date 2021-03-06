﻿// Copyright (c) 2015 University of Ulm
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringAgentMetricClient
{
    class MetricToLine
    {
        public String Apply(Metric metric)
        {
            Contract.Requires<ArgumentNullException>(metric != null);
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
