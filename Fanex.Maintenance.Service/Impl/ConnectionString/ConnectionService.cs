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
    using System.Diagnostics.CodeAnalysis;
    using Fanex.ConnectionString.Domain;
    using Fanex.Maintenance.Criteria.ConnectionString.Connection;
    using Fanex.Maintenance.Entity.Model.ConnectionString;
    using Fanex.Maintenance.Repository.Impl.ConnectionString;
    using Fanex.Maintenance.Repository.Interface.ConnectionString;
    using Fanex.Maintenance.Service.Interface.ConnectionString;
    using Fanex.NAP.Framework.Data;
    using Fanex.Common.Helper;
    using Fanex.Maintenance.Criteria.Misc;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
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

        public IEnumerable<ConnectionStringConnection> GetByAlias(int aliasId)
        {
            try
            {
                var criteria = new ConnectionGetByAliasCriteria
                {
                    AliasId = aliasId
                };

                return _connectionRepository.GetByAlais(criteria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ConnectionString GetById(int id)
        {
            try
            {
                var criteria = new ConnectionGetByIdCriteria
                {
                    ConectionId = id
                };

                return _connectionRepository.GetById(criteria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ConnectionString> GetSecondaryConection(int id)
        {
            try
            {
                var criteria = new ConnectionGetSecondaryConectionCriteria
                {
                    ConectionId = id
                };

                return _connectionRepository.GetSecondaryConection(criteria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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