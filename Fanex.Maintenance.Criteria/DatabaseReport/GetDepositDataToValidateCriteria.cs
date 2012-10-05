// -----------------------------------------------------------------------
// <copyright file="GetDepositDataToValidateLicenseeWLCriteria.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Maintenance.Criteria.DatabaseReport
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.NAP.Framework.Data;


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


    public class GetDepositDataToValidateSumWLCriteria : GetDepositDataToValidateLicenseeWLCriteria
    {      
        public override string GetSettingKey()
        {
            return "GET_DepositDataToValidate_SumWL";
        }       
    }

    public class GetDepositDataToValidateSumWLOldCriteria : GetDepositDataToValidateLicenseeWLCriteria
    {       
        public override string GetSettingKey()
        {
            return "GET_DepositDataToValidate_SumWLOld";
        }       
    }

    public class GetDepositDataToValidateBettingCriteria : GetDepositDataToValidateLicenseeWLCriteria
    {       
        public override string GetSettingKey()
        {
            return "GET_DepositDataToValidate_Betting";
        }       
    }

    public class GetDepositDataToValidateDWCriteria : GetDepositDataToValidateLicenseeWLCriteria
    {       
        public override string GetSettingKey()
        {
            return "GET_DepositDataToValidate_DW";
        }
    }
}
