// --------------------------------------------------------------------------
// <copyright file="GetBalanceUpDayCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.12.6">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.DatabaseReport
{
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
