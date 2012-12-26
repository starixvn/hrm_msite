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
    /// TODO: Update summary.
    /// </summary>
    public interface IDatabaseReportService
    { 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        DataTable GetUserOnlineDaily(DateTime date);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        DataTable GetUserOnline(int top);        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string GenerateTemplateUserDaily(DataTable data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="show"></param>
        /// <returns></returns>
        string GenerateTemplateUser(DataTable data, int show);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtb"></param>
        /// <param name="show"></param>
        /// <returns></returns>
        string GenerateDataChart(DataTable dtb, int show);
    }
}