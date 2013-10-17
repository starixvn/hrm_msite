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
    using Fanex.Maintenance.Core.Configurations;
    using Fanex.Maintenance.Core.Web;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SectionHelper
    {
        /// <summary>
        /// Get Connection String From Server Name
        /// </summary>
        /// <param name="serverName">Server Name</param>
        /// <returns>Connection String maps with server name</returns>
        public static string GetConnectionStringFromServerName(string serverName)
        {
            string connectionString = string.Empty;
            foreach (ServerElement server in DatabaseServersSection.Instance.Servers)
            {
                if (server.ServerName == serverName)
                {
                    if (server.Plain == "1")
                    {
                        connectionString = server.ConnectionString;
                    }
                    else
                    {
                        connectionString = Fanex.Common.Cryptography.DeCryptConnectionString(server.ConnectionString);
                    }

                    break;
                }
            }

            return connectionString;
        }

        /// <summary>
        /// Get Connection String From Server Name
        /// </summary>
        /// <param name="serverName">Server Name</param>
        /// <param name="getType">Get Server Type</param>
        /// <returns>Connection String maps with server name</returns>
        public static string GetConnectionStringFromServerIp(string serverIp = "", int getType = 0)
        {
            string connectionString = string.Empty;
            switch (getType)
            {
                case (int)ServerType.Normal:
                    foreach (ServerElement server in DatabaseServersSection.Instance.Servers)
                    {
                        if (server.ServerIp == serverIp)
                        {
                            if (server.Plain == "1")
                            {
                                connectionString = server.ConnectionString;
                            }
                            else
                            {
                                connectionString = Fanex.Common.Cryptography.DeCryptConnectionString(server.ConnectionString);
                            }

                            break;
                        }
                    }
                    break;

                case (int)ServerType.Resource:
                    foreach (ServerElement server in DatabaseServersSection.Instance.ResourceServers)
                    {
                        if (server.ServerIp == serverIp)
                        {
                            if (server.Plain == "1")
                            {
                                connectionString = server.ConnectionString;
                            }
                            else
                            {
                                connectionString = Fanex.Common.Cryptography.DeCryptConnectionString(server.ConnectionString);
                            }

                            break;
                        }
                    }
                    break;
            }

            return connectionString;
        }

        /// <summary>
        /// Get Server Ip From server Name
        /// </summary>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public static string GetServerIpFromServerName(string serverName)
        {
            foreach (ServerElement server in DatabaseServersSection.Instance.Servers)
            {
                if (server.ServerName == serverName)
                {
                    return server.ServerIp;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Get Sp Name From SpKey
        /// </summary>
        /// <param name="storeProcedureKey">Sp Key</param>
        /// <returns>Sp Name Map with sp Key</returns>
        public static string GetSpNameFromSpKey(string storeProcedureKey)
        {
            string storeProcedureName = string.Empty;

            foreach (SpNameElement sp in SpNameSection.Instance.SpNames)
            {
                if (sp.Key == storeProcedureKey)
                {
                    storeProcedureName = sp.Name;
                    break;
                }
            }

            return storeProcedureName;
        }
    }
}