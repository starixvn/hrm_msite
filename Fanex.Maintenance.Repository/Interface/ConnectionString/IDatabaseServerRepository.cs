// --------------------------------------------------------------------------
// <copyright file="IDatabaseServerRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.21">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Interface.ConnectionString
{
    using System.Collections.Generic;
    using Fanex.ConnectionString.Domain;
    using Fanex.Maintenance.Entity.Model.ConnectionString;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IDatabaseServerRepository
    {
        IEnumerable<DatabaseServer> GetAll(ICriteria criteria);

        IEnumerable<DatabaseServerSite> GetWithNumOfSite(ICriteria criteria);

        DatabaseServer Get(ICriteria criteria);

        ICommandResult InsertOrUpdate(ICriteria criteria, ICriteria logCriteria);
    }
}
