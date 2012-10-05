// --------------------------------------------------------------------------
// <copyright file="Jobs.cs" company="Nexcel Solutions Vietnam">
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
    public class Jobs : IJobs
    {
        public Guid JobId { get; set; }

        public IOrinatingServers OrinatingServer { get; set; }

        public int Enable { get; set; }

        public string Description { get; set; }

        public int StartStepId { get; set; }

        public IJobCategories Category { get; set; }

        public byte[] OwerSid { get; set; }

        public int NotifyLevelEventLog { get; set; }

        public int NotifyLevelEmail { get; set; }

        public int NotifyLevelNetsend { get; set; }

        public int NotifyLevelPage { get; set; }

        public IOperators NotifyEmailOperator { get; set; }

        public IOperators NotifyNetsendOperator { get; set; }

        public IOperators NotifyPageOperator { get; set; }

        public int DeleteLevel { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public int VersionNumber { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
