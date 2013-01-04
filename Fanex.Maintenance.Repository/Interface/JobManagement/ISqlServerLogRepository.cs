// --------------------------------------------------------------------------
// <copyright file="ISqlServerLogRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.10">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Interface.JobManagement
{
    using System.Collections.Generic;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ISqlServerLogRepository
    {
        /// <summary>
        /// Get sql server log by date
        /// </summary>
        /// <param name="sqlServerLogCriteria"></param>
        /// <param name="serverIp"></param>
        /// <returns></returns>
        IEnumerable<SqlServerLog> GetAllSqlServerLogOnDate(ICriteria sqlServerLogCriteria, string serverIp);

        /// <summary>
        /// Get agent log by date
        /// </summary>
        /// <param name="sqlAgentLogCriteria"></param>
        /// <param name="serverIp"></param>
        /// <returns></returns>
        IEnumerable<SqlServerLog> GetAllSqlAgentLogOnDate(ICriteria sqlAgentLogCriteria, string serverIp);


        /// <summary>
        /// Get server log or agent log
        /// </summary>
        /// <param name="sqlCriteria"></param>
        /// <param name="serverIp"></param>
        /// <param name="isAgent"></param>
        /// <returns></returns>
        IEnumerable<SqlServerLog> GetAllSqlLogOnDate(ICriteria sqlCriteria, string serverIp, bool isAgent);
    }
}
