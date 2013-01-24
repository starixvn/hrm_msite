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

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class ConnectionService : IConnectionService
    {
        #region Member

        private readonly IConnectionRepository _connectionRepository;

        #endregion

        #region Ctor

        public ConnectionService()
            : this(null)
        {
        }

        public ConnectionService(IConnectionRepository connectionRepository)
        {
            _connectionRepository = connectionRepository ?? new ConnectionRepository();
        }

        #endregion

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


        public bool UpdateDefaultConnection(int aliasId, int conectionId)
        {
            try
            {
                var criteria = new ConnectionUpdateDefaultCriteria
                {
                    AliasId = aliasId,
                    ConnectionId = conectionId
                };

                return _connectionRepository.InsertOrUpdate(criteria) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool InsertOrUpdate(ICriteria criteria)
        {
            try
            {
                return _connectionRepository.InsertOrUpdate(criteria) > 0;
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
    }
}
