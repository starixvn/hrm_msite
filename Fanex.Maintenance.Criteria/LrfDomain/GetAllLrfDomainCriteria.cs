// --------------------------------------------------------------------------
// <copyright file="GetAllLrfDomainCriteria.cs" company="Nexcel Solutions Vietnam">
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
    /// Get All LRF Domain Criteria.
    /// </summary>
    public class GetAllLrfDomainCriteria : CriteriaBase
    {
        public override string GetSettingKey()
        {
            return "Lrf_DomainSel";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
