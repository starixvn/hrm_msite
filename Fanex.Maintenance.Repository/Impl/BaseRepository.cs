// --------------------------------------------------------------------------
// <copyright file="BaseRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.8">Created</change>
//   <change who="Ricky.Nguyen" date="2012.10.9">Add Fetchm, Get Datatable Function</change>
// </history>

namespace Fanex.Maintenance.Repository.Impl
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using Fanex.Data;
    using Fanex.Maintenance.Core.Helper;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BaseRepository<T> : ReadonlyRepository<T>
    {
        /// <summary>
        /// Get Data table from Criteria
        /// </summary>
        /// <param name="criteria">A Criteria</param>
        /// <returns>A table contains information</returns>
        public virtual DataTable GetDataTable(ICriteria criteria)
        {
            try
            {
                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    DataSet ds = db.ExecuteDataSet(criteria.GetSpParams());
                    if (!ReferenceEquals(ds, null) && ds.Tables.Count > 0)
                    {
                        return ds.Tables[0];
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Data table with server Name
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public virtual DataTable GetDataTable(ICriteria criteria, string serverName)
        {
            try
            {
                DbSetting setting = new DbSetting
                {
                    ConnectionString = SectionHelper.GetConnectionStringFromServerName(serverName),
                    SpName = SectionHelper.GetSpNameFromSpKey(criteria.GetSettingKey())
                };

                using (var db = new ObjectDb(setting))
                {
                    DataSet ds = db.ExecuteDataSet(criteria.GetSpParams());
                    if (!ReferenceEquals(ds, null) && ds.Tables.Count > 0)
                    {
                        return ds.Tables[0];
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Fetch Data from Criteria
        /// </summary>
        /// <typeparam name="S">Class</typeparam>
        /// <param name="criteria">Criteria</param>
        /// <param name="serverName">Server Name will be executed sp</param>
        /// <param name="mapObjectFunction">Read data reader and pass object</param>
        /// <returns>Collection of object</returns>
        public virtual IEnumerable<S> Fetch<S>(ICriteria criteria, string serverIp, Func<IDataReader, S> mapObjectFunction) where S : class
        {
            IList<S> listObject = new List<S>();
            //// new dbsetting from server name and setting key
            DbSetting setting = new DbSetting
            {
                //// get connection string from servername
                ConnectionString = SectionHelper.GetConnectionStringFromServerIp(serverIp),
                SpName = SectionHelper.GetSpNameFromSpKey(criteria.GetSettingKey())
            };

            using (var db = new ObjectDb(setting))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        listObject.Add(mapObjectFunction(reader));
                    }
                }
            }

            return listObject;
        }

        /// <summary>
        /// Execute command to Db
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="serverIp"></param>
        /// <returns></returns>
        public virtual Hashtable ExecuteCommand(ICriteria criteria, string serverIp)
        {
            //// new dbsetting from server name and setting key
            DbSetting setting = new DbSetting
            {
                //// get connection string from servername
                ConnectionString = SectionHelper.GetConnectionStringFromServerIp(serverIp),
                SpName = SectionHelper.GetSpNameFromSpKey(criteria.GetSettingKey())
            };

            using (var db = new ObjectDb(setting))
            {
                db.ExecuteNonQuery(criteria.GetSpParams());
                return db.GetOuputParams();
            }
        }
    }
}