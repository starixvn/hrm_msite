// --------------------------------------------------------------------------
// <copyright file="GetAllMachinesCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.8.7">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.Log
{
    using System.Diagnostics.CodeAnalysis;
    using Fanex.NAP.Framework.Data;

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class GetAllMachinesCriteria : CriteriaBase
    {
        public override string GetSettingKey()
        {
            return "Logging_Machine_SelectAll";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}