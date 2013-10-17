// --------------------------------------------------------------------------
// <copyright file="DatabaseServerService.cs" company="Nexcel Solutions Vietnam">
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
    using Fanex.Maintenance.Criteria.ConnectionString.DatabaseServer;
    using Fanex.Maintenance.Criteria.Misc;
    using Fanex.Maintenance.Repository.Impl.ConnectionString;
    using Fanex.Maintenance.Repository.Interface.ConnectionString;
    using Fanex.Maintenance.Service.Interface.ConnectionString;
    using Fanex.NAP.Framework.Data;

    public class DatabaseServerService : BaseService, IDatabaseServerService
    {
        #region Member

        private readonly IDatabaseServerRepository _databaseServerRepository;

        #endregion

        #region Ctor

        public DatabaseServerService()
            : this(null)
        {
        }

        public DatabaseServerService(IDatabaseServerRepository databaseServerRepository)
        {
            _databaseServerRepository = databaseServerRepository ?? new DatabaseServerRepository();
        }

        #endregion

        public IEnumerable<DatabaseServer> GetAll()
        {
            try
            {
                var criteria = new DatabaseServerGetAllCriteria();

                if (criteria.IsValid())
                {
                    return _databaseServerRepository.GetAll(criteria);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
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

                if (criteria.IsValid())
                {
                    return _databaseServerRepository.GetWithNumOfSite(criteria);
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
                    return _databaseServerRepository.InsertOrUpdate(criteria, logCriteria);
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
                    return _databaseServerRepository.InsertOrUpdate(criteria, logCriteria);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return null;
        }

        public DatabaseServer GetById(int id)
        {
            try
            {
                var criteria = new DatabaseServerGetByIdCriteria
                {
                    DbServerId = id
                };

                if (criteria.IsValid())
                {
                    return _databaseServerRepository.Get(criteria);
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
