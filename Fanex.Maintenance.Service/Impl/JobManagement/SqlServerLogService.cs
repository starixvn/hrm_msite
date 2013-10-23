// --------------------------------------------------------------------------
// <copyright file="SqlServerLogService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.12">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Impl.JobManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Fanex.Logging;
    using Fanex.Maintenance.Core.Configurations;
    using Fanex.Maintenance.Core.Web;
    using Fanex.Maintenance.Criteria.Job;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Repository.Impl.JobManagement;
    using Fanex.Maintenance.Repository.Interface.JobManagement;
    using Fanex.Maintenance.Service.Interface.JobManagement;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SqlServerLogService : ISqlServerLogService
    {
        private readonly ISqlServerLogRepository _sqlServerLogRepository;

        #region Ctor

        private SqlServerLogService()
            : this(null)
        {
        }

        private SqlServerLogService(ISqlServerLogRepository sqlServerLogRepository)
        {
            _sqlServerLogRepository = sqlServerLogRepository ?? new SqlServerLogRepository();
        }

        #endregion Ctor

        #region Properties

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public string ServerIp { get; set; }

        public int? ErrorLevel { get; set; }

        #endregion Properties

        /// <summary>
        /// Create SQL server service.
        /// </summary>
        /// <returns>SQL Server log service.</returns>
        public static ISqlServerLogService Create()
        {
            return new SqlServerLogService();
        }

        /// <summary>
        /// Get Log With Date Range.
        /// </summary>
        /// <param name="from">From date time.</param>
        /// <returns>SQL Server log service.</returns>
        public ISqlServerLogService FromDate(DateTime from)
        {
            this.From = from;

            return this;
        }

        /// <summary>
        /// Get Log With Date Range.
        /// </summary>
        /// <param name="to">To date time.</param>
        /// <returns>SQL Server log service.</returns>
        public ISqlServerLogService ToDate(DateTime to)
        {
            this.To = to;

            return this;
        }

        /// <summary>
        /// Get Log With Server IP.
        /// </summary>
        /// <param name="serverIp">Server IP.</param>
        /// <returns>SQL Server log service.</returns>
        public ISqlServerLogService WithServer(string serverIp)
        {
            this.ServerIp = serverIp;

            return this;
        }

        /// <summary>
        /// Get SQL server logs.
        /// </summary>
        /// <param name="logType">Log type.</param>
        /// <returns>List of SQL Server log.</returns>
        public IEnumerable<SqlServerLog> GetSqlServerLogs(int logType = 1)
        {
            List<SqlServerLog> sqlServerLogs = new List<SqlServerLog>();

            try
            {
                //// Get All Schedule from Database
                if (!string.IsNullOrEmpty(ServerIp))
                {
                    sqlServerLogs.AddRange(GetLogBySpecificDateAndServerIp(logType, ServerIp));
                }
                else
                {
                    Task<IEnumerable<SqlServerLog>>[] listTask = new Task<IEnumerable<SqlServerLog>>[DatabaseServersSection.Instance.Servers.Count];
                    int taskIndex = 0;

                    foreach (ServerElement server in DatabaseServersSection.Instance.ResourceServers)
                    {
                        listTask[taskIndex] = Task.Factory.StartNew<IEnumerable<SqlServerLog>>(
                            param =>
                            {
                                SqlLogServerParamForTask sqlLogServerParamForTask = (SqlLogServerParamForTask)param;
                                return GetLogBySpecificDateAndServerIp(sqlLogServerParamForTask.LogType, sqlLogServerParamForTask.ServerIp);
                            },
                            new SqlLogServerParamForTask
                            {
                                LogType = logType,
                                ServerIp = server.ServerIp
                            });
                        taskIndex++;

                        // sqlServerLogs.AddRange(GetLogBySpecificDateAndServerIp(logType, server.ServerIp));
                    }

                    Task.WaitAll(listTask);

                    for (int i = 0; i < listTask.Length; i++)
                    {
                        sqlServerLogs.AddRange(listTask[i].Result);
                    }
                }

                // Filter with log status
                if (logType == (int)LogType.SqlAgentLog && this.ErrorLevel != null)
                {
                    sqlServerLogs = sqlServerLogs.Where<SqlServerLog>((sqlServerLog) =>
                        {
                            if (sqlServerLog.ErrorLevel == this.ErrorLevel)
                            {
                                return true;
                            }

                            return false;
                        }).ToList();
                }

                // Order Result Data
                sqlServerLogs.Sort((x, y) => DateTime.Compare(y.LogDate, x.LogDate));
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }

            return sqlServerLogs;
        }

        /// <summary>
        /// Get Log with error level: Error, Warning or Information.
        /// </summary>
        /// <param name="errorLevel">Error level.</param>
        /// <returns>SQL Server log service.</returns>
        public ISqlServerLogService WithErrorLevel(int? errorLevel)
        {
            this.ErrorLevel = errorLevel;

            return this;
        }

        /// <summary>
        /// Get Log By SpecificDate And Server IP.
        /// </summary>
        /// <param name="logType">Log type.</param>
        /// <param name="serverIp">Server IP address.</param>
        /// <returns>List of SQL Server log.</returns>
        private IEnumerable<SqlServerLog> GetLogBySpecificDateAndServerIp(int logType, string serverIp)
        {
            List<SqlServerLog> sqlServerLogs = new List<SqlServerLog>();

            try
            {
                var criteria = new GetReadSQLLogCriteria
                {
                    FromDate = From,
                    ToDate = To
                };

                switch (logType)
                {
                    case (int)LogType.SqlServerLog:
                        criteria.IsAgent = false;
                        sqlServerLogs = _sqlServerLogRepository.GetAllSqlLogOnDate(criteria, serverIp, false).ToList();
                        break;

                    case (int)LogType.SqlAgentLog:
                        criteria.IsAgent = true;
                        sqlServerLogs = _sqlServerLogRepository.GetAllSqlLogOnDate(criteria, serverIp, true).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }

            return sqlServerLogs;
        }
    }
}