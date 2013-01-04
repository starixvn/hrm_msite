// --------------------------------------------------------------------------
// <copyright file="GetSqlServerLogCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.10">Created</change>
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
    public class GetReadSQLLogCriteria : CriteriaBase
    {
        [SpParam(Ignored = true)]
        public string Server { get; set; }
                
        [Display(Name = "From", ResourceType = typeof(ViewResource))]
        public DateTime FromDate { get; set; }
                
        [Display(Name = "To", ResourceType = typeof(ViewResource))]
        public DateTime ToDate { get; set; }
                
        [Display(Name = "IsAgent", ResourceType = typeof(ViewResource))]
        public bool IsAgent { get; set; }

        [SpParam(Ignored = true)]
        public int? ErrorLevel { get; set; }

        [SpParam(Ignored = true)]
        public int? PageSize { get; set; }

        [SpParam(Ignored = true)]
        public int? PageIndex { get; set; }        

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "int_ReadSQLLog";
        }
    }
}
