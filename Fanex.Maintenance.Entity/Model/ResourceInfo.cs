// -----------------------------------------------------------------------
// <copyright file="ResourceInfo.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Maintenance.Entity.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ResourceInfo
    {
        public float CpuPercent { get; set; }
        public float MemoryUsage { get; set; }
        public string ServerIp { get; set; }
        public float TotalMemory { get; set; }
    }
}
