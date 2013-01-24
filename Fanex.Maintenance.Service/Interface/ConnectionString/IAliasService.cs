// --------------------------------------------------------------------------
// <copyright file="IAliasService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.21">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Interface.ConnectionString
{
    using System.Collections.Generic;
    using Fanex.Maintenance.Entity.Model.ConnectionString;
    using Fanex.NAP.Framework.Data;
    using Fanex.ConnectionString.Domain;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IAliasService
    {
        IEnumerable<AliasConnectionString> GetAll(string alias = "", int? dbServerId = null, int? siteId = null);

        IEnumerable<Alias> GetBySite(int siteId);

        IEnumerable<AliasConnectionStringConnection> GetSwitchConnection(int fromDbServerId, int toDbServerId);

        Alias Get(int aliasId);

        bool InsertOrUpdate(ICriteria criteria);

        bool UpdateStatus(int aliasId, bool isActive);
    }
}
