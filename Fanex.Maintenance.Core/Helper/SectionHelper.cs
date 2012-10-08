// --------------------------------------------------------------------------
// <copyright file="SectionHelper.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.8">Created</change>
// </history>


namespace Fanex.Maintenance.Core.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Maintenance.Core.Configurations;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SectionHelper
    {
        public static string GetConnectionStringFromServerName(string serverName)
        {
            string connectionString = string.Empty;
            foreach (ServerElement server in DatabaseServersSection.Instance.Servers)
            {
                if (server.ServerName == serverName)
                {
                    connectionString = server.ConnectionString;
                    break;
                }
            }
            return connectionString;
        }


        public static string GetSpNameFromSpKey(string spKey)
        {
            string spName = string.Empty;
            foreach (SpNameElement sp in SpNameSection.Instance.SpNames)
            {
                if (sp.Key == spKey)
                {
                    spName = sp.Name;
                    break;
                }
            }
            return spName;
        }
    }
}
