// --------------------------------------------------------------------------
// <copyright file="AliasGetBySiteCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.17">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.ConnectionString.Alias
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Alias get by site criteria.
    /// </summary>
    public class AliasGetBySiteCriteria : CriteriaBase
    {
        public int SiteId { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "ConnectionString_Alias_GetBySite";
        }
    }
}
