// --------------------------------------------------------------------------
// <copyright file="IJobService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.5">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Interface.JobManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Maintenance.Entity.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IJobService
    {
        IJobService WithStatus(int? status);

        IJobService FromServer(string serverName);

        IJobService WithJob(string jobName);

        IJobService WithSchedule(string scheduleName);

        IJobService WithCurrentExecuteStatus(int? executeStatus);

        IJobService WithLastRunDate(DateTime? lastRunDate);

        IJobService WithNextRunDate(DateTime? nextRunDate);

        IJobService WithStartRunDate(DateTime startRunDate);

        IJobService WithEndRunDate(DateTime endRunDate);

        IEnumerable<JobSchedule> GetJobs();

        IEnumerable<JobLog> GetJobLogs();
    }
}
