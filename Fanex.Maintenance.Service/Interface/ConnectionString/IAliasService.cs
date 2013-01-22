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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Maintenance.Entity.Model.ConnectionString;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IAliasService
    {
        IEnumerable<AliasConnectionString> GetAll(string alias = "", int? dbServerId = null, int? siteId = null);
    }
}
