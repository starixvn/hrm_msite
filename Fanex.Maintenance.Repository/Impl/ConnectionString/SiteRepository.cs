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
    using Fanex.ConnectionString.Domain;
    using Fanex.Data;
    using Fanex.Maintenance.Entity.Model.ConnectionString;
    using Fanex.Maintenance.Repository.Interface.ConnectionString;
    using NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SiteRepository : ISiteRepository
    {
        public IEnumerable<Site> Fetch(ICriteria criteria)
        {
            try
            {
                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    return db.Query<Site, Users, Site>(
                        (s, u) => { s.Modifier = u; return s; },
                        "ModifierUserId",
                        criteria.GetSpParams());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<SiteAlias> GetSiteAlias(ICriteria criteria)
        {
            try
            {
                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    return db.Query<SiteAlias, Site, Users, SiteAlias>(
                        (sa, s, u) => { sa.Site = s; s.Modifier = u; return sa; },
                        "Id,ModifierUserId",
                        criteria.GetSpParams());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Site Get(ICriteria criteria)
        {
            try
            {
                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    return db.QueryEntity<Site>(criteria.GetSpParams());
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

                    return (int)hsParam["siteid"];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}