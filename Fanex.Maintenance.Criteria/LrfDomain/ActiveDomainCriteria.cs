// --------------------------------------------------------------------------
// <copyright file="ActiveDomainUpCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Forrest.Nguyen" date="2013.10.23">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.LrfDomain
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Update LRF Domain criteria.
    /// </summary>
    public class ActiveDomainCriteria : CriteriaBase
    {
        public int Id { get; set; }

        public override string GetSettingKey()
        {
            return "Lrf_ActiveDomainUpd";
        }

        public override bool IsValid()
        {
            return true;
        }  
    }
}
