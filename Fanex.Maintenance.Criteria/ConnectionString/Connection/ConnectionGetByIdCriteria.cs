// --------------------------------------------------------------------------
// <copyright file="ConnectionGetByIdCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.22">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.ConnectionString.Connection
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Alias get by ID.
    /// </summary>
    public class ConnectionGetByIdCriteria : CriteriaBase
    {
        public int ConnectionId { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "ConnectionString_ConnectionString_GetById";
        }
    }
}
