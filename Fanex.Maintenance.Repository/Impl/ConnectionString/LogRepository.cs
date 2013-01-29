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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Maintenance.Repository.Interface.ConnectionString;
    using Fanex.Maintenance.Entity.Model.ConnectionString;
    using Fanex.NAP.Framework.Data;
    using Fanex.Data;
    using System.Collections;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LogRepository : BaseRepository<LogExecSp>, ILogRepository
    {
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
