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
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class StartJobCriteria : CriteriaBase
    {
        public string Job_Name { get; set; }

        /// <summary>
        /// Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.
        /// </summary>
        public int? Error_Flag { get; set; }

        /// <summary>
        /// The target server on which to start the job. server_name is nvarchar(128), with a default of NULL. server_name must be one of the target servers to which the job is currently targeted.
        /// </summary>
        public string Server_Name { get; set; }

        public string Step_Name { get; set; }

        /// <summary>
        /// Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.
        /// </summary>
        public int? Output_Flag { get; set; }

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