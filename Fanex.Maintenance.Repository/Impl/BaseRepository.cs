// --------------------------------------------------------------------------
// <copyright file="BaseRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.8">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Impl
{
    using System;
    using System.Data;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using Fanex.Maintenance.Core.Helper;
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BaseRepository<T> : ReadonlyRepository<T>
    {
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

        public virtual IEnumerable<T> Fetch(ICriteria criteria, string serverName, Func<IDataReader, T> mapObjectFunction)
        {
            IList<T> listT = new List<T>();
            //// new dbsetting from server name and setting key
            DbSetting setting = new DbSetting
            {
                //// get connection string from servername
                ConnectionString = SectionHelper.GetConnectionStringFromServerName(serverName),
                SpName = SectionHelper.GetSpNameFromSpKey(criteria.GetSettingKey())
            };

            using (var db = new ObjectDb(setting))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        listT.Add(mapObjectFunction(reader));
                    }
                }
            }
            return listT;
        }
    }
}