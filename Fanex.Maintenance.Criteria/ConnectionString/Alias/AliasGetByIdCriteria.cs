// --------------------------------------------------------------------------
// <copyright file="AliasGetByIdCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.17">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.ConnectionString.Alias
{
    using Fanex.NAP.Framework.Data;
    using System.ComponentModel.DataAnnotations;
    using Fanex.Maintenance.Core;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AliasGetByIdCriteria : CriteriaBase
    {
        public int AliasId { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "ConnectionString_Alias_GetById";
        }
    }
}
