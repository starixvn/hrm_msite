// --------------------------------------------------------------------------
// <copyright file="GetMachineByIdCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.8.9">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.Log
{
    using System.Diagnostics.CodeAnalysis;
    using Fanex.NAP.Framework.Data;

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class GetMachineByIdCriteria : CriteriaBase
    {
        public int MachineId { get; set; }

        public override string GetSettingKey()
        {
            return "Logging_Machine_ById";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}