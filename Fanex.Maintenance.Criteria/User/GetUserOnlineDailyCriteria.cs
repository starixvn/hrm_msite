// --------------------------------------------------------------------------
// <copyright file="GetUserOnlineDailyCriteria.cs" company="Nexcel Solutions Vietnam">
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

    /// <summary>
    /// Get user online daily criteria.
    /// </summary>
    public class GetUserOnlineDailyCriteria : CriteriaBase
    {    
        public DateTime Date { get; set; }

        public override string GetSettingKey()
        {
            return "Is_OnlineReportDaily";
        }
    }
}
