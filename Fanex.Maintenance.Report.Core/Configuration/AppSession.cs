using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using System.Data;
using DNAide.Data;
using DNAide.Core;

namespace IBCMaintenance.Configuration
{
    public class AppSession
    {
        public static string UserName
        {
            get
            {
                return CastAide.AsString(HttpContext.Current.Session["UserName"], string.Empty); 
            }
            set
            {
                HttpContext.Current.Session["UserName"] = value;
            }
        }
        
        public static int CustID
        {
            get
            {
                return CastAide.AsInt32(HttpContext.Current.Session["CustID"], -1);
            }
            set
            {
                HttpContext.Current.Session["CustID"] = value;
            }
        }

        public static DateTime BalanceUpDay
        {
            get
            {
                GetBalanceUpDate();
                return (DateTime)CacheFactory.GetCacheManager().GetData("BalanceUpDay");
            }
        }

        public static DateTime MoveBetsDay
        {
            get
            {
                GetBalanceUpDate();
                return (DateTime)CacheFactory.GetCacheManager().GetData("MoveBetsDay");
            }
        }

        private static void GetBalanceUpDate()
        {
            if (CacheFactory.GetCacheManager().GetData("BalanceUpDay") == null || CacheFactory.GetCacheManager().GetData("MoveBetsDay") == null)
            {
                DbResult dr = DbHelper.Execute("Age_SelBalanceUpDay", DatabaseType.DB052_REPLICATE);
                DataRow r = dr.ResultSet.Tables[0].Rows[0];                

                CacheFactory.GetCacheManager().Add("BalanceUpDay", CastAide.AsDateTime(r["BalanceUpDay"]), CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromMinutes(30)));
                CacheFactory.GetCacheManager().Add("MoveBetsDay", CastAide.AsDateTime(r["MoveBetsDay"]), CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromMinutes(30)));
            }            
        }             
    }   
}
