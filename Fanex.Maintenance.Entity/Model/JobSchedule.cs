// --------------------------------------------------------------------------
// <copyright file="JobSchedule.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.8">Created</change>
// </history>

namespace Fanex.Maintenance.Entity.Model
{
    using System;
    using Fanex.Maintenance.Entity.Domain;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class JobSchedule
    {
        public Job Job { get; set; }

        public DateTime LastRunDate { get; set; }

        public DateTime NextRunDate { get; set; }

        public int ExecuteStatus { get; set; }            
    }
}
