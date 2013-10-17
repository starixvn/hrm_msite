// --------------------------------------------------------------------------
// <copyright file="IConnectionService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.22">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Interface.ConnectionString
{
    using System.Collections.Generic;
    using Fanex.ConnectionString.Domain;
    using Fanex.Maintenance.Entity.Model.ConnectionString;
    using Fanex.NAP.Framework.Data;

    public interface IConnectionService
    {
        IEnumerable<ConnectionStringConnection> GetByAlias(int aliasId);

        IEnumerable<ConnectionString> GetSecondaryConection(int id);

        ConnectionString GetById(int id);

        ICommandResult InsertOrUpdate(ICriteria criteria);
    }
}
