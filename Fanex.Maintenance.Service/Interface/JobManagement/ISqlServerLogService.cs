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
    /// SQL Server log service interface.
    /// </summary>
    public interface ISqlServerLogService
    {
        ISqlServerLogService FromDate(DateTime from);

        ISqlServerLogService ToDate(DateTime to);

        ISqlServerLogService WithServer(string serverIp);

        ISqlServerLogService WithErrorLevel(int? errorLevel);

        IEnumerable<SqlServerLog> GetSqlServerLogs(int logType = 1);
    }
}
