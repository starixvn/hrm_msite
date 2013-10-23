// --------------------------------------------------------------------------
// <copyright file="SiteRepository.cs" company="Nexcel Solutions Vietnam">
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
    using System.Linq;
    using Fanex.ConnectionString.Domain;
    using Fanex.Data;
    using Fanex.Maintenance.Criteria.Misc;
    using Fanex.Maintenance.Entity.Model.ConnectionString;
    using Fanex.Maintenance.Repository.Interface.ConnectionString;
    using NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SiteRepository : BaseRepository<Site>, ISiteRepository
    {
        /// <summary>
        /// Fetch site.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>List of site.</returns>
        public override IEnumerable<Site> Fetch(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<Site, Users, Site>(
                    (s, u) => { s.Modifier = u; return s; },
                    "ModifierUserId",
                    criteria.GetSpParams()).OrderBy(x => x.Name).ToList();
            }
        }

        /// <summary>
        /// Get site alias.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>List of site alias.</returns>
        public IEnumerable<SiteAlias> GetSiteAlias(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<SiteAlias, Site, Users, SiteAlias>(
                    (sa, s, u) => { sa.Site = s; s.Modifier = u; return sa; },
                    "Id,ModifierUserId",
                    criteria.GetSpParams()).OrderBy(x => x.Site.Name).ToList();
            }
        }

        /// <summary>
        /// Get site object.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Site object.</returns>
        public override Site Get(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.QueryEntity<Site>(criteria.GetSpParams());
            }
        }

        /// <summary>
        /// Insert or update site.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="logCriteria">Log criteria.</param>
        /// <returns>Command result.</returns>
        public ICommandResult InsertOrUpdate(ICriteria criteria, ICriteria logCriteria)
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
    }
}