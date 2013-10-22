// --------------------------------------------------------------------------
// <copyright file="ConnectionService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.22">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Impl.ConnectionString
{
    using System;
    using System.Collections.Generic;
    using Fanex.Common.Helper;
    using Fanex.ConnectionString.Domain;
    using Fanex.Maintenance.Criteria.ConnectionString.Connection;
    using Fanex.Maintenance.Criteria.Misc;
    using Fanex.Maintenance.Entity.Model.ConnectionString;
    using Fanex.Maintenance.Repository.Impl.ConnectionString;
    using Fanex.Maintenance.Repository.Interface.ConnectionString;
    using Fanex.Maintenance.Service.Interface.ConnectionString;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Connection service.
    /// </summary>
    public class ConnectionService : BaseService, IConnectionService
    {
        #region Member

        private readonly IConnectionRepository _connectionRepository;

        #endregion Member

        #region Ctor

        public ConnectionService()
            : this(null)
        {
        }

        public ConnectionService(IConnectionRepository connectionRepository)
        {
            _connectionRepository = connectionRepository ?? new ConnectionRepository();
        }

        #endregion Ctor

        /// <summary>
        /// Get connection by alias.
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns>List of connection of connection string.</returns>
        public IEnumerable<ConnectionStringConnection> GetByAlias(int aliasId)
        {
            Check.Argument.IsNotNegativeOrZero(aliasId, "aliasid");

            try
            {
                var criteria = new ConnectionGetByAliasCriteria
                {
                    AliasId = aliasId
                };

                if (criteria.IsValid())
                {
                    return _connectionRepository.GetByAlias(criteria);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return null;
        }

        /// <summary>
        /// Get connection by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Connection string.</returns>
        public ConnectionString GetById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                var criteria = new ConnectionGetByIdCriteria
                {
                    ConnectionId = id
                };

                if (criteria.IsValid())
                {
                    return _connectionRepository.GetById(criteria);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return null;
        }

        /// <summary>
        /// Get secondary connection.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of connection string.</returns>
        public IEnumerable<ConnectionString> GetSecondaryConnection(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                var criteria = new ConnectionGetSecondaryConnectionCriteria
                {
                    ConnectionId = id
                };

                if (criteria.IsValid())
                {
                    return _connectionRepository.GetSecondaryConnection(criteria);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return null;
        }

        /// <summary>
        /// Update default connection.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Command result.</returns>
        public ICommandResult UpdateDefaultConnection(ICriteria criteria)
        {
            // check parameters
            Check.Argument.IsNotNull(criteria, "criteria");

            try
            {
                LogExecSpCriteria logCriteria = GetLogExecSpCriteria();

                if (criteria.IsValid() && logCriteria.IsValid())
                {
                    return _connectionRepository.InsertOrUpdate(criteria, logCriteria);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return null;
        }

        /// <summary>
        /// Insert or update connection.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Command result.</returns>
        public ICommandResult InsertOrUpdate(ICriteria criteria)
        {
            // check parameters
            Check.Argument.IsNotNull(criteria, "criteria");

            try
            {
                LogExecSpCriteria logCriteria = GetLogExecSpCriteria();

                if (criteria.IsValid() && logCriteria.IsValid())
                {
                    return _connectionRepository.InsertOrUpdate(criteria, logCriteria);
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