// --------------------------------------------------------------------------
// <copyright file="AliasConnectionString.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.17">Created</change>
// </history>

namespace Fanex.Maintenance.Entity.Model.ConnectionString
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.ConnectionString.Domain;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AliasConnectionString
    {
        public ConnectionString ConnectionString { get; set; }

        public Alias Alias { get; set; }

        public int NumOfSite { get; set; }
    }
}
