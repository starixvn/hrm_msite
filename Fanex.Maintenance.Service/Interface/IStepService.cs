// --------------------------------------------------------------------------
// <copyright file="IStepService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.8">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Interface
{
    using System.Collections.Generic;
    using Fanex.Maintenance.Entity.Domain;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IStepService
    {
        /// <summary>
        /// Get step by job id
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        IEnumerable<Step> GetStepByJob(int jobId);

        /// <summary>
        /// Get step by job name
        /// </summary>
        /// <param name="jobName"></param>
        /// <returns></returns>
        IEnumerable<Step> GetStepByJob(string jobName);
    }
}
