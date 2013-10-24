// --------------------------------------------------------------------------
// <copyright file="GetCreditDataToValidateBettingCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.12.6">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.DatabaseReport
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get credit data to validate betting.
    /// </summary>
    public class GetCreditDataToValidateBettingCriteria : CriteriaBase
    {
        public DateTime Date { get; set; }

        public int DrawFilter { get; set; }

        public override string GetSettingKey()
        {
            return "GET_CreditDataToValidate_Betting";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
