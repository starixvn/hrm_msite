// --------------------------------------------------------------------------
// <copyright file="ISteps.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.5">Created</change>
// </history>

namespace Fanex.Maintenance.Entity.Domain
{
    using System;

    public interface Step : IEntity<int, string>
    {
        /// <summary>
        /// ID of the job.
        /// </summary>
        Job Job { get; set; }

        /// <summary>
        /// Name of the subsystem used by SQL Server Agent to execute the job step.
        /// </summary>
        string SubSystem { get; set; }

        /// <summary>
        /// Command to be executed by subsystem.
        /// </summary>
        string Command { get; set; }

        /// <summary>
        /// Reserved.
        /// </summary>
        int Flags { get; set; }

        /// <summary>
        /// Reserved.
        /// </summary>
        string AdditionalParameters { get; set; }

        /// <summary>
        /// Error-level value returned by CmdExec subsystem steps to indicate success.
        /// </summary>
        int CmdExecSuccessCode { get; set; }

        /// <summary>
        /// Action to be performed when a step is executed successfully.
        /// </summary>
        int OnSuccessAction { get; set; }

        /// <summary>
        /// ID of the next step to execute when a step is executed successfully.
        /// </summary>
        Step OnSuccessStep { get; set; }

        /// <summary>
        /// Action to be performed when a step is not executed successfully.
        /// </summary>
        int OnFailAction { get; set; }

        /// <summary>
        /// ID of the next step to execute when a step is not executed successfully.
        /// </summary>
        Step OnFailStep { get; set; }

        /// <summary>
        /// Reserved.
        /// </summary>
        string Server { get; set; }

        /// <summary>
        /// Name of the database in which command is executed if subsystem is TSQL.
        /// </summary>
        string DatabaseName { get; set; }

        /// <summary>
        /// Name of the database user whose account will be used when executing the step.
        /// </summary>
        string DababaseUsername { get; set; }

        /// <summary>
        /// Number of retry attempts made if the step fails.
        /// </summary>
        int RetryAttempts { get; set; }

        /// <summary>
        /// Amount of time to wait between retry attempts. 
        /// </summary>
        int RetryInterval { get; set; }

        /// <summary>
        /// Reserved.
        /// </summary>
        int OsRunPriority { get; set; }

        /// <summary>
        /// Name of the file in which the step's output is saved when subsystem is TSQL, PowerShell, or CmdExec.
        /// </summary>
        string OutputFileName { get; set; }

        /// <summary>
        /// Outcome of the previous execution of the job step.
        ///0 = Failed
        ///1 = Succeeded
        ///2 = Retry
        ///3 = Canceled
        ///5 = Unknown
        /// </summary>
        int LastRunOutCome { get; set; }

        /// <summary>
        /// Duration (hhmmss) of the step the last time it ran.
        /// </summary>
        int LastRunDuration { get; set; }

        /// <summary>
        /// Number of retry attempts in the last execution of the job step.
        /// </summary>
        int LastRunRetries { get; set; }

        /// <summary>
        /// Date (yyyymmdd) the step last started execution.
        /// </summary>
        int LastRunDate { get; set; }

        /// <summary>
        /// Time (hhmmss) the step last started execution.
        /// </summary>
        int LastRunTime { get; set; }

        /// <summary>
        /// Proxy for the job step.
        /// </summary>
        int ProxyId { get; set; }

        /// <summary>
        /// Identifier for the job step. 
        /// </summary>
        Guid StepId { get; set; }
    }
}
