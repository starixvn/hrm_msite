// --------------------------------------------------------------------------
// <copyright file="GetCpuUtilizationCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.24">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.Job
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class GetCpuUtilizationCriteria : CriteriaBase
    {
        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "int_Get_PerformanceCounter_CPU";
        }
    }
}
