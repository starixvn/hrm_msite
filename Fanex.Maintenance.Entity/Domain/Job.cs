// --------------------------------------------------------------------------
// <copyright file="IJobs.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.5">Created</change>
// </history>

namespace Fanex.Maintenance.Entity.Domain
{
    using System;

    public interface Job : IEntity<int, string>
    {
        Guid JobId { get; set; }

        OrinatingServer OrinatingServer { get; set; }

        int Enable { get; set; }

        string Description { get; set; }

        /// <summary>
        /// ID of the step in the job where execution should begin.
        /// </summary>
        int StartStepId { get; set; }

        JobCategory Category { get; set; }

        /// <summary>
        /// Security identifier number (SID) of the job owner.
        /// </summary>
        byte[] OwerSid { get; set; }

        /// <summary>
        ///  Bitmask indicating under what circumstances a notification event should be logged to the Microsoft Windows application log:
        ///0 = Never
        ///1 = When the job succeeds
        ///2 = When the job fails
        ///3 = Whenever the job completes (regardless of the job outcome)
        /// </summary>
        int NotifyLevelEventLog { get; set; }

        /// <summary>
        /// Bitmask indicating under what circumstances a notification e-mail should be sent when a job completes:
        ///0 = Never
        ///1 = When the job succeeds
        ///2 = When the job fails
        ///3 = Whenever the job completes (regardless of the job outcome)
        /// </summary>
        int NotifyLevelEmail { get; set; }

        /// <summary>
        /// Bitmask indicating under what circumstances a network message should be sent when a job completes:
        ///0 = Never
        ///1 = When the job succeeds
        ///2 = When the job fails
        ///3 = Whenever the job completes (regardless of the job outcome)
        /// </summary>
        int NotifyLevelNetsend { get; set; }

        /// <summary>
        /// Bitmask indicating under what circumstances a page should be sent when a job completes:
        ///0 = Never
        ///1 = When the job succeeds
        ///2 = When the job fails
        ///3 = Whenever the job completes (regardless of the job outcome)
        /// </summary>
        int NotifyLevelPage { get; set; }

        /// <summary>
        /// E-mail name of the operator to notify.
        /// </summary>
        Operator NotifyEmailOperator { get; set; }

        /// <summary>
        /// ID of the computer or user used when sending network messages.
        /// </summary>
        Operator NotifyNetsendOperator { get; set; }

        /// <summary>
        /// ID of the computer or user used when sending a page.
        /// </summary>
        Operator NotifyPageOperator { get; set; }

        /// <summary>
        ///  Bitmask indicating under what circumstances the job should be deleted when a job completes:
        ///0 = Never
        ///1 = When the job succeeds
        ///2 = When the job fails
        ///3 = Whenever the job completes (regardless of the job outcome)
        /// </summary>
        int DeleteLevel { get; set; }

        DateTime DateCreated { get; set; }

        DateTime DateModified { get; set; }

        int VersionNumber { get; set; }
    }
}
