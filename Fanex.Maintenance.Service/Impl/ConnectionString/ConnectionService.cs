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
    using System.Linq;
    using System.Text;
    using Fanex.Maintenance.Service.Interface.ConnectionString;
    using Fanex.ConnectionString.Domain;
    using Fanex.Maintenance.Repository.Impl.ConnectionString;
    using Fanex.Maintenance.Repository.Interface.ConnectionString;
    using Fanex.Maintenance.Criteria.ConnectionString.Connection;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
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

        public IEnumerable<ConnectionString> GetByAlias(int aliasId)
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
    }
}
