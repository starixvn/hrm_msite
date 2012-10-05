// --------------------------------------------------------------------------
// <copyright file="OrinatingServers.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.5">Created</change>
// </history>

namespace Fanex.Maintenance.Entity.Model
{
    using Fanex.Maintenance.Entity.Domain;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class OrinatingServers : IOrinatingServers
    {
        public int MasterServer { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
