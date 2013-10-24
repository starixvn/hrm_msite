// --------------------------------------------------------------------------
// <copyright file="IConnectionRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.22">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Interface.ConnectionString
{
    using System.Collections.Generic;
    using Fanex.ConnectionString.Domain;
    using Fanex.Maintenance.Entity.Model.ConnectionString;
    using Fanex.NAP.Framework.Data;

    public interface IConnectionRepository
    {
        IEnumerable<ConnectionString> GetSecondaryConnection(ICriteria criteria);

        IEnumerable<ConnectionStringConnection> GetByAlias(ICriteria criteria);

        ConnectionString GetById(ICriteria criteria);

        ICommandResult InsertOrUpdate(ICriteria criteria, ICriteria logCriteria);
    }
}
