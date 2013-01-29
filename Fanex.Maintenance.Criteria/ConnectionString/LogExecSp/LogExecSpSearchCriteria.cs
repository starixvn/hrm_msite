// --------------------------------------------------------------------------
// <copyright file="LogExecSpSearchCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.28">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.ConnectionString.LogExecSp
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Maintenance.Core;
    using Fanex.NAP.Framework.Data;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class LogExecSpSearchCriteria : CriteriaBase
    {
        public string Username { get; set; }

        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM dd,yyyy}", ApplyFormatInEditMode = true)]
        public DateTime From { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM dd,yyyy}", ApplyFormatInEditMode = true)]
        public DateTime To { get; set; }

        [Display(Name = "Filter", ResourceType = typeof(ViewResource))]
        public int FilterType { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalRows { get; set; }

        public override string GetSettingKey()
        {
            return "ConnectionString_LogExecSp_Search";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
