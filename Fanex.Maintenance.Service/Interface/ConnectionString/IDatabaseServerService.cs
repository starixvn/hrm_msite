// --------------------------------------------------------------------------
// <copyright file="IDatabaseServerService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.21">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Interface.ConnectionString
{
    using System.Collections.Generic;
    using Fanex.ConnectionString.Domain;
    using Fanex.Maintenance.Entity.Model.ConnectionString;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Database server service interface.
    /// </summary>
    public interface IDatabaseServerService
    {
        IEnumerable<DatabaseServer> GetAll();

        IEnumerable<DatabaseServerSite> GetWithNumOfSite(string name = "");

        DatabaseServer GetById(int id);

        ICommandResult InsertOrUpdate(ICriteria criteria);
    }
}
