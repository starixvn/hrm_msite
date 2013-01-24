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
    using Fanex.Maintenance.Criteria.ConnectionString.DatabaseServer;
    using Fanex.Maintenance.Repository.Impl.ConnectionString;
    using Fanex.Maintenance.Repository.Interface.ConnectionString;
    using Entity.Model.ConnectionString;
    using Fanex.NAP.Framework.Data;
    using Fanex.Logging;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DatabaseServiceService : IDatabaseServerService
    {
        #region Member

        private readonly IDatabaseServerRepository _databaseServerRepository;

        #endregion

        #region Ctor

        public DatabaseServiceService()
            : this(null)
        {
        }

        public DatabaseServiceService(IDatabaseServerRepository databaseServerRepository)
        {
            _databaseServerRepository = databaseServerRepository ?? new DatabaseServerRepository();
        }

        #endregion

        public IEnumerable<DatabaseServer> GetAll()
        {
            try
            {
                var criteria = new DatabaseServerGetAllCriteria();

                return _databaseServerRepository.GetAll(criteria);
            }
            catch (Exception ex)
            {
                Logging.Logger.Log.Error(ex.Message, ex);
            }

            return null;
        }


        public IEnumerable<DatabaseServerSite> GetWithNumOfSite(string name = "")
        {
            try
            {
                var criteria = new DatabaseServerGetAllCriteria
                {
                    Name = name
                };

                return _databaseServerRepository.GetWithNumOfSite(criteria);
            }
            catch (Exception ex)
            {
                Logging.Logger.Log.Error(ex.Message, ex);
            }

            return null;
        }

        public bool InsertOrUpdate(ICriteria criteria)
        {
            try
            {
                int check = _databaseServerRepository.InsertOrUpdate(criteria);

                return check > 0;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }

            return false;
        }


        public bool UpdateStatus(int dbServerId, bool isActive)
        {
            try
            {
                var criteria = new DatabaseServerUpdateStatusCriteria
                {
                    DbServerId = dbServerId,
                    IsActive = isActive                    
                };
                
                int check = _databaseServerRepository.InsertOrUpdate(criteria);

                return check > 0;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }

            return false;
        }


        public DatabaseServer GetById(int id)
        {
            try
            {
                var criteria = new DatabaseServerGetByIdCriteria
                {
                    DbServerId = id
                };

                return _databaseServerRepository.Get(criteria);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }

            return null;
        }
    }
}
