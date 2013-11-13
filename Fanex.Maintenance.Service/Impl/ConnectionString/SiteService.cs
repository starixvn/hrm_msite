// --------------------------------------------------------------------------
// <copyright file="SiteService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.21">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Impl.ConnectionString
{
    using System;
    using System.Collections.Generic;
    using Entity.Model.ConnectionString;
    using Fanex.Common.Helper;
    using Fanex.ConnectionString.Domain;
    using Fanex.Maintenance.Criteria.ConnectionString.Site;
    using Fanex.Maintenance.Criteria.Misc;
    using Fanex.Maintenance.Repository.Impl.ConnectionString;
    using Fanex.Maintenance.Repository.Interface.ConnectionString;
    using Fanex.Maintenance.Service.Interface.ConnectionString;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SiteService : BaseService, ISiteService
    {
        #region Member

        private readonly ISiteRepository _siteRepository;

        #endregion

        #region Ctor

        public SiteService()
            : this(null)
        {
        }

        public SiteService(ISiteRepository siteRepository)
        {
            _siteRepository = siteRepository ?? new SiteRepository();
        }

        #endregion

        /// <summary>
        /// Get all sites.
        /// </summary>
        /// <param name="name">Site object name.</param>
        /// <param name="databaseServerId">Database server ID.</param>
        /// <returns>List of site.</returns>
        public IEnumerable<Site> GetAll(string name = "", int? databaseServerId = null)
        {
            var criteria = new SiteGetAllCriteria
            {
                Name = name,
                DbServerId = databaseServerId
            };

            if (criteria.IsValid())
            {
                return _siteRepository.Fetch(criteria);
            }

            return null;
        }

        /// <summary>
        /// Get site by alias.
        /// </summary>
        /// <param name="aliasId">Connection string alias ID.</param>]
        /// <returns>List of site.</returns>
        public IEnumerable<Site> GetByAlias(int aliasId)
        {
            Check.Argument.IsNotNegativeOrZero(aliasId, "aliasid");

            var criteria = new SiteGetByAliasCriteria
            {
                AliasId = aliasId
            };

            if (criteria.IsValid())
            {
                return _siteRepository.Fetch(criteria);
            }

            return null;
        }

        /// <summary>
        /// Get site with number of alias.
        /// </summary>
        /// <param name="name">Site object name.</param>
        /// <param name="databaseServerId">Database server ID.</param>
        /// <returns>List of site alias.</returns>
        public IEnumerable<SiteAlias> GetSiteWithNumOfAlias(string name = "", int? databaseServerId = null)
        {
            var criteria = new SiteGetAllCriteria
            {
                Name = name,
                DbServerId = databaseServerId
            };

            if (criteria.IsValid())
            {
                return _siteRepository.GetSiteAlias(criteria);
            }

            return null;
        }

        /// <summary>
        /// Get site by ID.
        /// </summary>
        /// <param name="id">Site object ID.</param>
        /// <returns>Site object.</returns>
        public Site GetById(int id)
        {
            var criteria = new SiteGetByIdCriteria
            {
                SiteId = id
            };

            if (criteria.IsValid())
            {
                return _siteRepository.Get(criteria);
            }

            return null;
        }

        /// <summary>
        /// Insert or update site.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Command result.</returns>
        public ICommandResult InsertOrUpdate(ICriteria criteria)
        {
            // check parameters
            Check.Argument.IsNotNull(criteria, "criteria");

            LogExecSpCriteria logCriteria = GetLogExecSpCriteria();

            if (criteria.IsValid() && logCriteria.IsValid())
            {
                return _siteRepository.InsertOrUpdate(criteria, logCriteria);
            }

            return null;
        }

        /// <summary>
        /// Update site status.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Command result.</returns>
        public ICommandResult UpdateStatus(ICriteria criteria)
        {
            // check parameters
            Check.Argument.IsNotNull(criteria, "criteria");

            LogExecSpCriteria logCriteria = GetLogExecSpCriteria();

            if (criteria.IsValid() && logCriteria.IsValid())
            {
                return _siteRepository.InsertOrUpdate(criteria, logCriteria);
            }

            return null;
        }

        /// <summary>
        /// Get site by database server.
        /// </summary>
        /// <param name="databaseServerId">Database server ID.</param>
        /// <returns>List of site.</returns>
        public IEnumerable<Site> GetByDbServer(int databaseServerId)
        {
            var criteria = new SiteGetByDbServerCriteria
            {
                DbServerId = databaseServerId
            };

            if (criteria.IsValid())
            {
                return _siteRepository.Fetch(criteria);
            }

            return null;
        }
    }
}
