// --------------------------------------------------------------------------
// <copyright file="DatabaseServerGetSwitchSecondConnectionCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.21">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.ConnectionString.DatabaseServer
{
    using System.ComponentModel.DataAnnotations;
    using Fanex.Maintenance.Core;
    using Fanex.NAP.Framework.Data;

    public class DatabaseServerGetSecondConnectionFromDbServerCriteria : CriteriaBase
    {
        [Display(Name = "Database_Server", ResourceType = typeof(ViewResource))]
        public int DbServerId { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "ConnectionString_DatabaseServer_GetSecondConnectionFromDbServer";
        }
    }
}
