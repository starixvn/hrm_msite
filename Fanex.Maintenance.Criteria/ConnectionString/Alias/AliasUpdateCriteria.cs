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
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Fanex.Maintenance.Core;
    using Fanex.NAP.Framework.Data;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class AliasUpdateCriteria : CriteriaBase
    {
        private readonly string _formatXml = "<Sites SiteId='{0}'/>";

        private string _name;

        private string _description;

        public int AliasId { get; set; }

        [Required]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = !string.IsNullOrWhiteSpace(value) ? value.Trim() : string.Empty;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = !string.IsNullOrWhiteSpace(value) ? value.Trim() : string.Empty;
            }
        }

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
                        xmlSite.Append(string.Format(_formatXml, item.ToString()));
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
