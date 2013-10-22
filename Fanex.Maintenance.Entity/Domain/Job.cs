// --------------------------------------------------------------------------
// <copyright file="Job.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.5">Created</change>
// </history>

namespace Fanex.Maintenance.Entity.Domain
{
    using System;

    /// <summary>
    /// The job class.
    /// </summary>
    public class Job : IEntity<int, string>
    {
        public Guid JobId { get; set; }

        public OrinatingServer OrinatingServer { get; set; }

        public int Enable { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// ID of the step in the job where execution should begin.
        /// </summary>
        public int StartStepId { get; set; }

        public JobCategory Category { get; set; }

        /// <summary>
        /// Security identifier number (SID) of the job owner.
        /// </summary>
        public byte[] OwerSid { get; set; }

        /// <summary>
        ///  Bitmask indicating under what circumstances a notification event should be logged to the Microsoft Windows application log:
        /// 0 = Never
        /// 1 = When the job succeeds
        /// 2 = When the job fails
        /// 3 = Whenever the job completes (regardless of the job outcome).
        /// </summary>
        public int NotifyLevelEventLog { get; set; }

        /// <summary>
        /// Bitmask indicating under what circumstances a notification e-mail should be sent when a job completes:
        /// 0 = Never
        /// 1 = When the job succeeds
        /// 2 = When the job fails
        /// 3 = Whenever the job completes (regardless of the job outcome).
        /// </summary>
        public int NotifyLevelEmail { get; set; }

        /// <summary>
        /// Bitmask indicating under what circumstances a network message should be sent when a job completes:
        /// 0 = Never
        /// 1 = When the job succeeds
        /// 2 = When the job fails
        /// 3 = Whenever the job completes (regardless of the job outcome).
        /// </summary>
        public int NotifyLevelNetsend { get; set; }

        /// <summary>
        /// Bitmask indicating under what circumstances a page should be sent when a job completes:
        /// 0 = Never
        /// 1 = When the job succeeds
        /// 2 = When the job fails
        /// 3 = Whenever the job completes (regardless of the job outcome).
        /// </summary>
        public int NotifyLevelPage { get; set; }

        /// <summary>
        /// E-mail name of the operator to notify.
        /// </summary>
        public Operator NotifyEmailOperator { get; set; }

        /// <summary>
        /// ID of the computer or user used when sending network messages.
        /// </summary>
        public Operator NotifyNetsendOperator { get; set; }

        /// <summary>
        /// ID of the computer or user used when sending a page.
        /// </summary>
        public Operator NotifyPageOperator { get; set; }

        /// <summary>
        ///  Bitmask indicating under what circumstances the job should be deleted when a job completes:
        /// 0 = Never
        /// 1 = When the job succeeds
        /// 2 = When the job fails
        /// 3 = Whenever the job completes (regardless of the job outcome).
        /// </summary>
        public int DeleteLevel { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public int VersionNumber { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
