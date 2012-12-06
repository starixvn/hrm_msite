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
        public Guid? Job_Id { get; set; }

        public string Job_Name { get; set; }

        public int? Run_Status { get; set; }

        public int? Start_Run_Date { get; set; }

        public int? End_Run_Date { get; set; }

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
