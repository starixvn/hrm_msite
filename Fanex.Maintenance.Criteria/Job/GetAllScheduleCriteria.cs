// --------------------------------------------------------------------------
// <copyright file="GetAllScheduleCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.8">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.Job
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Fanex.Maintenance.Core;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class GetAllScheduleCriteria : CriteriaBase
    {
        [SpParam(Ignored = true)]
        public string Server { get; set; }

        [SpParam(Ignored = true)]
        public int? FreqType { get; set; }

        [SpParam(Ignored = true)]
        [Display(Name = "FrequentSubType", ResourceType = typeof(ViewResource))]
        public int? FreqSubType { get; set; }

        [SpParam(Ignored = true)]
        [Display(Name = "From", ResourceType = typeof(ViewResource))]
        public DateTime From { get; set; }

        [SpParam(Ignored = true)]
        [Display(Name = "To", ResourceType = typeof(ViewResource))]
        public DateTime To { get; set; }

        [SpParam(Ignored = true)]
        public int? SearchDateType { get; set; }

        [SpParam(SqlParamName = "Schedule_Id")]
        public int? ScheduleId { get; set; }

        [SpParam(SqlParamName = "Schedule_Name")]
        public string ScheduleName { get; set; }

        [SpParam(SqlParamName = "Attached_Schedules_Only")]
        public int AttachedSchedulesOnly { get; set; }

        [SpParam(SqlParamName = "Include_Description")]
        public int IncludeDescription { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "sp_help_schedule";
        }
    }
}