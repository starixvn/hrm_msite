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
        public static DatabaseServersSection Instance
        {
            get
            {
                var databaseServers = ConfigurationManager.GetSection("DatabaseServersSection") as DatabaseServersSection;
                return databaseServers;
            }
        }
        
        /// <summary>
        /// Gets List Server.
        /// </summary>
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

        /// <summary>
        /// Gets List Server ForGet Resource.
        /// </summary>
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

    /// <summary>
    /// Server Collection, map to collection in config file.
    /// </summary>
    public class ServerCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Controller.
        /// </summary>
        public ServerCollection()
        {
        }

        /// <summary>
        /// Collection Type.
        /// </summary>
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        /// <summary>
        /// Get Element.
        /// </summary>
        /// <param name="index">Index value.</param>
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

        /// <summary>
        /// Add Element.
        /// </summary>
        /// <param name="mappingElement">Mapping element.</param>
        public void Add(ServerElement mappingElement)
        {
            BaseAdd(mappingElement);
        }

        /// <summary>
        /// Base Add.
        /// </summary>
        /// <param name="element"></param>
        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        /// <summary>
        /// Create new element.
        /// </summary>
        /// <returns>Server element.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ServerElement();
        }

        /// <summary>
        /// Get element key.
        /// </summary>
        /// <param name="element"></param>
        /// <returns>Return object.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ServerElement)element).ServerIp + ((ServerElement)element).ServerName 
                + ((ServerElement)element).ConnectionString;
        }
    }

    /// <summary>
    /// Server Element.
    /// </summary>
    public class ServerElement : ConfigurationElement
    {
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

        /// These property below not in config file
        public int SqlServerAgentStatus { get; set; }
    }
}