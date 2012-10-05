// -----------------------------------------------------------------------
// <copyright file="GetDepositDataToValidateLicenseeWLCriteria.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Maintenance.Criteria.Licensee
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.NAP.Framework.Data;


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


    public class GetCreditDataToValidateDWCriteria : GetCreditDataToValidateBettingCriteria
    {      
        public override string GetSettingKey()
        {
            return "GET_CreditDataToValidate_DW";
        }       
    }  
}
