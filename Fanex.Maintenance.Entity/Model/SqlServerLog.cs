// --------------------------------------------------------------------------
// <copyright file="SqlServerLog.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.10">Created</change>
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
    public class SqlServerLog
    {
        public DateTime LogDate { get; set; }
        public string LogMessage { get; set; }
        public int ErrorLevel { get; set; }
        public string ServerIp { get; set; }
    }
}
