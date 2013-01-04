// --------------------------------------------------------------------------
// <copyright file="MemoryInfo.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2013.1.4">Created</change>
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
    public class MemoryInfo
    {
        public float MemoryUsage { get; set; }

        public string ServerIp { get; set; }

        public string ServerName { get; set; }

        public float TotalMemory { get; set; }
    }
}
