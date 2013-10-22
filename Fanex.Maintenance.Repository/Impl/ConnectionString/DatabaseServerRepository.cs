// --------------------------------------------------------------------------
// <copyright file="DatabaseServerRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.21">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Impl.ConnectionString
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Entity.Model.ConnectionString;
    using Fanex.ConnectionString.Domain;
    using Fanex.Data;
    using Fanex.Maintenance.Criteria.Misc;
    using Fanex.Maintenance.Repository.Interface.ConnectionString;
    using NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DatabaseServerRepository : BaseRepository<DatabaseServer>, IDatabaseServerRepository
    {
        /// <summary>
        /// Get all database server.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>List of database server.</returns>
        public IEnumerable<DatabaseServer> GetAll(ICriteria criteria)
        {
            try
            {
                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    return db.Query<DatabaseServer, Users, DatabaseServer>(
                        (d, u) => { d.Modifier = u; return d; },
                        "ModifierId",
                        criteria.GetSpParams());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get database server with number of site.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>List of database server site.</returns>
        public IEnumerable<DatabaseServerSite> GetWithNumOfSite(ICriteria criteria)
        {
            try
            {
                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    return db.Query<DatabaseServerSite, DatabaseServer, Users, DatabaseServerSite>(
                        (ds, s, u) => { ds.DatabaseServer = s; s.Modifier = u; return ds; },
                        "Id,ModifierId",
                        criteria.GetSpParams());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Insert or update database server.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="logCriteria">Log criteria.</param>
        /// <returns>Command result.</returns>
        public ICommandResult InsertOrUpdate(ICriteria criteria, ICriteria logCriteria)
        {
            try
            {
                Hashtable hash = criteria.GetSpParams();
                hash = logCriteria.AddSpParams(hash);
                hash = (new ResultCriteria()).AddSpParams(hash);

                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    db.ExecuteNonQuery(hash);

                    return GetCommandResult(hash);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get database server.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Database server.</returns>
        public DatabaseServer Get(ICriteria criteria)
        {
            try
            {
                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    return db.QueryEntity<DatabaseServer, Users, DatabaseServer>(
                        (d, u) => { d.Modifier = u; return d; },
                        "ModifierId",
                        criteria.GetSpParams());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}