// -----------------------------------------------------------------------
// <copyright file="ResourceInfo.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Maintenance.Entity.Model
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
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
