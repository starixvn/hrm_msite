// --------------------------------------------------------------------------
// <copyright file="ConnectionUpdateDefaultCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.22">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.ConnectionString.Connection
{
    using System;
    using Fanex.NAP.Framework.Data;

    public class ConnectionUpdateDefaultCriteria : CriteriaBase
    {
        public int AliasId { get; set; }

        public int ConnectionId { get; set; }

        public int Modifier { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "ConnectionString_ConnectionString_UpdateDefaultConnection";
        }
    }
}
