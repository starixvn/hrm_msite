// --------------------------------------------------------------------------
// <copyright file="SqlServerLogRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.10">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Impl.JobManagement
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Data;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Repository.Interface.JobManagement;
    using Fanex.NAP.Framework.Data;
    using Fanex.Common.Extensions;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SqlServerLogRepository : BaseRepository<SqlServerLog>, ISqlServerLogRepository
    {
        /// <summary>
        /// Get All Sql Server Log With specific Date
        /// </summary>
        /// <param name="sqlServerLogCriteria"></param>
        /// <param name="serverIp"></param>
        /// <returns></returns>
        public IEnumerable<SqlServerLog> GetAllSqlServerLogOnDate(ICriteria sqlServerLogCriteria, string serverIp)
        {
            try
            {
                //// Create Function will map Data from DataReader to Schedule Object
                Func<IDataReader, SqlServerLog> mapObjectFunc = (reader) =>
                {
                    return new SqlServerLog
                    {
                        LogDate = reader.ToDateTime("LogDate"),
                        LogMessage = reader.ToString("Text")
                    };
                };

                if (!string.IsNullOrEmpty(serverIp))
                {
                    IEnumerable<SqlServerLog> serverLogs = Fetch<SqlServerLog>(sqlServerLogCriteria, serverIp, mapObjectFunc);
                    serverLogs.ForEach(serverLog =>
                    {
                        serverLog.ServerIp = serverIp;
                    });
                    return serverLogs;
                }
            }
            catch (Exception ex)
            {
                //Log here
                //throw ex;
            }
            return new List<SqlServerLog>();
        }

        /// <summary>
        /// Get All Sql Agent Log With Specific Date
        /// </summary>
        /// <param name="sqlAgentLogCriteria"></param>
        /// <param name="serverIp"></param>
        /// <returns></returns>
        public IEnumerable<SqlServerLog> GetAllSqlAgentLogOnDate(ICriteria sqlAgentLogCriteria, string serverIp)
        {
            try
            {
                //// Create Function will map Data from DataReader to Schedule Object
                Func<IDataReader, SqlServerLog> mapObjectFunc = (reader) =>
                {
                    return new SqlServerLog
                    {
                        LogDate = reader.ToDateTime("LogDate"),
                        LogMessage = reader.ToString("Text"),
                        ErrorLevel = reader.ToInt("ErrorLevel")
                    };
                };

                if (!string.IsNullOrEmpty(serverIp))
                {
                    IEnumerable<SqlServerLog> serverLogs = Fetch<SqlServerLog>(sqlAgentLogCriteria, serverIp, mapObjectFunc);
                    serverLogs.ForEach(serverLog =>
                    {
                        serverLog.ServerIp = serverIp;
                    });
                    return serverLogs;
                }
            }
            catch (Exception ex)
            {
                //Log here
                //throw ex;
            }
            return new List<SqlServerLog>();
        }
    }
}