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
    using Fanex.Maintenance.Entity.Model;

    /// <summary>
    /// Schedule service interface.
    /// </summary>
    public interface IScheduleService
    {
        IScheduleService WithServer(string serverIp);

        IScheduleService WithFreqType(int? freqType);

        IScheduleService WithFreqSubType(int? freqSubType);

        IScheduleService WithScheduleName(string scheduleName);

        IScheduleService WithSearchDateType(int? searchDateType);

        IScheduleService WithDateRange(DateTime? fromDate, DateTime? toDate);

        IEnumerable<ScheduleDetail> GetSchedules();
    }
}