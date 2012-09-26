// -----------------------------------------------------------------------
// <copyright file="BaseRepository.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Maintenance.Core.Repository.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.NAP.Framework.Data;
    using System.Data;
    using Fanex.Data;

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
    }
}
