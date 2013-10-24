// --------------------------------------------------------------------------
// <copyright file="SiteGetByDbServerCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.21">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.ConnectionString.Site
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get site by DB server criteria.
    /// </summary>
    public class SiteGetByDbServerCriteria : CriteriaBase
    {
        public int DbServerId { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "ConnectionString_Site_GetByDbServer";
        }
    }
}
