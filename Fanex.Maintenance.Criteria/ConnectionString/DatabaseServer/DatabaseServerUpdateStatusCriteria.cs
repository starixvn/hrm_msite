// --------------------------------------------------------------------------
// <copyright file="DatabaseServerUpdateStatusCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.21">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.ConnectionString.DatabaseServer
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Update database server status criteria.
    /// </summary>
    public class DatabaseServerUpdateStatusCriteria : CriteriaBase
    {
        public int DbServerId { get; set; }

        public bool IsActive { get; set; }

        public int Modifier { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "ConnectionString_DatabaseServer_UpdateStatus";
        }
    }
}
