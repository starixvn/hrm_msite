// --------------------------------------------------------------------------
// <copyright file="IJobRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.8">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Interface.JobManagement
{
    using System.Collections;
    using System.Collections.Generic;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IJobRepository
    {
        /// <summary>
        /// Get all job
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="serverName"></param>
        /// <returns></returns>
        IEnumerable<JobSchedule> GetAllJob(ICriteria criteria, string serverName);

        /// <summary>
        /// Get job log
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="serverName"></param>
        /// <returns></returns>
        IEnumerable<JobLog> GetJobLog(ICriteria criteria, string serverName);

        /// <summary>
        /// Start job
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="serverIp"></param>
        /// <returns></returns>
        Hashtable StartJob(ICriteria criteria, string serverIp);

        /// <summary>
        /// Stop job
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="serverIp"></param>
        /// <returns></returns>
        Hashtable StopJob(ICriteria criteria, string serverIp);
    }
}
