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
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class GetAllJobInScheduleCriteria : CriteriaBase
    {
        public string Schedule_Name { get; set; }

        public int? Schedule_Id { get; set; }

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
