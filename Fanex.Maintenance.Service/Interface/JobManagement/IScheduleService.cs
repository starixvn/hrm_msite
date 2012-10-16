// --------------------------------------------------------------------------
// <copyright file="IScheduleService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.8">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Interface.JobManagement
{
    using System;
    using System.Collections.Generic;
    using Fanex.Maintenance.Entity.Domain;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IScheduleService
    {
        /// <summary>
        /// From server
        /// </summary>
        /// <param name="serverIp"></param>
        /// <returns></returns>
        IScheduleService WithServer(string serverIp);

        /// <summary>
        /// Filter by freq
        /// </summary>
        /// <param name="freqType"></param>
        /// <returns></returns>
        IScheduleService WithFreqType(int? freqType);

        /// <summary>
        /// Filter by freg sub
        /// </summary>
        /// <param name="freqSubType"></param>
        /// <returns></returns>
        IScheduleService WithFreqSubType(int? freqSubType);

        /// <summary>
        /// Filter by schedule name
        /// </summary>
        /// <param name="scheduleName"></param>
        /// <returns></returns>
        IScheduleService WithScheduleName(string scheduleName);

        /// <summary>
        /// Filter by date time type
        /// </summary>
        /// <param name="searchDateType"></param>
        /// <returns></returns>
        IScheduleService WithSearchDateType(int? searchDateType);

        /// <summary>
        /// Filter by date range
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        IScheduleService WithDateRange(DateTime? fromDate, DateTime? toDate);

        /// <summary>
        /// Get schedule
        /// </summary>
        /// <returns></returns>
        IEnumerable<Schedule> GetSchedules();
    }
}