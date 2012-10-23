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
    using Fanex.Maintenance.Entity.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IJobService
    {
        /// <summary>
        /// Filter by status
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        IJobService WithStatus(int? status);

        /// <summary>
        /// From server
        /// </summary>
        /// <param name="serverName"></param>
        /// <returns></returns>
        IJobService FromServer(string serverName);

        /// <summary>
        /// Filter by job name
        /// </summary>
        /// <param name="jobName"></param>
        /// <returns></returns>
        IJobService WithJob(string jobName);

        /// <summary>
        /// Filter by shedule name
        /// </summary>
        /// <param name="scheduleName"></param>
        /// <returns></returns>
        IJobService WithSchedule(string scheduleName);

        /// <summary>
        /// Filter by shedule id
        /// </summary>
        /// <param name="scheduleName"></param>
        /// <returns></returns>
        IJobService WithScheduleId(int? scheduleId);

        /// <summary>
        /// Filter by current execution status
        /// </summary>
        /// <param name="executeStatus"></param>
        /// <returns></returns>
        IJobService WithCurrentExecuteStatus(int? executeStatus);

        /// <summary>
        /// Filter by last run date
        /// </summary>
        /// <param name="lastRunDate"></param>
        /// <returns></returns>
        IJobService WithLastRunDate(DateTime? lastRunDate);

        /// <summary>
        /// Filter by next run date
        /// </summary>
        /// <param name="nextRunDate"></param>
        /// <returns></returns>
        IJobService WithNextRunDate(DateTime? nextRunDate);

        /// <summary>
        /// Filter by start run date
        /// </summary>
        /// <param name="startRunDate"></param>
        /// <returns></returns>
        IJobService WithStartRunDate(DateTime? startRunDate);

        /// <summary>
        /// Filter by end run date
        /// </summary>
        /// <param name="endRunDate"></param>
        /// <returns></returns>
        IJobService WithEndRunDate(DateTime? endRunDate);

        /// <summary>
        /// Filter by job id
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        IJobService WithJobId(string guid);

        /// <summary>
        /// Filter by step name
        /// </summary>
        /// <param name="stepName"></param>
        /// <returns></returns>
        IJobService WithStepName(string stepName);

        /// <summary>
        /// Get job
        /// </summary>
        /// <returns></returns>
        IEnumerable<JobSchedule> GetJobs();

        /// <summary>
        /// Get log of job
        /// </summary>
        /// <returns></returns>
        IEnumerable<JobLog> GetJobLogs();

        /// <summary>
        /// Start job
        /// </summary>
        /// <returns></returns>
        CommandResult StartJob();

        /// <summary>
        /// Stop job
        /// </summary>
        /// <returns></returns>
        CommandResult StopJob();
    }
}
