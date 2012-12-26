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
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
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

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class GetDepositDataToValidateSumWLCriteria : GetDepositDataToValidateLicenseeWLCriteria
    {
        public override string GetSettingKey()
        {
            return "GET_DepositDataToValidate_SumWL";
        }
    }

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class GetDepositDataToValidateSumWLOldCriteria : GetDepositDataToValidateLicenseeWLCriteria
    {
        public override string GetSettingKey()
        {
            return "GET_DepositDataToValidate_SumWLOld";
        }
    }

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class GetDepositDataToValidateBettingCriteria : GetDepositDataToValidateLicenseeWLCriteria
    {
        public override string GetSettingKey()
        {
            return "GET_DepositDataToValidate_Betting";
        }
    }

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class GetDepositDataToValidateDWCriteria : GetDepositDataToValidateLicenseeWLCriteria
    {
        public override string GetSettingKey()
        {
            return "GET_DepositDataToValidate_DW";
        }
    }
}
