// --------------------------------------------------------------------------
// <copyright file="DatabaseServerGetAllCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.21">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.ConnectionString.DatabaseServer
{
    using System.Diagnostics.CodeAnalysis;
    using Fanex.NAP.Framework.Data;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class DatabaseServerGetAllCriteria : CriteriaBase
    {
        public string Name { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "ConnectionString_DatabaseServer_GetAll";
        }
    }
}
