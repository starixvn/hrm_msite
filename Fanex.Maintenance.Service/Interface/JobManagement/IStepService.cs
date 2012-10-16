// --------------------------------------------------------------------------
// <copyright file="IStepService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.9">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Interface.JobManagement
{
    using System.Collections.Generic;
    using Fanex.Maintenance.Entity.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IStepService
    {
        /// <summary>
        /// From server
        /// </summary>
        /// <param name="serverName"></param>
        /// <returns></returns>
        IStepService FromServer(string serverName);

        /// <summary>
        /// Filter by job name
        /// </summary>
        /// <param name="jobName"></param>
        /// <returns></returns>
        IStepService WithJob(string jobName);

        /// <summary>
        /// Get step
        /// </summary>
        /// <returns></returns>
        IEnumerable<StepDetail> GetSteps();
    }
}
