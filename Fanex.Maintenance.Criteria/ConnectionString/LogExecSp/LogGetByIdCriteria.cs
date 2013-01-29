// --------------------------------------------------------------------------
// <copyright file="LogGetByIdCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.28">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.ConnectionString.LogExecSp
{
    using System.Diagnostics.CodeAnalysis;
    using Fanex.NAP.Framework.Data;

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class LogGetByIdCriteria : CriteriaBase
    {
        public int LogId { get; set; }

        public override string GetSettingKey()
        {
            return "ConnectionString_LogExecSp_GetById";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
