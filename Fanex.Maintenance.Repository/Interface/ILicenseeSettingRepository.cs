// --------------------------------------------------------------------------
// <copyright file="ILicenseeSettingRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.20">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Maintenance.Criteria.LrfDomain;
    using Fanex.Maintenance.Entity.Model;

    /// <summary>
    /// Licensee Repository Interface.
    /// </summary>
    public interface ILicenseeSettingRepository
    {
        void ActiveDomain(ActiveDomainCriteria criteria);
    }
}
