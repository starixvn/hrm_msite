// --------------------------------------------------------------------------
// <copyright file="DatabaseServiceService.cs" company="Nexcel Solutions Vietnam">
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
    using System.Linq;
    using System.Text;
    using Fanex.Maintenance.Service.Interface.ConnectionString;
    using Fanex.ConnectionString.Domain;
    using Fanex.Maintenance.Criteria.ConnectionString.Site;
    using Fanex.Maintenance.Repository.Impl.ConnectionString;
    using Fanex.Maintenance.Repository.Interface.ConnectionString;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SiteService : ISiteService
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

        public IEnumerable<Site> GetAll()
        {
            try
            {
                var criteria = new SiteGetAllCriteria();

                return _siteRepository.Fetch(criteria);
            }
            catch (Exception ex)
            {
                Logging.Logger.Log.Error(ex.Message, ex);
            }

            return null;
        }

        public IEnumerable<Site> GetByAlias(int aliasId)
        {
            try
            {
                var criteria = new SiteGetByAliasCriteria
                {
                    AliasId = aliasId
                };

                return _siteRepository.Fetch(criteria);
            }
            catch (Exception ex)
            {
                Logging.Logger.Log.Error(ex.Message, ex);
            }

            return null;
        }
    }
}
