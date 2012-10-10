// --------------------------------------------------------------------------
// <copyright file="ScheduleDetail.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.10">Created</change>
// </history>

namespace Fanex.Maintenance.Entity.Model
{
    using Fanex.Maintenance.Entity.Domain;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ScheduleDetail : Schedule
    {
        public int JobCount { get; set; }

        public string ServerIp { get; set; }

        public string FreqIntervalCombine { get; set; }

        public string FreqSubIntervalCombine { get; set; }
    }
}