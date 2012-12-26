// --------------------------------------------------------------------------
// <copyright file="NotifyInfo.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.17">Created</change>
// </history>

namespace Fanex.Maintenance.Entity.Model
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class NotifyInfo
    {
        public string ServerIp { get; set; }

        public NotifyType Type { get; set; }

        public string Message { get; set; }

        public string OccurTime { get; set; }

        public string JobName { get; set; }
    }

    /// <summary>
    /// Notify Type
    /// </summary>
    public enum NotifyType : int
    {
        AgentLog = 1,
        Job = 2
    }
}
