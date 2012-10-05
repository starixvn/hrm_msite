// -----------------------------------------------------------------------
// <copyright file="GetUserOnlineCriteria.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Maintenance.Criteria.User
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class GetUserOnlineCriteria : CriteriaBase
    {
        public int Top { get; set; }

        public override string GetSettingKey()
        {
            return "Is_SelTotalOnlineUser";
        }

        public override bool IsValid()
        {
            return true;
        }
    }

    public class GetUserOnlineDailyCriteria : CriteriaBase
    {      

        public DateTime Date { get; set; }

        public override string GetSettingKey()
        {
            return "Is_OnlineReportDaily";
        }
    }
}
