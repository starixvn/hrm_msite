// --------------------------------------------------------------------------
// <copyright file="SqlServerLog.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.10">Created</change>
//   <change who="Ricky.Nguyen" date="2012.10.22">Update Task Object</change>
// </history>

namespace Fanex.Maintenance.Entity.Model
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class SqlServerLog
    {
        public DateTime LogDate { get; set; }

        public string LogMessage { get; set; }

        public int ErrorLevel { get; set; }

        public string ServerIp { get; set; }
    }

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class SqlLogServerParamForTask
    {
        public int LogType { get; set; }

        public string ServerIp { get; set; }
    }

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class SqlLogSpParamForTask
    {
        public int P1 { get; set; }

        public int P2 { get; set; }

        public string ServerIp { get; set; }
    }
}
