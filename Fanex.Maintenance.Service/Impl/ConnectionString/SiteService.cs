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

        public IEnumerable<Site> GetAll(string name = "", int? dbServerId = null)
        {
            try
            {
                var criteria = new SiteGetAllCriteria
                {
                    Name = name,
                    DbServerId = dbServerId
                };

                if (criteria.IsValid())
                {
                    return _siteRepository.Fetch(criteria);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return null;
        }

        public IEnumerable<Site> GetByAlias(int aliasId)
        {
            Check.Argument.IsNotNegativeOrZero(aliasId, "aliasid");

            try
            {
                var criteria = new SiteGetByAliasCriteria
                {
                    AliasId = aliasId
                };

                if (criteria.IsValid())
                {
                    return _siteRepository.Fetch(criteria);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return null;
        }

        public IEnumerable<SiteAlias> GetSiteWithNumOfAlias(string name = "", int? dbServerId = null)
        {
            try
            {
                var criteria = new SiteGetAllCriteria
                {
                    Name = name,
                    DbServerId = dbServerId
                };

                if (criteria.IsValid())
                {
                    return _siteRepository.GetSiteAlias(criteria);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return null;
        }


        public Site GetById(int id)
        {
            try
            {
                var criteria = new SiteGetByIdCriteria
                {
                    SiteId = id
                };

                if (criteria.IsValid())
                {
                    return _siteRepository.Get(criteria);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return null;
        }


        public ICommandResult InsertOrUpdate(ICriteria criteria)
        {
            // check parameters
            Check.Argument.IsNotNull(criteria, "criteria");

            try
            {
                LogExecSpCriteria logCriteria = GetLogExecSpCriteria();

                if (criteria.IsValid() && logCriteria.IsValid())
                {
                    return _siteRepository.InsertOrUpdate(criteria, logCriteria);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return null;
        }


        public ICommandResult UpdateStatus(ICriteria criteria)
        {
            // check parameters
            Check.Argument.IsNotNull(criteria, "criteria");

            try
            {
                LogExecSpCriteria logCriteria = GetLogExecSpCriteria();

                if (criteria.IsValid() && logCriteria.IsValid())
                {
                    return _siteRepository.InsertOrUpdate(criteria, logCriteria);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return null;
        }


        public IEnumerable<Site> GetByDbServer(int dbServerId)
        {
            try
            {
                var criteria = new SiteGetByDbServerCriteria
                {
                    DbServerId = dbServerId
                };

                if (criteria.IsValid())
                {
                    return _siteRepository.Fetch(criteria);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return null;
        }
    }
}
