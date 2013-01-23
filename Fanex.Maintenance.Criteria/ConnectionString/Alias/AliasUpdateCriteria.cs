// --------------------------------------------------------------------------
// <copyright file="AliasUpdateStatusCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.17">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.ConnectionString.Alias
{
    using System;
    using System.Text;
    using Fanex.NAP.Framework.Data;
    using System.ComponentModel.DataAnnotations;
    using Fanex.Maintenance.Core;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AliasUpdateCriteria : CriteriaBase
    {
        private readonly string Format = "<Sites SiteId='{0}'/>";

        public int AliasId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(ViewResource))]
        public bool IsActive { get; set; }

        public int Modifier { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string Sites
        {
            get
            {
                var xmlSite = new StringBuilder("<ROOT>");
                if (!ReferenceEquals(SiteId, null))
                {
                    foreach (var item in SiteId)
                    {
                        xmlSite.Append(string.Format(Format, item.ToString()));
                    }
                }
                return xmlSite.Append("</ROOT>").ToString();
            }
        }

        [SpParam(Ignored = true)]
        public int[] SiteId { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "ConnectionString_Alias_Update";
        }
    }
}
