// --------------------------------------------------------------------------
// <copyright file="ISiteRepository.cs" company="Nexcel Solutions Vietnam">
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
    public interface ISiteRepository
    {
        IEnumerable<Site> Fetch(ICriteria criteria);

        Site Get(ICriteria criteria);

        IEnumerable<SiteAlias> GetSiteAlias(ICriteria criteria);

        ICommandResult InsertOrUpdate(ICriteria criteria, ICriteria logCriteria);
    }
}
