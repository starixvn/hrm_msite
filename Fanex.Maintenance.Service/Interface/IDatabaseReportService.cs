// --------------------------------------------------------------------------
// <copyright file="IDatabaseReportService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.16">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Interface
{
    using System;
    using System.Data;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Database report service interface.
    /// </summary>
    public interface IDatabaseReportService
    { 
        DataTable GetUserOnlineDaily(DateTime date);

        DataTable GetUserOnline(int top);

        string GenerateTemplateUserDaily(DataTable data);

        string GenerateTemplateUser(DataTable data);

        string GenerateDataChart(DataTable dtb, int show);
    }
}