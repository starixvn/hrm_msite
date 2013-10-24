// --------------------------------------------------------------------------
// <copyright file="ILogRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.28">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Interface.ConnectionString
{
    using System.Collections.Generic;
    using Fanex.Maintenance.Entity.Model.ConnectionString;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Log repository interface.
    /// </summary>
    public interface ILogRepository
    {
        IEnumerable<LogExecSp> Fetch(ICriteria criteria, out int totalRows);

        LogExecSp GetById(ICriteria criteria);
    }
}
