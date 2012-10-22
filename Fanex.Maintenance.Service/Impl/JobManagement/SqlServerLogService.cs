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
    using Fanex.Logging;
    using Fanex.Maintenance.Core.Configurations;
    using Fanex.Maintenance.Core.Web;
    using Fanex.Maintenance.Criteria.Job;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Repository.Impl.JobManagement;
    using Fanex.Maintenance.Repository.Interface.JobManagement;
    using Fanex.Maintenance.Service.Interface.JobManagement;
    using System.Threading.Tasks;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SqlServerLogService : ISqlServerLogService
    {
        private readonly ISqlServerLogRepository _sqlServerLogRepository;

        #region Ctor

        /// <summary>
        /// Constructor
        /// </summary>
        private SqlServerLogService()
            : this(null)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sqlServerLogRepository"></param>
        private SqlServerLogService(ISqlServerLogRepository sqlServerLogRepository)
        {
            _sqlServerLogRepository = sqlServerLogRepository ?? new SqlServerLogRepository();
        }

        /// <summary>
        /// Create sql server service
        /// </summary>
        /// <returns></returns>
        public static ISqlServerLogService Create()
        {
            return new SqlServerLogService();
        }

        #endregion Ctor

        #region Properties

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public string ServerIp { get; set; }

        public int? ErrorLevel { get; set; }

        #endregion Properties

        /// <summary>
        /// Get Log With Date Range
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public ISqlServerLogService FromDate(DateTime from)
        {
            this.From = from;
            return this;
        }

        /// <summary>
        /// Get Log With Date Range
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public ISqlServerLogService ToDate(DateTime to)
        {
            this.To = to;
            return this;
        }

        /// <summary>
        /// Get Log With Server Ip
        /// </summary>
        /// <param name="serverIp"></param>
        /// <returns></returns>
        public ISqlServerLogService WithServer(string serverIp)
        {
            this.ServerIp = serverIp;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
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
                    foreach (ServerElement server in DatabaseServersSection.Instance.Servers)
                    {

                        listTask[taskIndex] = Task.Factory.StartNew<IEnumerable<SqlServerLog>>(param
                            =>
                            {
                                SqlLogServerParamForTask sqlLogServerParamForTask = (SqlLogServerParamForTask)param;
                                return GetLogBySpecificDateAndServerIp(sqlLogServerParamForTask.LogType, sqlLogServerParamForTask.ServerIp);
                            }, new SqlLogServerParamForTask
                            {
                                LogType = logType,
                                ServerIp = server.ServerIp
                            });
                        taskIndex++;
                        //sqlServerLogs.AddRange(GetLogBySpecificDateAndServerIp(logType, server.ServerIp));
                    }

                    Task.WaitAll(listTask);
                    for (int i = 0; i < listTask.Length; i++)
                    {
                        sqlServerLogs.AddRange(listTask[i].Result);
                    }
                }

                //// Filter with log status
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

                //// Order Result Data
                sqlServerLogs.Sort((x, y) => DateTime.Compare(y.LogDate, x.LogDate));
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }

            return sqlServerLogs;
        }

        /// <summary>
        /// Get Log By SpecificDate And ServerIp
        /// </summary>
        /// <param name="serverIp"></param>
        /// <returns></returns>
        private IEnumerable<SqlServerLog> GetLogBySpecificDateAndServerIp(int logType, string serverIp)
        {
            List<SqlServerLog> sqlServerLogs = new List<SqlServerLog>();
            //// Restrict get Log from Sql Server is 7 and Sql Server Agent is 10
            int maxDayForGetLog = logType == (int)LogType.SqlServerLog ? 7 : 10;
            Task<IEnumerable<SqlServerLog>>[] listTask = new Task<IEnumerable<SqlServerLog>>[maxDayForGetLog];
            for (int i = 0; i < maxDayForGetLog; i++)
            {
                try
                {
                    switch (logType)
                    {
                        case (int)LogType.SqlServerLog:
                            listTask[i] = Task.Factory.StartNew<IEnumerable<SqlServerLog>>(param
                           =>
                           {
                               SqlLogSpParamForTask sqlLogSpParamForTask = (SqlLogSpParamForTask)param;
                               return _sqlServerLogRepository.GetAllSqlServerLogOnDate(new GetSqlServerLogCriteria
                               {
                                   p1 = sqlLogSpParamForTask.P1
                               }, sqlLogSpParamForTask.ServerIp);
                           }, new SqlLogSpParamForTask
                           {
                               P1 = i,
                               ServerIp = serverIp
                           });
                            break;
                        case (int)LogType.SqlAgentLog:
                            listTask[i] = Task.Factory.StartNew<IEnumerable<SqlServerLog>>(param
                          =>
                            {
                                SqlLogSpParamForTask sqlLogSpParamForTask = (SqlLogSpParamForTask)param;
                                return _sqlServerLogRepository.GetAllSqlAgentLogOnDate(new GetSqlServerLogCriteria
                                {
                                    p1 = sqlLogSpParamForTask.P1,
                                    p2 = 2
                                }, sqlLogSpParamForTask.ServerIp);
                            }, new SqlLogSpParamForTask
                            {
                                P1 = i,
                                ServerIp = serverIp
                            });
                            break;
                    }


                }
                catch (Exception ex)
                {
                    Logger.Log.Error(ex.Message, ex);
                }
            }

            Task.WaitAll(listTask);
            for (int i = 0; i < listTask.Length; i++)
            {
                sqlServerLogs.AddRange(listTask[i].Result);
            }

            sqlServerLogs = sqlServerLogs.Where(sqlServerLog => {
                if (sqlServerLog.LogDate < To && sqlServerLog.LogDate > From)
                {
                    return true;
                }
                return false;
            }).ToList();

            return sqlServerLogs;
        }

        /// <summary>
        /// Get Log with error level: Error, Warning or Information
        /// </summary>
        /// <param name="errorLevel"></param>
        /// <returns></returns>
        public ISqlServerLogService WithErrorLevel(int? errorLevel)
        {
            this.ErrorLevel = errorLevel;
            return this;
        }
    }
}