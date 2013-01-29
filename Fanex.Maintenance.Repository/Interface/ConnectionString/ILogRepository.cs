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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.NAP.Framework.Data;
    using Fanex.Maintenance.Entity.Model.ConnectionString;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ILogRepository
    {
        IEnumerable<LogExecSp> Fetch(ICriteria criteria, out int totalRows);

        LogExecSp GetById(ICriteria criteria);
    }
}
