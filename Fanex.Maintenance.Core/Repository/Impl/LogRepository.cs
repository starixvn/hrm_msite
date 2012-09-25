// --------------------------------------------------------------------------
// <copyright file="LogRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.20">Created</change>
// </history>

namespace Fanex.Maintenance.Core.Repository.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.NAP.Framework.Data;
    using Fanex.Maintenance.Core.Model;
    using Fanex.Data;

    public class LogRepository : ReadonlyRepository<Logs>
    {
        /// <summary>
        /// Override return Logs
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IEnumerable<Logs> Fetch(ICriteria criteria)
        {
            try
            {
                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    return db.Query<Logs, Categorys, Logs>((m, c) =>
                    {
                        m.Category = new List<Categorys>();
                        m.Category.Add(c);
                        return m;
                    }, "CategoryID,CategoryName", criteria.GetSpParams());
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

    }
}
