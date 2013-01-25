// --------------------------------------------------------------------------
// <copyright file="IConnectionStringRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.17">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Interface.ConnectionString
{
    using System.Collections.Generic;
    using Fanex.Maintenance.Entity.Model.ConnectionString;
    using Fanex.NAP.Framework.Data;
    using Fanex.ConnectionString.Domain;

    public interface IAliasRepository
    {
        IEnumerable<AliasConnectionString> GetAll(ICriteria criteria);

        IEnumerable<AliasConnectionStringConnection> GetSwitchConnection(ICriteria criteria);

        IEnumerable<Alias> GetBySite(ICriteria criteria);

        Alias GetById(ICriteria criteria);

        ICommandResult InsertOrUpdate(ICriteria criteria, ICriteria logCriteria);
    }
}
