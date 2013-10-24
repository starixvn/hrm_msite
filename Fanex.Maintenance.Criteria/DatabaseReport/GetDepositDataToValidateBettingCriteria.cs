// --------------------------------------------------------------------------
// <copyright file="GetDepositDataToValidateBettingCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.12.6">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.DatabaseReport
{
    /// <summary>
    /// Get deposit data to validate betting criteria.
    /// </summary>
    public class GetDepositDataToValidateBettingCriteria : GetDepositDataToValidateLicenseeWLCriteria
    {
        public override string GetSettingKey()
        {
            return "GET_DepositDataToValidate_Betting";
        }
    }
}
