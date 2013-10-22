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
        /// <summary>
        /// ID of the job.
        /// </summary>
        public Job Job { get; set; }

        /// <summary>
        /// Name of the subsystem used by SQL Server Agent to execute the job step.
        /// </summary>
        public string SubSystem { get; set; }

        /// <summary>
        /// Command to be executed by subsystem.
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// Reserved.
        /// </summary>
        public string Flags { get; set; }

        /// <summary>
        /// Reserved.
        /// </summary>
        public string AdditionalParameters { get; set; }

        /// <summary>
        /// Error-level value returned by Command Execute subsystem steps to indicate success.
        /// </summary>
        public int CmdExecSuccessCode { get; set; }

        /// <summary>
        /// Action to be performed when a step is executed successfully.
        /// </summary>
        public string OnSuccessAction { get; set; }

        /// <summary>
        /// ID of the next step to execute when a step is executed successfully.
        /// </summary>
        public Step OnSuccessStep { get; set; }

        /// <summary>
        /// Action to be performed when a step is not executed successfully.
        /// </summary>
        public string OnFailAction { get; set; }

        /// <summary>
        /// ID of the next step to execute when a step is not executed successfully.
        /// </summary>
        public Step OnFailStep { get; set; }

        /// <summary>
        /// Reserved.
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Name of the database in which command is executed if subsystem is TSQL.
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Name of the database user whose account will be used when executing the step.
        /// </summary>
        public string DababaseUsername { get; set; }

        /// <summary>
        /// Number of retry attempts made if the step fails.
        /// </summary>
        public int RetryAttempts { get; set; }

        /// <summary>
        /// Amount of time to wait between retry attempts. 
        /// </summary>
        public int RetryInterval { get; set; }

        /// <summary>
        /// Reserved.
        /// </summary>
        public string OsRunPriority { get; set; }

        /// <summary>
        /// Name of the file in which the step's output is saved when subsystem is TSQL, PowerShell, or command execute.
        /// </summary>
        public string OutputFileName { get; set; }

        /// <summary>
        /// Outcome of the previous execution of the job step.
        /// 0 = Failed
        /// 1 = Succeeded
        /// 2 = Retry
        /// 3 = Canceled
        /// 5 = Unknown.
        /// </summary>
        public int LastRunOutCome { get; set; }

        /// <summary>
        /// Duration (hhmmss) of the step the last time it ran.
        /// </summary>
        public int LastRunDuration { get; set; }

        /// <summary>
        /// Number of retry attempts in the last execution of the job step.
        /// </summary>
        public int LastRunRetries { get; set; }

        /// <summary>
        /// Date (yyyymmdd) the step last started execution.
        /// </summary>
        public int LastRunDate { get; set; }

        /// <summary>
        /// Time (hhmmss) the step last started execution.
        /// </summary>
        public int LastRunTime { get; set; }

        /// <summary>
        /// Proxy for the job step.
        /// </summary>
        public int ProxyId { get; set; }

        /// <summary>
        /// Identifier for the job step. 
        /// </summary>
        public Guid StepId { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
