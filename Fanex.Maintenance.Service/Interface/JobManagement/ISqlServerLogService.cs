// --------------------------------------------------------------------------
// <copyright file="ISqlServerLogService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.16">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Interface.JobManagement
{
    using System;
    using System.Collections.Generic;
    using Fanex.Maintenance.Entity.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ISqlServerLogService
    {
        /// <summary>
        /// Filter from date
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        ISqlServerLogService FromDate(DateTime from);

        /// <summary>
        /// Filter to date
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        ISqlServerLogService ToDate(DateTime to);

        /// <summary>
        /// From server
        /// </summary>
        /// <param name="serverIp"></param>
        /// <returns></returns>
        ISqlServerLogService WithServer(string serverIp);

        /// <summary>
        /// Filter by error level
        /// </summary>
        /// <param name="errorLevel"></param>
        /// <returns></returns>
        ISqlServerLogService WithErrorLevel(int? errorLevel);

        /// <summary>
        /// Get sql server log by type
        /// </summary>
        /// <param name="logType"></param>
        /// <returns></returns>
        IEnumerable<SqlServerLog> GetSqlServerLogs(int logType = 1);
    }
}
