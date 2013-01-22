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
    using Fanex.Logging;
    using Fanex.Maintenance.Criteria.ConnectionString.Alias;
    using Fanex.Maintenance.Repository.Impl.ConnectionString;
    using Fanex.Maintenance.Repository.Interface.ConnectionString;
    using Fanex.Maintenance.Service.Interface.ConnectionString;
    using Fanex.NAP.Framework.Data;
    using Fanex.ConnectionString.Domain;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AliasService : IAliasService
    {
        #region Member

        private readonly IAliasRepository _aliasRepository;

        #endregion Member

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

                return _aliasRepository.GetAll(criteria);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }

            return null;
        }

        public bool InsertOrUpdate(ICriteria criteria)
        {
            try
            {
                int check = _aliasRepository.InsertOrUpdate(criteria);

                return check > 0;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }

            return false;
        }

        public Alias Get(int aliasId)
        {
            try
            {
                var criteria = new AliasGetByIdCriteria
                {
                    AliasId = aliasId
                };

                return _aliasRepository.GetById(criteria);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }

            return null;
        }


        public bool UpdateStatus(int aliasId, bool isActive)
        {
            try
            {
                var criteria = new AliasUpdateStatusCriteria
                {
                    AliasId = aliasId,
                    IsActive = isActive
                };

                int check = _aliasRepository.InsertOrUpdate(criteria);

                return check > 0;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }

            return false;
        }
    }
}