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
    /// <summary>
    /// Notify Type.
    /// </summary>
    public enum NotifyType : int
    {
        /// <summary>
        /// Agent log notify.
        /// </summary>
        AgentLog = 1,

        /// <summary>
        /// Job notify.
        /// </summary>
        Job = 2
    }
    
    /// <summary>
    /// Notify info.
    /// </summary>
    public class NotifyInfo
    {
        public string ServerIp { get; set; }

        public NotifyType Type { get; set; }

        public string Message { get; set; }

        public string OccurTime { get; set; }

        public string JobName { get; set; }
    }
}
