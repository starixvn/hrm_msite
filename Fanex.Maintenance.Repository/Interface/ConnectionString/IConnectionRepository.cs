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
    using System.Diagnostics.CodeAnalysis;
    using Fanex.ConnectionString.Domain;
    using Fanex.Maintenance.Entity.Model.ConnectionString;
    using Fanex.NAP.Framework.Data;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public interface IConnectionRepository
    {
        IEnumerable<ConnectionString> GetSecondaryConection(ICriteria criteria);

        IEnumerable<ConnectionStringConnection> GetByAlais(ICriteria criteria);

        ConnectionString GetById(ICriteria criteria);

        ICommandResult InsertOrUpdate(ICriteria criteria, ICriteria logCriteria);
    }
}
