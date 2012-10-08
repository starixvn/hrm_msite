// --------------------------------------------------------------------------
// <copyright file="IJobRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.8">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Interface.JobManagement
{
    using System.Collections.Generic;
    using Fanex.Maintenance.Entity.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IJobRepository
    {
        IEnumerable<JobSchedule> GetAllJob(string serverName,string jobName,int? enabled,int? currentExecutionnStaus);
    }
}
