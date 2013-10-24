// --------------------------------------------------------------------------
// <copyright file="ServerCollection.cs" company="Nexcel Solutions Vietnam">
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
    /// Server Collection, map to collection in config file.
    /// </summary>
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

        /// <summary>
        /// Get Element.
        /// </summary>
        /// <param name="index">Index value.</param>
        /// <returns>Server element.</returns>
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
        /// Add mapping element.
        /// </summary>
        /// <param name="mappingElement">Mapping element.</param>
        public void Add(ServerElement mappingElement)
        {
            BaseAdd(mappingElement);
        }

        /// <summary>
        /// Base add configuration element.
        /// </summary>
        /// <param name="element">Configuration element.</param>
        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        /// <summary>
        /// Create new element.
        /// </summary>
        /// <returns>Configuration element.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ServerElement();
        }

        /// <summary>
        /// Get element key.
        /// </summary>
        /// <param name="element">Configuration element.</param>
        /// <returns>Return object.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ServerElement)element).ServerIp + ((ServerElement)element).ServerName
                + ((ServerElement)element).ConnectionString;
        }
    }
}