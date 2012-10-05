// --------------------------------------------------------------------------
// <copyright file="Steps.cs" company="Nexcel Solutions Vietnam">
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
    /// TODO: Update summary.
    /// </summary>
    public class Steps : ISteps
    {
        public IJobs Job { get; set; }

        public string SubSystem { get; set; }

        public string Command { get; set; }

        public int Flags { get; set; }

        public string AdditionalParameters { get; set; }

        public int CmdExecSuccessCode { get; set; }

        public int OnSuccessAction { get; set; }

        public ISteps OnSuccessStep { get; set; }

        public int OnFailAction { get; set; }

        public ISteps OnFailStep { get; set; }

        public string Server { get; set; }

        public string DatabaseName { get; set; }

        public string DababaseUsername { get; set; }

        public int RetryAttempts { get; set; }

        public int RetryInterval { get; set; }

        public int OsRunPriority { get; set; }

        public string OutputFileName { get; set; }

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
