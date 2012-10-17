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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class NotifyInfo
    {
        public string ServerIp { get; set; }
        public NotifyType Type { get; set; }
        public string Message { get; set; }
        public DateTime OccurTime { get; set; }
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
