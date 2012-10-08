// -----------------------------------------------------------------------
// <copyright file="BaseRepository.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Maintenance.Repository.Impl
{
    using System;
    using System.Data;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using Fanex.Maintenance.Core.Helper;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BaseRepository : ReadonlyRepository<dynamic>
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

    }
}