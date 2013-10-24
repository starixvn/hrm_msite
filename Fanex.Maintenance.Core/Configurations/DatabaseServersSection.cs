// --------------------------------------------------------------------------
// <copyright file="DatabaseServersSection.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.5">Created</change>
//   <change who="Ricky.Nguyen" date="2012.10.16">Stylecop</change>
// </history>

namespace Fanex.Maintenance.Core.Configurations
{
    using System.Configuration;

    /// <summary>
    /// Database servers section.
    /// </summary>
    public class DatabaseServersSection : ConfigurationSection
    {
        /// <summary>
        /// Gets create static instance for reference.
        /// </summary>
        /// <value>Database server section.</value>
        public static DatabaseServersSection Instance
        {
            get
            {
                var databaseServers = ConfigurationManager.GetSection("DatabaseServersSection") as DatabaseServersSection;
                return databaseServers;
            }
        }

        [ConfigurationProperty("servers", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ServerCollection), AddItemName = "add")]
        public ServerCollection Servers
        {
            get
            {
                var serverCollection = (ServerCollection)base["servers"];
                return serverCollection;
            }
        }

        [ConfigurationProperty("resourceservers", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ServerCollection), AddItemName = "add")]
        public ServerCollection ResourceServers
        {
            get
            {
                var resourceServerCollection = (ServerCollection)base["resourceservers"];

                return resourceServerCollection;
            }
        }
    }
}