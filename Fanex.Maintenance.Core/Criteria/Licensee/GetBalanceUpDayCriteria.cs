// -----------------------------------------------------------------------
// <copyright file="BalanceUpDayCriteria.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Maintenance.Core.Criteria.Licensee
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class GetBalanceUpDayCriteria : CriteriaBase
    {
        public override string GetSettingKey()
        {
            return "Lrf_SelBalanceUpDay";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
