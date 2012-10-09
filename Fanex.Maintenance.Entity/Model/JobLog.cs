// --------------------------------------------------------------------------
// <copyright file="JobLog.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.9">Created</change>
// </history>

namespace Fanex.Maintenance.Entity.Model
{
    using Fanex.Maintenance.Entity.Domain;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class JobLog
    {
        public int InstanceId { get; set; }

        public Job Job { get; set; }

        public Step Step { get; set; }

        public int SqlMessageId { get; set; }

        public int SqlSeverityId { get; set; }

        public string Message { get; set; }

        public int RunSatus { get; set; }

        public int RunDate { get; set; }

        public int RunTime { get; set; }

        public int RunDuration { get; set; }

        public int OperatorIdEmailed { get; set; }

        public int OperatorIdNetsend { get; set; }

        public int OperatorIdPaged { get; set; }

        public int RetriesAttempted { get; set; }

        public string Server { get; set; }
    }
}
