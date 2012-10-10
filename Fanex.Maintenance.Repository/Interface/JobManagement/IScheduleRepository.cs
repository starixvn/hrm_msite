// --------------------------------------------------------------------------
// <copyright file="IScheduleRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.8">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Interface.JobManagement
{
    using System.Collections.Generic;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IScheduleRepository
    {
        IEnumerable<ScheduleDetail> GetAllSchedule(ICriteria scheduleCriteria, string serverName);
    }
}