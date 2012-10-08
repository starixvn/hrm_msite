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

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class GetListJobCriteria : CriteriaBase
    {
        [SpParam(Ignored = true)]
        public string Server { get; set; }

        public string Job_Name { get; set; }

        public int? Enabled { get; set; }

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
