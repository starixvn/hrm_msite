// --------------------------------------------------------------------------
// <copyright file="GetCreditDataToValidateDWCriteria.cs" company="Nexcel Solutions Vietnam">
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
    /// Get credit data to validate DW.
    /// </summary>
    public class GetCreditDataToValidateDWCriteria : GetCreditDataToValidateBettingCriteria
    {
        public override string GetSettingKey()
        {
            return "GET_CreditDataToValidate_DW";
        }
    }
}
