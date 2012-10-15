// --------------------------------------------------------------------------
// <copyright file="ServiceControlCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.15">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.Job
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ServiceControlCriteria : CriteriaBase
    {

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "xp_servicecontrol";
        }
    }
}
