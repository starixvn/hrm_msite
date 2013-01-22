// --------------------------------------------------------------------------
// <copyright file="AliasGetAllCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.17">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.ConnectionString.Alias
{
    using Fanex.NAP.Framework.Data;
    using System.ComponentModel.DataAnnotations;
    using Fanex.Maintenance.Core;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AliasGetAllCriteria : CriteriaBase
    {
        [Display(Name = "Database_Server", ResourceType = typeof(ViewResource))]
        public int? DbServerId { get; set; }

        [Display(Name = "Site", ResourceType = typeof(ViewResource))]
        public int? SiteId { get; set; }

        [Display(Name = "Name", ResourceType = typeof(ViewResource))]
        public string Alias { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "ConnectionString_Alias_GetAll";
        }
    }
}
