// --------------------------------------------------------------------------
// <copyright file="IStepService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.9">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Interface.JobManagement
{
    using System.Collections.Generic;
    using Fanex.Maintenance.Entity.Model;

    /// <summary>
    /// Step service interface.
    /// </summary>
    public interface IStepService
    {
        IStepService FromServer(string serverName);

        IStepService WithJob(string jobName);

        IEnumerable<StepDetail> GetSteps();
    }
}
