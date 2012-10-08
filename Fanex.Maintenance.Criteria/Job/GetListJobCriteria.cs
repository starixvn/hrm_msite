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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.NAP.Framework.Data;
    using System.ComponentModel.DataAnnotations;
    using Fanex.Maintenance.Core;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class GetListJobCriteria : CriteriaBase
    {
        [SpParam(Ignored = true)]
        [Display(Name = "Server", ResourceType = typeof(ViewResource))]
        public string Server { get; set; }

        [SpParam(Ignored = true)]
        [Display(Name = "Date_Type", ResourceType = typeof(ViewResource))]
        public int? DateType { get; set; }

        [SpParam(Ignored = true)]
        [Display(Name = "From", ResourceType = typeof(ViewResource))]
        public DateTime? From { get; set; }

        [SpParam(Ignored = true)]
        [Display(Name = "To", ResourceType = typeof(ViewResource))]
        public DateTime? To { get; set; }

        [Display(Name = "Job", ResourceType = typeof(ViewResource))]
        public string Job_Name { get; set; }

        [Display(Name = "Status", ResourceType = typeof(ViewResource))]
        public int? Enabled { get; set; }

        [Display(Name = "Execution_Status", ResourceType = typeof(ViewResource))]
        public int? Execution_Status { get; set; }

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
