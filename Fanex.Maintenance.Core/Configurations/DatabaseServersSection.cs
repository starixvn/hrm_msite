// --------------------------------------------------------------------------
// <copyright file="DatabaseServersSection.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.5">Created</change>
// </history>

namespace Fanex.Maintenance.Core.Configurations
{
    using System.Configuration;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DatabaseServersSection : ConfigurationSection
    {
        [ConfigurationProperty("servers", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ServerCollection),
            AddItemName = "add")]
        public ServerCollection Servers
        {
            get
            {
                var spNameCollection =
                    (ServerCollection)base["servers"];
                return spNameCollection;
            }
        }

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

    public class ServerCollection : ConfigurationElementCollection
    {
        public ServerCollection()
        {
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        public void Add(ServerElement mappingElement)
        {
            BaseAdd(mappingElement);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ServerElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ServerElement)element).ServerIp + ((ServerElement)element).ServerName + ((ServerElement)element).ConnectionString;
        }

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
    }
}