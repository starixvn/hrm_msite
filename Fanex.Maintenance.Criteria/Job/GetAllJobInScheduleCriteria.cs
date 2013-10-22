// --------------------------------------------------------------------------
// <copyright file="GetAllJobInScheduleCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.8">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.Job
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get all job in schedule.
    /// </summary>
    public class GetAllJobInScheduleCriteria : CriteriaBase
    {
        [SpParam(SqlParamName = "Schedule_Name")]
        public string ScheduleName { get; set; }

        [SpParam(SqlParamName = "Schedule_Id")]
        public int? ScheduleId { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "sp_help_jobs_in_schedule";
        }
    }
}
