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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.ConnectionString.Domain;
    using Fanex.Maintenance.Entity.Model.ConnectionString;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ISiteService
    {
        IEnumerable<Site> GetAll(string name = "", int? dbServerId = null);

        IEnumerable<SiteAlias> GetSiteWithNumOfAlias(string name = "", int? dbServerId = null);

        IEnumerable<Site> GetByAlias(int aliasId);

        IEnumerable<Site> GetByDbServer(int dbServerId);

        Site GetById(int id);

        ICommandResult InsertOrUpdate(ICriteria criteria);
    }
}
