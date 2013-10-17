// --------------------------------------------------------------------------
// <copyright file="IStepRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.9">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Interface.JobManagement
{
    using System.Collections.Generic;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IStepRepository
    {
        /// <summary>
        /// Get all step by job
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="serverName"></param>
        /// <returns></returns>
        IEnumerable<StepDetail> GetStepByJob(ICriteria criteria, string serverName);
    }
}
