// --------------------------------------------------------------------------
// <copyright file="GetJobStepCriteria.cs" company="Nexcel Solutions Vietnam">
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

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class GetJobStepCriteria : CriteriaBase
    {
        public Guid? Job_Id { get; set; }

        public string Job_Name { get; set; }

        public int? Step_Id { get; set; }

        public int? Step_Name { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "sp_help_jobstep";
        }
    }
}
