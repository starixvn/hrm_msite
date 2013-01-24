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
    using System.Collections.Generic;
    using System.Data;
    using Entity.Model.ConnectionString;
    using Fanex.Common.Extensions;
    using Fanex.ConnectionString.Domain;
    using Fanex.Data;
    using Fanex.Maintenance.Repository.Interface.ConnectionString;
    using NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DatabaseServerRepository : IDatabaseServerRepository
    {
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

        public int InsertOrUpdate(ICriteria criteria)
        {
            try
            {
                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    var hsParam = criteria.GetSpParams();

                    db.ExecuteNonQuery(hsParam);

                    return (int)hsParam["dbserverid"];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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