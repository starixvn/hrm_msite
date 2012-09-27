// -----------------------------------------------------------------------
// <copyright file="IDatabaseReportService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Maintenance.Core.Service.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data;
    using Fanex.NAP.Framework.Data;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IDatabaseReportService
    {
        DataTable GetSumWinloss(DateTime date, int drawFilter);

        DataTable GetLicenseeWinloss(DateTime date, int drawFilter);

        DataTable GetBetting(DateTime date, int drawFilter);

        DataTable GetDW(DateTime date, int drawFilter);

        DataTable GetBalanceUpDay(ICriteria criteria);

        DataTable CreditDataBetting(DateTime date, int drawFilter);

        DataTable CreditDataDW(DateTime date, int drawFilter);

        DataTable GetUserOnlineDaily(DateTime date);

        DataTable GetUserOnline(int top);

        string GenerateTemplateHtmlLicensee(DateTime date, int drawFilter);

        string GenerateTemplateHtmlCredit(DateTime date, int drawFilter);

        string GenerateTemplateUserDaily(DataTable data);

        string GenerateTemplateUser(DataTable data,int show);

        string GenerateDataChart(DataTable dtb, int show);
    }
}
