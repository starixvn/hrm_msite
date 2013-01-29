// --------------------------------------------------------------------------
// <copyright file="ILogService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.28">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Interface.ConnectionString
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.ConnectionString.Domain;
    using Fanex.Maintenance.Entity.Model.ConnectionString;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ILogService
    {
        IQueryable<LogExecSp> SearchExecSp(
          int pageIndex,
          int pageSize,
          out int totalRows,
          string name,
          string username,
          DateTime from,
          DateTime to,
          int filterBy = 0);

        LogExecSp GetById(int logId);

        List<LogData> GetValueAliasInsert(LogExecSp logExecSp, ref List<LogData> listSite);

        List<LogData> GetValueAliasUpdate(LogExecSp logExecSp, ref List<LogData> listSite, ref List<LogData> listOldSite);

        List<LogData> GetValueUpdateStatus(LogExecSp logExecSp);

        List<LogData> GetValueConnectionInsert(LogExecSp logExecSp);

        List<LogData> GetValueConnectionUpdate(LogExecSp logExecSp);        

        List<LogData> GetValueDatabaseServerInsert(LogExecSp logExecSp);

        List<LogData> GetValueDatabaseServerUpdate(LogExecSp logExecSp);

        List<LogData> GetValueSiteInsert(LogExecSp logExecSp);

        List<LogData> GetValueSiteUpdate(LogExecSp logExecSp);

        List<ConnectionString> GetValueChangeConnection(LogExecSp logExecSp, ref List<ConnectionString> listOld);
    }
}
