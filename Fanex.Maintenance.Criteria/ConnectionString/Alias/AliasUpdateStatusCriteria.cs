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
    public class AliasUpdateStatusCriteria : CriteriaBase
    {        
        public int AliasId { get; set; }

        public bool IsActive { get; set; }
        
        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "ConnectionString_Alias_UpdateStatus";
        }
    }
}
