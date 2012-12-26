// --------------------------------------------------------------------------
// <copyright file="ServerStatus.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.11.2">Created</change>
// </history>


namespace Fanex.Maintenance.Entity.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ServerStatus
    {
        public string ServerIp { get; set; }
        public bool SqlServerAgentRunning { get; set; }
        public bool SqlServerCannotConnect { get; set; }
    }
}
