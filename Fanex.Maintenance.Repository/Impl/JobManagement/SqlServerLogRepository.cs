// --------------------------------------------------------------------------
// <copyright file="SqlServerLogRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.10">Created</change>
//   <change who="Ricky.Nguyen" date="2012.10.12">Update StyleCop</change>
// </history>

namespace Fanex.Maintenance.Repository.Impl.JobManagement
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Fanex.Common.Extensions;
    using Fanex.Logging;
    using Fanex.Maintenance.Core.Web;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Repository.Interface.JobManagement;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SqlServerLogRepository : BaseRepository<SqlServerLog>, ISqlServerLogRepository
    {
        /// <summary>
        /// Get All SQL Server Log With specific Date.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="serverIp">Server IP.</param>
        /// <returns>List of SQL Server log.</returns>
        public IEnumerable<SqlServerLog> GetAllSqlServerLogOnDate(ICriteria criteria, string serverIp)
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
                    IEnumerable<SqlServerLog> serverLogs = Fetch<SqlServerLog>(criteria, serverIp, mapObjectFunc);
                    serverLogs.ForEach(serverLog =>
                    {
                        serverLog.ServerIp = serverIp;
                    });
                    return serverLogs;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }

            return new List<SqlServerLog>();
        }

        /// <summary>
        /// Get All SQL Agent Log With Specific Date.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="serverIp">Server IP.</param>
        /// <returns>List of SQL Server log.</returns>
        public IEnumerable<SqlServerLog> GetAllSqlAgentLogOnDate(ICriteria criteria, string serverIp)
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
                    IEnumerable<SqlServerLog> serverLogs = Fetch<SqlServerLog>(criteria, serverIp, mapObjectFunc);
                    serverLogs.ForEach(serverLog =>
                    {
                        serverLog.ServerIp = serverIp;
                    });
                    return serverLogs;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }

            return new List<SqlServerLog>();
        }

        /// <summary>
        /// Get all SQL log on date.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="serverIp">Server IP address.</param>
        /// <param name="isAgent">Is agent or not.</param>
        /// <returns>List of SQL Server log.</returns>
        public IEnumerable<SqlServerLog> GetAllSqlLogOnDate(ICriteria criteria, string serverIp, bool isAgent)
        {
            try
            {
                //// Create Function will map Data from DataReader to Schedule Object
                Func<IDataReader, SqlServerLog> mapObjectFunc;

                if (isAgent)
                {
                    mapObjectFunc = (reader) =>
                    {
                        return new SqlServerLog
                        {
                            LogDate = reader.ToDateTime("LogDate"),
                            LogMessage = reader.ToString("Text"),
                            ErrorLevel = reader.ToInt("ErrorLevel")
                        };
                    };
                }
                else
                {
                    mapObjectFunc = (reader) =>
                    {
                        return new SqlServerLog
                        {
                            LogDate = reader.ToDateTime("LogDate"),
                            LogMessage = reader.ToString("Text")
                        };
                    };
                }

                if (!string.IsNullOrEmpty(serverIp))
                {
                    IEnumerable<SqlServerLog> serverLogs = Fetch<SqlServerLog>(criteria, serverIp, mapObjectFunc, (int)ServerType.Resource);
                    serverLogs.ForEach(serverLog =>
                    {
                        serverLog.ServerIp = serverIp;
                    });
                    return serverLogs;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }

            return new List<SqlServerLog>();
        }
    }
}