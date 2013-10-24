// --------------------------------------------------------------------------
// <copyright file="IStepRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.9">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Interface.JobManagement
{
    using System.Collections.Generic;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Step repository.
    /// </summary>
    public interface IStepRepository
    {
        IEnumerable<StepDetail> GetStepByJob(ICriteria criteria, string serverName);
    }
}
