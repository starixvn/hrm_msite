using System.Data;
using System.Collections;
using IBCMaintenance.Configuration;
using DNAide.Data;

namespace Fanex.Maintenance.Report.Core.Report
{
    public class CommonDao
    {
        #region BalanceUpDay

        public DataTable Lrf_SelBalanceUpDay()
        {
            StoreName spName = new StoreName();
            DataTable dt = DbHelper.Execute(spName.Lrf_SelBalanceUpDay, new Hashtable(), 
                                            DatabaseType.LICENSEE_ARCHIVE).ResultSet.Tables[0];
            return dt;
        }

        #endregion
    }
}
