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
    using System.Linq;
    using System.Text;
    using Fanex.Maintenance.Repository.Interface.ConnectionString;
    using Fanex.ConnectionString.Domain;
    using NAP.Framework.Data;
    using Fanex.Data;

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
    }
}
