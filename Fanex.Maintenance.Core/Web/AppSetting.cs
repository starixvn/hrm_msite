// --------------------------------------------------------------------------
// <copyright file="AppSetting.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.16">StyleCop</change>
// </history>

namespace Fanex.Maintenance.Core.Web
{
    using System;
    using System.Configuration;
    using System.Globalization;
    using System.Web;
    using Fanex.Logging;
    using Fanex.Common;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AppSetting
    {
        /// <summary>
        /// Is Virtual
        /// </summary>
        private static string _isVirtualDir = "false";
        
        /// <summary>
        /// Minute
        /// </summary>
        private static int _minute = 10;

        private static int _getDataNotifyInterval = 60000;
        private static int _getServerResourcesInfoInterval = 2000;


        /// <summary>
        /// Contructor
        /// </summary>
        static AppSetting()
        {
            try
            {
                _isVirtualDir = ConfigurationManager.AppSettings["isVirtualDirectory"];
                _minute = Converter.ToInt(ConfigurationManager.AppSettings["Minute"]);
                _getDataNotifyInterval = Converter.ToInt(ConfigurationManager.AppSettings["GetDataNotifyInterval"], 60000);
                _getServerResourcesInfoInterval = Converter.ToInt(ConfigurationManager.AppSettings["GetServerResourcesInfoInterval"], 2000);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.ToString(), ex);
            }
        }

        /// <summary>
        /// Is Virtual Directory
        /// </summary>
        public static bool IsVirtualDirectory
        {
            get
            {
                return (string.Compare(_isVirtualDir, "true", true) == 0);
            }
        }

        /// <summary>
        /// Minute
        /// </summary>
        public static int Minute
        {
            get
            {
                return _minute;
            }
        }

        public static int GetDataNotifyInterval
        {
            get
            {
                return _getDataNotifyInterval;
            }
        }

        public static int GetServerResourcesInfoInterval
        {
            get
            {
                return _getServerResourcesInfoInterval;
            }
        }
            

        /// <summary>
        /// Culture Info
        /// </summary>
        public static CultureInfo Culture
        {
            get
            {
                return CultureInfo.CurrentCulture;
            }
        }

        /// <summary>
        /// Context
        /// </summary>
        public static HttpContext Context
        {
            get
            {
                return HttpContext.Current;
            }
        }
    }

    /// <summary>
    /// Site Name
    /// </summary>
    public static class SiteName
    {
        /// <summary>
        /// Agent Name
        /// </summary>
        public static string _Agent = "Agent";

        /// <summary>
        /// Accounting Name
        /// </summary>
        public static string _Accounting = "Accounting";

        /// <summary>
        /// Affiliate Name
        /// </summary>
        public static string _Affiliate = "Affiliate";

        /// <summary>
        /// Array Site
        /// </summary>
        public static string[] Arr = { _Agent, _Accounting, _Affiliate };
    }

    /// <summary>
    /// Result
    /// </summary>
    public class RESULT
    {
        /// <summary>
        /// Is Success
        /// </summary>
        public bool isSuccess = true;

        /// <summary>
        /// Is DataOk
        /// </summary>
        public bool isDataOK = false;

        /// <summary>
        /// Error Message
        /// </summary>
        public string errMsg = string.Empty;
    }
}