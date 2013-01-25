// --------------------------------------------------------------------------
// <copyright file="DatabaseServerGetAllCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.21">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.ConnectionString.Alias
{
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Maintenance.Core;
    using Fanex.NAP.Framework.Data;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class AliasGetSwitchConnectionCriteria : CriteriaBase
    {
        [Display(Name = "From_DbServer", ResourceType = typeof(ViewResource))]
        public int FromDbServerId { get; set; }

        [Display(Name = "To_DbServer", ResourceType = typeof(ViewResource))]
        public int ToDbServerId { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "ConnectionString_DatabaseServer_GetSwitchConnection";
        }
    }
}
