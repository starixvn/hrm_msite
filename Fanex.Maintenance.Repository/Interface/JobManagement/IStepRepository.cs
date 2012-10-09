// --------------------------------------------------------------------------
// <copyright file="IStep.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.9">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Interface.JobManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Maintenance.Entity.Domain;
    using Fanex.Maintenance.Entity.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IStepRepository
    {
        IEnumerable<StepDetail> GetStepByJob(string serverName, string jobName);
    }
}
