// --------------------------------------------------------------------------
// <copyright file="ScheduleParamForTask.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.10">Created</change>
// </history>

namespace Fanex.Maintenance.Entity.Model
{
    /// <summary>
    /// Schedule parameter for task.
    /// </summary>
    public class ScheduleParamForTask
    {
        public string ScheduleName { get; set; }

        public string ServerIp { get; set; }
    }
}