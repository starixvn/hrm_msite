// --------------------------------------------------------------------------
// <copyright file="GetListJobCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.8">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.Job
{
    using System;
    using Fanex.NAP.Framework.Data;
    using System.ComponentModel.DataAnnotations;
    using Fanex.Maintenance.Core;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class GetListJobCriteria : CriteriaBase
    {
        [SpParam(Ignored = true)]
        [Display(Name = "Server", ResourceType = typeof(ViewResource))]
        public string Server { get; set; }

        [SpParam(Ignored = true)]
        [Display(Name = "Schedule", ResourceType = typeof(ViewResource))]
        public string Schedule { get; set; }

        [SpParam(Ignored = true)]
        [Display(Name = "Schedule", ResourceType = typeof(ViewResource))]
        public int? ScheduleId { get; set; }

        [SpParam(Ignored = true)]
        [Display(Name = "Date_Type", ResourceType = typeof(ViewResource))]
        public int? DateType { get; set; }

        [SpParam(Ignored = true)]
        [Display(Name = "From", ResourceType = typeof(ViewResource))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMMM dd,yyyy}")]
        public DateTime? From { get; set; }

        [SpParam(Ignored = true)]
        [Display(Name = "To", ResourceType = typeof(ViewResource))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMMM dd,yyyy}")]
        public DateTime? To { get; set; }

        [Display(Name = "Job", ResourceType = typeof(ViewResource))]
        [SpParam(SqlParamName = "Job_Name")]
        public string JobName { get; set; }

        [Display(Name = "Status", ResourceType = typeof(ViewResource))]
        public int? Enabled { get; set; }

        [Display(Name = "Execution_Status", ResourceType = typeof(ViewResource))]
        [SpParam(SqlParamName = "Execution_Status")]
        public int? ExecutionStatus { get; set; }

        [SpParam(SqlParamName = "Job_Aspect")]
        public string JobAspect { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "sp_help_job";
        }
    }
}
