// -----------------------------------------------------------------------
// <copyright file="SqlServerLogService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Maintenance.Service.Impl.JobManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Maintenance.Service.Interface.JobManagement;
    using Fanex.Maintenance.Repository.Interface.JobManagement;
    using Fanex.Maintenance.Repository.Impl.JobManagement;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Core.Configurations;
    using Fanex.Maintenance.Criteria.Job;

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

        public static ISqlServerLogService Create()
        {
            return new SqlServerLogService();
        }

        #endregion Ctor

        #region Properties

        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string ServerIp { get; set; }

        #endregion

        public ISqlServerLogService FromDate(DateTime from)
        {
            this.From = from;
            return this;
        }

        public ISqlServerLogService ToDate(DateTime to)
        {
            this.To = to;
            return this;
        }

        public ISqlServerLogService WithServer(string serverIp)
        {
            this.ServerIp = serverIp;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SqlServerLog> GetSqlServerLogs()
        {
            List<SqlServerLog> sqlServerLogs = new List<SqlServerLog>();
            //// Get All Schedule from Database
            if (!string.IsNullOrEmpty(ServerIp))
            {
                sqlServerLogs.AddRange(GetLogBySpecificDateAndServerIp(ServerIp));
            }
            else
            {
                foreach (ServerElement server in DatabaseServersSection.Instance.Servers)
                {
                    sqlServerLogs.AddRange(GetLogBySpecificDateAndServerIp(server.ServerIp));
                }
            }

            //// Order Result Data
            sqlServerLogs.Sort((x, y) => DateTime.Compare(y.LogDate, x.LogDate));

            return sqlServerLogs;
        }

        /// <summary>
        /// Get Log By SpecificDate And ServerIp
        /// </summary>
        /// <param name="serverIp"></param>
        /// <returns></returns>
        private IEnumerable<SqlServerLog> GetLogBySpecificDateAndServerIp(string serverIp)
        {
            List<SqlServerLog> sqlServerLogs = new List<SqlServerLog>();
            TimeSpan maxQueryTimeSpan = DateTime.Now - From;
            TimeSpan queryTimeSpan = To - From;
            //// Restrict get Log from Sql Server is 7
            int maxDayForGetLog = maxQueryTimeSpan.Days > 6 ? 6 : maxQueryTimeSpan.Days;
            int endDate = maxDayForGetLog;
            int startDate = (DateTime.Now - To).Days;
            for (int i = startDate; i <= maxDayForGetLog; i++)
            {
                try
                {
                    sqlServerLogs.AddRange(_sqlServerLogRepository.GetAllSqlServerLogOnDate(
                        new GetSqlServerLogCriteria
                        {
                            p1 = i
                        }, serverIp));
                }
                catch (Exception ex)
                {
                }
            }
            return sqlServerLogs;
        }
    }
}
