// --------------------------------------------------------------------------
// <copyright file="SiteUpdateStatusCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.21">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.ConnectionString.Site
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Update site status criteria.
    /// </summary>
    public class SiteUpdateStatusCriteria : CriteriaBase
    {
        public int SiteId { get; set; }

        public bool IsActive { get; set; }

        public int Modifier { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "ConnectionString_Site_UpdateStatus";
        }
    }
}
