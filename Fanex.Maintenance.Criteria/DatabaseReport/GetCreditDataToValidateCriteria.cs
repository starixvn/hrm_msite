// --------------------------------------------------------------------------
// <copyright file="GetCreditDataToValidateCriteria.cs" company="Nexcel Solutions Vietnam">
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

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class GetCreditDataToValidateDWCriteria : GetCreditDataToValidateBettingCriteria
    {      
        public override string GetSettingKey()
        {
            return "GET_CreditDataToValidate_DW";
        }       
    }  
}
