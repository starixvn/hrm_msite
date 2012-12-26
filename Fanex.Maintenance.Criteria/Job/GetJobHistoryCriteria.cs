// --------------------------------------------------------------------------
// <copyright file="GetJobHistoryCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.9">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.Job
{
    using System;
    using Fanex.NAP.Framework.Data;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class GetJobHistoryCriteria : CriteriaBase
    {
        [SpParam(SqlParamName = "Job_Id")]
        public Guid? JobId { get; set; }

        [SpParam(SqlParamName = "Job_Name")]
        public string JobName { get; set; }

        [SpParam(SqlParamName = "Run_Status")]
        public int? RunStatus { get; set; }

        [SpParam(SqlParamName = "Start_Run_Date")]
        public int? StartRunDate { get; set; }

        [SpParam(SqlParamName = "End_Run_Date")]
        public int? EndRunDate { get; set; }

        public string Mode { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "sp_help_jobhistory";
        }
    }
}
