// --------------------------------------------------------------------------
// <copyright file="IOrinatingServers.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.5">Created</change>
// </history>

namespace Fanex.Maintenance.Entity.Domain
{
    public class OrinatingServer : IEntity<int, string>
    {
        public int MasterServer { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
