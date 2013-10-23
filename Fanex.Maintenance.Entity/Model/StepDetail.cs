// --------------------------------------------------------------------------
// <copyright file="StepDetail.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.5">Created</change>
// </history>

namespace Fanex.Maintenance.Entity.Model
{
    using System;
    using Fanex.Maintenance.Entity.Domain;

    /// <summary>
    /// Step detail.
    /// </summary>
    public class StepDetail : IEntity<int, string>
    {
        public Job Job { get; set; }

        public string SubSystem { get; set; }

        public string Command { get; set; }

        public string Flags { get; set; }

        public string AdditionalParameters { get; set; }

        /// <summary>
        /// Gets or sets error-level value returned by Command Execute subsystem steps to indicate success.
        /// </summary>
        /// <value>Return code value.</value>
        public int CmdExecSuccessCode { get; set; }

        /// <summary>
        /// Gets or sets action to be performed when a step is executed successfully.
        /// </summary>
        /// <value>Success action value.</value>
        public string OnSuccessAction { get; set; }

        /// <summary>
        /// Gets or sets ID of the next step to execute when a step is executed successfully.
        /// </summary>
        /// <value>Success step value.</value>
        public Step OnSuccessStep { get; set; }

        /// <summary>
        /// Gets or sets action to be performed when a step is not executed successfully.
        /// </summary>
        /// <value>Fail action value.</value>
        public string OnFailAction { get; set; }

        /// <summary>
        /// Gets or sets ID of the next step to execute when a step is not executed successfully.
        /// </summary>
        /// <value>Fail step value.</value>
        public Step OnFailStep { get; set; }

        public string Server { get; set; }

        public string DatabaseName { get; set; }

        public string DababaseUsername { get; set; }

        public int RetryAttempts { get; set; }

        public int RetryInterval { get; set; }

        public string OsRunPriority { get; set; }

        /// <summary>
        /// Gets or sets name of the file in which the step's output is saved when subsystem is TSQL, PowerShell, or command execute.
        /// </summary>
        /// <value>File name string.</value>
        public string OutputFileName { get; set; }

        /// <summary>
        /// Gets or sets outcome of the previous execution of the job step.
        /// </summary>
        /// <value>
        /// 0 = Failed
        /// 1 = Succeeded
        /// 2 = Retry
        /// 3 = Canceled
        /// 5 = Unknown.
        /// </value>
        public int LastRunOutCome { get; set; }

        public int LastRunDuration { get; set; }

        public int LastRunRetries { get; set; }

        public int LastRunDate { get; set; }

        public int LastRunTime { get; set; }

        public int ProxyId { get; set; }

        public Guid StepId { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
