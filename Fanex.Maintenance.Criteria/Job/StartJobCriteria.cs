// --------------------------------------------------------------------------
// <copyright file="StartJobCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.15">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.Job
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class StartJobCriteria : CriteriaBase
    {
        [SpParam(SqlParamName = "Job_Name")]
        public string JobName { get; set; }

        [SpParam(SqlParamName = "Error_Flag")]
        public int? ErrorFlag { get; set; }

        [SpParam(SqlParamName = "Server_Name")]
        public string ServerName { get; set; }

        [SpParam(SqlParamName = "Step_Name")]
        public string StepName { get; set; }

        [SpParam(SqlParamName = "Output_Flag")]
        public int? OutputFlag { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "sp_start_job";
        }
    }
}