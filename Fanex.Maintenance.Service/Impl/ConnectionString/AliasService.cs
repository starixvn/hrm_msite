// --------------------------------------------------------------------------
// <copyright file="AliasService.cs" company="Nexcel Solutions Vietnam">
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
    using Fanex.Maintenance.Criteria.ConnectionString.Alias;
    using Fanex.Maintenance.Criteria.ConnectionString.DatabaseServer;
    using Fanex.Maintenance.Criteria.Misc;
    using Fanex.Maintenance.Repository.Impl.ConnectionString;
    using Fanex.Maintenance.Repository.Interface.ConnectionString;
    using Fanex.Maintenance.Service.Interface.ConnectionString;
    using Fanex.NAP.Framework.Data;

    public class AliasService : BaseService, IAliasService
    {
        private readonly IAliasRepository _aliasRepository;

        #region Ctor

        public AliasService()
            : this(null)
        {
        }

        public AliasService(IAliasRepository aliasrepository)
        {
            _aliasRepository = aliasrepository ?? new AliasRepository();
        }

        #endregion Ctor

        public IEnumerable<AliasConnectionString> GetAll(string alias = "", int? dbServerId = null, int? siteId = null)
        {
            try
            {
                var criteria = new AliasGetAllCriteria
                {
                    Alias = alias,
                    DbServerId = dbServerId,
                    SiteId = siteId
                };

                if (criteria.IsValid())
                {
                    return _aliasRepository.GetAll(criteria);
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
                    return _aliasRepository.InsertOrUpdate(criteria, logCriteria);
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
                    return _aliasRepository.InsertOrUpdate(criteria, logCriteria);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return null;
        }

        public Alias Get(int aliasId)
        {
            // check parameters
            Check.Argument.IsNotNegativeOrZero(aliasId, "aliasId");

            try
            {
                var criteria = new AliasGetByIdCriteria
                {
                    AliasId = aliasId
                };

                if (criteria.IsValid())
                {
                    return _aliasRepository.GetById(criteria);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return null;
        }

        public IEnumerable<Alias> GetBySite(int siteId)
        {
            // check parameters
            Check.Argument.IsNotNegativeOrZero(siteId, "siteId");

            try
            {
                var criteria = new AliasGetBySiteCriteria
                {
                    SiteId = siteId
                };

                if (criteria.IsValid())
                {
                    return _aliasRepository.GetBySite(criteria);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return null;
        }

        public IEnumerable<AliasConnectionStringConnection> GetSwitchConnection(int fromDbServerId, int toDbServerId)
        {
            // check parameters
            Check.Argument.IsNotNegativeOrZero(fromDbServerId, "fromDbServerId");
            Check.Argument.IsNotNegativeOrZero(toDbServerId, "toDbServerId");

            try
            {
                var criteria = new DatabaseServerGetConnectionFromDbServerCriteria
                {
                    FromDbServerId = fromDbServerId,
                    ToDbServerId = toDbServerId
                };

                if (criteria.IsValid())
                {
                    return _aliasRepository.GetSwitchConnection(criteria);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return null;
        }

        public IEnumerable<AliasConnectionStringConnection> GetSwitchSecondConnection(int dbServerId)
        {
            // check parameters
            Check.Argument.IsNotNegativeOrZero(dbServerId, "dbServerId");

            try
            {
                var criteria = new DatabaseServerGetSecondConnectionFromDbServerCriteria
                {
                    DbServerId = dbServerId
                };

                if (criteria.IsValid())
                {
                    return _aliasRepository.GetSwitchConnection(criteria);
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