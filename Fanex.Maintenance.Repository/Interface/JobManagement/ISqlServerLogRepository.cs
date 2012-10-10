// --------------------------------------------------------------------------
// <copyright file="ISqlServerLogRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.10">Created</change>
// </history>


namespace Fanex.Maintenance.Repository.Interface.JobManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
using Fanex.Maintenance.Entity.Model;
using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ISqlServerLogRepository
    {
        IEnumerable<SqlServerLog> GetAllSqlServerLogOnDate(ICriteria sqlServerLogCriteria, string serverIp);
    }
}
