// --------------------------------------------------------------------------
// <copyright file="Logs.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.20">Created</change>
// </history>

namespace Fanex.Maintenance.Entity.Model
{
    using System;
    using System.Collections.Generic;

    public class Logs
    {
        public long LogId { get; set; }

        public int EventId { get; set; }

        public string Severity { get; set; }

        public string Title { get; set; }

        public DateTime TimeStamp { get; set; }

        public string MachineName { get; set; }

        public string AppDomainName { get; set; }

        public int Priority { get; set; }

        public int C_EventId { get; set; }

        public string ProcessId { get; set; }

        public string ProcessName { get; set; }

        public string Win32ThreadId { get; set; }

        public string ThreadName { get; set; }

        public List<LogCategorys> Category { get; set; }

        public string Message { get; set; }
    }
}
