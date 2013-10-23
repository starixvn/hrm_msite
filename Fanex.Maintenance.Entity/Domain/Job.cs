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

        public int StartStepId { get; set; }

        public JobCategory Category { get; set; }

        /// <summary>
        /// Gets or sets Security identifier number (SID) of the job owner.
        /// </summary>
        /// <value>Byte array.</value>
        public byte[] OwerSid { get; set; }

        /// <summary>
        /// Gets or sets bitmask indicating under what circumstances a notification event should be logged to the Microsoft Windows application log.
        /// </summary>
        /// <value>
        /// 0 = Never
        /// 1 = When the job succeeds
        /// 2 = When the job fails
        /// 3 = Whenever the job completes (regardless of the job outcome).
        /// </value>
        public int NotifyLevelEventLog { get; set; }

        /// <summary>
        /// Gets or sets bitmask indicating under what circumstances a notification e-mail should be sent when a job completes.
        /// </summary>
        /// <value>
        /// 0 = Never
        /// 1 = When the job succeeds
        /// 2 = When the job fails
        /// 3 = Whenever the job completes (regardless of the job outcome).
        /// </value>
        public int NotifyLevelEmail { get; set; }

        /// <summary>
        /// Gets or sets bitmask indicating under what circumstances a network message should be sent when a job completes.
        /// </summary>
        /// <value>
        /// 0 = Never
        /// 1 = When the job succeeds
        /// 2 = When the job fails
        /// 3 = Whenever the job completes (regardless of the job outcome).
        /// </value>
        public int NotifyLevelNetsend { get; set; }

        /// <summary>
        /// Gets or sets bitmask indicating under what circumstances a page should be sent when a job completes.
        /// </summary>
        /// <value>
        /// 0 = Never
        /// 1 = When the job succeeds
        /// 2 = When the job fails
        /// 3 = Whenever the job completes (regardless of the job outcome).
        /// </value>
        public int NotifyLevelPage { get; set; }

        /// <summary>
        /// Gets or sets email name of the operator to notify.
        /// </summary>
        /// <value>Operator object.</value>
        public Operator NotifyEmailOperator { get; set; }

        /// <summary>
        /// Gets or sets ID of the computer or user used when sending network messages.
        /// </summary>
        /// <value>Operator object.</value>
        public Operator NotifyNetsendOperator { get; set; }

        /// <summary>
        /// Gets or sets ID of the computer or user used when sending a page.
        /// </summary>
        /// <value>Operator object.</value>
        public Operator NotifyPageOperator { get; set; }

        /// <summary>
        /// Gets or sets bitmask indicating under what circumstances the job should be deleted when a job completes.
        /// </summary>
        /// <value>
        /// 0 = Never
        /// 1 = When the job succeeds
        /// 2 = When the job fails
        /// 3 = Whenever the job completes (regardless of the job outcome).
        /// </value>
        public int DeleteLevel { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public int VersionNumber { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
