using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Configuration;
using System.Globalization;

namespace IBCMaintenance.Configuration
{
    public class AppSetting
    {
        private static string _isVirtualDir = "false";
        private static int _Minute = 10;
        static AppSetting()
        {
            _isVirtualDir = ConfigurationManager.AppSettings["isVirtualDirectory"];
            _Minute = Convert.ToInt32(ConfigurationManager.AppSettings["Minute"]);
        }

        public static bool IsVirtualDirectory
        {
            get
            {
                return (string.Compare(_isVirtualDir, "true", true) == 0);
            }
        }
        public static int Minute
        {
            get
            {
                return _Minute;
            }
        }
        public static CultureInfo Culture
        {
            get
            {
                return CultureInfo.CurrentCulture;
            }
        }

        public static HttpContext Context
        {
            get
            {
                return HttpContext.Current;
            }
        }        
    }
    public static class SiteName
    {
        public static string _Agent = "Agent";
        public static string _Accounting = "Accounting";
        public static string _Affiliate = "Affiliate";        
        public static string[] Arr = { _Agent, _Accounting, _Affiliate };
    }
    public class RESULT
    {
        public bool isSuccess = true;
        public bool isDataOK = false;
        public string errMsg = string.Empty;        
    }
}
