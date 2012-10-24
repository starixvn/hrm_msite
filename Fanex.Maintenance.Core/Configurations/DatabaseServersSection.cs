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
    /// TODO: Update summary.
    /// </summary>
    public class DatabaseServersSection : ConfigurationSection
    {
        /// <summary>
        /// List Server
        /// </summary>
        [ConfigurationProperty("servers", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ServerCollection),
            AddItemName = "add")]
        public ServerCollection Servers
        {
            get
            {
                var serverCollection = (ServerCollection)base["servers"];
                
                return serverCollection;
            }
        }

        /// <summary>
        /// List Server ForGet Resource
        /// </summary>
        [ConfigurationProperty("resourceservers", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ServerCollection),
            AddItemName = "add")]
        public ServerCollection ResourceServers
        {
            get
            {
                var resourceServerCollection = (ServerCollection)base["resourceservers"];

                return resourceServerCollection;
            }
        }

        /// <summary>
        /// Create static instance for reference
        /// </summary>
        public static DatabaseServersSection Instance
        {
            get
            {
                var databaseServers =
                 ConfigurationManager.GetSection("DatabaseServersSection") as DatabaseServersSection;
                
                return databaseServers;
            }
        }
    }

    /// <summary>
    /// Server Collection, map to collection in config file
    /// </summary>
    public class ServerCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Controller
        /// </summary>
        public ServerCollection()
        {
        }

        /// <summary>
        /// Collection Type
        /// </summary>
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        /// <summary>
        /// Add Element
        /// </summary>
        /// <param name="mappingElement"></param>
        public void Add(ServerElement mappingElement)
        {
            BaseAdd(mappingElement);
        }

        /// <summary>
        /// Base Add
        /// </summary>
        /// <param name="element"></param>
        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        /// <summary>
        /// Create New Element
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ServerElement();
        }

        /// <summary>
        /// Get Element Key
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ServerElement)element).ServerIp + ((ServerElement)element).ServerName + ((ServerElement)element).ConnectionString;
        }

        /// <summary>
        /// Get Element
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ServerElement this[int index]
        {
            get
            {
                return (ServerElement)BaseGet(index);
            }

            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }

                BaseAdd(index, value);
            }
        }
    }

    /// <summary>
    /// Server Element
    /// </summary>
    public class ServerElement : ConfigurationElement
    {
        /// <summary>
        /// Connection String attribute
        /// </summary>
        [ConfigurationProperty("ConnectionString", IsRequired = true, IsKey = true)]
        public string ConnectionString
        {
            get
            {
                return (string)this["ConnectionString"];
            }

            set
            {
                this["ConnectionString"] = value;
            }
        }

        /// <summary>
        /// Server Name Attribute
        /// </summary>
        [ConfigurationProperty("ServerName", IsRequired = true)]
        public string ServerName
        {
            get
            {
                return (string)this["ServerName"];
            }

            set
            {
                this["ServerName"] = value;
            }
        }

        /// <summary>
        /// Server Ip Attribute
        /// </summary>
        [ConfigurationProperty("ServerIp", IsRequired = true)]
        public string ServerIp
        {
            get
            {
                return (string)this["ServerIp"];
            }

            set
            {
                this["ServerIp"] = value;
            }
        }

        /// <summary>
        /// Plain or Encrypted
        /// </summary>
        [ConfigurationProperty("Plain", IsRequired = true)]
        public string Plain
        {
            get
            {
                return (string)this["Plain"];
            }

            set
            {
                this["Plain"] = value;
            }
        }

        /// <summary>
        /// Sql Server Agent Status
        /// </summary>
        /// These property below not in config file
        public int SqlServerAgentStatus { get; set; }
    }
}