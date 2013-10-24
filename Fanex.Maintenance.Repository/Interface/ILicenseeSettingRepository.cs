// --------------------------------------------------------------------------
// <copyright file="ILicenseeSettingRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Forrest.Nguyen" date="2013.10.23">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Interface
{
    using Fanex.Data;
    using Fanex.Maintenance.Criteria.LrfDomain;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Repository.Interface;

    /// <summary>
    /// Licensee Setting Repository Interface.
    /// </summary>
    public interface ILicenseeSettingRepository
    {
        /// <summary>
        /// Active Domain Up.
        /// </summary>
        /// <param name="criteria">Domain criteria.</param>
        void ActiveDomain(ActiveDomainCriteria criteria);
    }
}
