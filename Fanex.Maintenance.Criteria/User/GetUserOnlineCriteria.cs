// --------------------------------------------------------------------------
// <copyright file="GetUserOnlineCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.12.6">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.User
{
    using System;
    using Fanex.NAP.Framework.Data;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class GetUserOnlineCriteria : CriteriaBase
    {
        public int Top { get; set; }

        public override string GetSettingKey()
        {
            return "Is_SelTotalOnlineUser";
        }

        public override bool IsValid()
        {
            return true;
        }
    }

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class GetUserOnlineDailyCriteria : CriteriaBase
    {    
        public DateTime Date { get; set; }

        public override string GetSettingKey()
        {
            return "Is_OnlineReportDaily";
        }
    }
}
