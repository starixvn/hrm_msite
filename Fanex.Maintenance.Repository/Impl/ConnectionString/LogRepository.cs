// --------------------------------------------------------------------------
// <copyright file="LogRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.28">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Impl.ConnectionString
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Data;
    using Fanex.Maintenance.Entity.Model.ConnectionString;
    using Fanex.Maintenance.Repository.Interface.ConnectionString;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Log repository.
    /// </summary>
    public class LogRepository : BaseRepository<LogExecSp>, ILogRepository
    {
        /// <summary>
        /// Fetch log.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="totalRows">Number of rows.</param>
        /// <returns>List of log execute SP.</returns>
        public IEnumerable<LogExecSp> Fetch(ICriteria criteria, out int totalRows)
        {
            try
            {
                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    Hashtable hash = criteria.GetSpParams();

                    var logList = db.Query<LogExecSp>(hash);

                    totalRows = Convert.ToInt32(hash["totalrows"]);

                    return logList.AsQueryable();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get log by ID.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Log execute SP.</returns>
        public LogExecSp GetById(ICriteria criteria)
        {
            try
            {
                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    Hashtable hash = criteria.GetSpParams();

                    return db.QueryEntity<LogExecSp>(hash);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
