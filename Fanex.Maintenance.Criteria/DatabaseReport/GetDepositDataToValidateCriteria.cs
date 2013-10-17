// --------------------------------------------------------------------------
// <copyright file="GetDepositDataToValidateCriteria.cs" company="Nexcel Solutions Vietnam">
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
    /// Get deposit data to validate licensee winloss criteria.
    /// </summary>
    public class GetDepositDataToValidateLicenseeWLCriteria : CriteriaBase
    {
        public DateTime Date { get; set; }

        public int DrawFilter { get; set; }

        public override string GetSettingKey()
        {
            return "GET_DepositDataToValidate_LicenseeWL";
        }

        public override bool IsValid()
        {
            return true;
        }
    }

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

    /// <summary>
    /// Get deposit data to validate sum winloss old criteria.
    /// </summary>
    public class GetDepositDataToValidateSumWLOldCriteria : GetDepositDataToValidateLicenseeWLCriteria
    {
        public override string GetSettingKey()
        {
            return "GET_DepositDataToValidate_SumWLOld";
        }
    }

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

    /// <summary>
    /// Get deposit data to validate data warehouse criteria.
    /// </summary>
    public class GetDepositDataToValidateDWCriteria : GetDepositDataToValidateLicenseeWLCriteria
    {
        public override string GetSettingKey()
        {
            return "GET_DepositDataToValidate_DW";
        }
    }
}
