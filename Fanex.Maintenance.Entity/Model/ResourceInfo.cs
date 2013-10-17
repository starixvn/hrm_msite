// --------------------------------------------------------------------------
// <copyright file="ResourceInfo.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2013.1.4">Created</change>
// </history>

namespace Fanex.Maintenance.Entity.Model
{
    using System;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ResourceInfo
    {
        public float SqlCpuPercent { get; set; }

        public float SystemCpuPercent { get; set; }

        public float MemoryUsage { get; set; }

        public string ServerIp { get; set; }

        public string ServerName { get; set; }

        public float TotalMemory { get; set; }

        public DateTime EventTime { get; set; }
    }
}
