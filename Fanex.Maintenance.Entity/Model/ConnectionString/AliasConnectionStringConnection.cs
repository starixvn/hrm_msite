// --------------------------------------------------------------------------
// <copyright file="AliasConnectionStringConnection.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.17">Created</change>
// </history>

namespace Fanex.Maintenance.Entity.Model.ConnectionString
{
    using Fanex.ConnectionString.Domain;

    /// <summary>
    /// Alias connection string connection.
    /// </summary>
    public class AliasConnectionStringConnection
    {
        public Alias Alias { get; set; }

        public ConnectionString FromConnectionString { get; set; }

        public ConnectionString ToConnectionString { get; set; }
    }
}
