// --------------------------------------------------------------------------
// <copyright file="LogRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.20">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Impl
{
    using System;
    using System.Collections.Generic;
    using Fanex.Data;
    using Fanex.Maintenance.Criteria.Log;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Log repository
    /// </summary>
    public class LogRepository : ReadonlyRepository<Log>
    {
        /// <summary>
        /// Override return Logs
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IEnumerable<Log> GetLog(ref GetLogCriteria criteria)
        {
            try
            {
                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    var param = criteria.GetSpParams();

                    var list = db.Query<Log, LogCategory, Machine, Log>((l, c, m) =>
                    {
                        l.Category = c;
                        l.Machine = m;
                        return l;
                    }, "CategoryID,MachineId", param);

                    criteria.TotalRows = Convert.ToInt32(param["totalrows"]);

                    return list;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get details message of log
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        public string GetLogMessage(long logId)
        {
            using (ObjectDb db = new ObjectDb("Logging_SelMessage"))
            {
                string temp = db.ExecuteScalar(new { logid = logId }).ToString();
                return temp;
            }
        }

        /// <summary>
        /// Remove log
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public int DeleteLogs(DateTime from)
        {
            int row = 0;
            try
            {
                using (ObjectDb db = new ObjectDb("Logging_Del"))
                {
                    row = db.ExecuteNonQuery(new { from = from });
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return row;
        }

        /// <summary>
        /// Update log's monitor status
        /// </summary>
        /// <param name="logId"></param>
        /// <param name="monitoringUserId"></param>
        /// <param name="isMonitored"></param>
        /// <returns></returns>
        public int MonitorLog(string logIds, int? monitoringUserId, bool isMonitored)
        {
            int row = 0;
            try
            {
                using (ObjectDb db = new ObjectDb("Logging_Monitor"))
                {
                    row = db.ExecuteNonQuery(new { LogIds = logIds, @MonitoringUserId = monitoringUserId, IsMonitored = isMonitored });
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return row;
        }
    }
}