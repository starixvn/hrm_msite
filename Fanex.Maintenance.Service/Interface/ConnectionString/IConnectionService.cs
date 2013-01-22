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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.ConnectionString.Domain;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IConnectionService
    {
        IEnumerable<ConnectionString> GetByAlias(int aliasId);

        bool UpdateDefaultConnection(int aliasId, int conectionId);
    }
}
