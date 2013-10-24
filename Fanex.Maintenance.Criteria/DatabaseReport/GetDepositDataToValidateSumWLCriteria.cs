// --------------------------------------------------------------------------
// <copyright file="GetDepositDataToValidateSumWLCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.12.6">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.DatabaseReport
{
    /// <summary>
    /// Get deposit data to validate sum winloss criteria.
    /// </summary>
    public class GetDepositDataToValidateSumWLCriteria : GetDepositDataToValidateLicenseeWLCriteria
    {
        public override string GetSettingKey()
        {
            return "GET_DepositDataToValidate_SumWL";
        }
    }
}
