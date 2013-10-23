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
    using Fanex.Maintenance.Entity.Model;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Base repository.
    /// </summary>
    /// <typeparam name="T">The element type of the class.</typeparam>
    public class BaseRepository<T> : ReadonlyRepository<T>
    {
        /// <summary>
        /// Get Data table from Criteria.
        /// </summary>
        /// <param name="criteria">A Criteria.</param>
        /// <returns>A table contains information.</returns>
        public virtual DataTable GetDataTable(ICriteria criteria)
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

        /// <summary>
        /// Get Data table with server Name.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="serverName">Server name.</param>
        /// <returns>Data table.</returns>
        public virtual DataTable GetDataTable(ICriteria criteria, string serverName)
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

        /// <summary>
        /// Fetch Data from Criteria.
        /// </summary>
        /// <typeparam name="S">The class type.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <param name="serverIp">Server Name will be executed sp.</param>
        /// <param name="mapObjectFunction">Read data reader and pass object.</param>
        /// <param name="serverType">Server type.</param>
        /// <returns>Collection of object.</returns>
        public virtual IEnumerable<S> Fetch<S>(
            ICriteria criteria, 
            string serverIp, 
            Func<IDataReader, S> mapObjectFunction, 
            int? serverType = null) where S : class
        {
            IList<S> listObject = new List<S>();
            //// new dbsetting from server name and setting key
            DbSetting setting = new DbSetting
            {
                //// get connection string from servername
                ConnectionString = SectionHelper.GetConnectionStringFromServerIp(serverIp, serverType != null ? (int)serverType : 0),
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
        /// Fetch item.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="mapObjectFunction">Map object function.</param>
        /// <returns>List of object.</returns>
        public virtual IEnumerable<S> Fetch<S>(
            ICriteria criteria, 
            Func<IDataReader, S> mapObjectFunction) where S : class
        {
            IList<S> listObject = new List<S>();

            using (var db = new ObjectDb(criteria.GetSettingKey()))
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
        /// Execute command to Db.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="serverIp">Server IP.</param>
        /// <returns>Command result.</returns>
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

        /// <summary>
        /// Get command result.
        /// </summary>
        /// <param name="hash">Hash command result.</param>
        /// <returns>Command result.</returns>
        public CommandResult GetCommandResult(Hashtable hash)
        {
            var errCode = hash["errcode"];
            var errMsg = hash["errmsg"];

            return new CommandResult
            {
                Code = (errCode is DBNull) ? 0 : Convert.ToInt32(errCode),
                Message = (errMsg is DBNull) ? string.Empty : Convert.ToString(errMsg)
            };
        }
    }
}