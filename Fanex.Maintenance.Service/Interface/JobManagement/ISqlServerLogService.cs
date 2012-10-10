// -----------------------------------------------------------------------
// <copyright file="ISqlServerLogService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Maintenance.Service.Interface.JobManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Maintenance.Entity.Model;

    /// <summary>
    /// TODO: Update summary.
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
