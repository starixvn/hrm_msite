// --------------------------------------------------------------------------
// <copyright file="Schedules.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.5">Created</change>
// </history>

namespace Fanex.Maintenance.Entity.Model
{
    using System;
    using Fanex.Maintenance.Entity.Domain;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Schedules : ISchedules
    {
        public int ScheduleUId { get; set; }

        public IOrinatingServers OrinatingServer { get; set; }

        public byte[] OwnerSid { get; set; }

        public int Enable { get; set; }

        public int FregType { get; set; }

        public int FregInterval { get; set; }

        public int FregSubdayType { get; set; }

        public int FregSubdayInterval { get; set; }

        public int FregRelativeInterval { get; set; }

        public int FregRecurrenceFactor { get; set; }

        public int ActiveStartDate { get; set; }

        public int ActiveEndDate { get; set; }

        public int ActiveStartTime { get; set; }

        public int ActiveEndTime { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime ModifedCreated { get; set; }

        public int VersionNumber { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
