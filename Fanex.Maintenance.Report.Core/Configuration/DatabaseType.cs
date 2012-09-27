using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBCMaintenance.Configuration
{
    public class DatabaseType
    {
        private static string _main = "main";
        private static string _onUser = "on_user";
        private static string _maintenance = "maintenance";
        private static string _dblog = "dblog";        
        private static string _db03 = "db03";
        private static string _db052 = "db052";
        private static string _db06 = "db06";
        private static string _licensee = "licensee";
        private static string _licenseeArchiveDb = "licensee_archive";

        public static string MAIN
        {
            get
            {
                return _main;
            }
        }        
       
        public static string ON_USER
        {
            get
            {
                return _onUser;
            }
        }

        public static string MAINTENANCE
        {
            get
            {
                return _maintenance;
            }
        }

        public static string DBLog
        {
            get
            {
                return _dblog;
            }
        }

        public static string DB03_REPLICATE
        {
            get
            {
                return _db03;
            }
        }

        public static string DB052_REPLICATE
        {
            get
            {
                return _db052;
            }
        }

        public static string DB06_REPLICATE
        {
            get
            {
                return _db06;
            }
        }

        public static string LICENSEE
        {
            get
            {
                return _licensee;
            }
        }

        public static string LICENSEE_ARCHIVE
        {
            get { return _licenseeArchiveDb; }
        }
    }
}
