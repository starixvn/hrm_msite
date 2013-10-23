// --------------------------------------------------------------------------
// <copyright file="SpNameCollection.cs" company="Nexcel Solutions Vietnam">
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
    /// Sp Name Collection.
    /// </summary>
    public class SpNameCollection : ConfigurationElementCollection
    {
        public SpNameCollection()
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
        /// Get Element with index.
        /// </summary>
        /// <param name="index">Index value.</param>
        /// <returns>SP name element.</returns>
        public SpNameElement this[int index]
        {
            get
            {
                return (SpNameElement)BaseGet(index);
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
        public void Add(SpNameElement mappingElement)
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
        /// Create new Element.
        /// </summary>
        /// <returns>Configuration element.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new SpNameElement();
        }

        /// <summary>
        /// Get Element Key.
        /// </summary>
        /// <param name="element">Configuration element.</param>
        /// <returns>Returned object.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SpNameElement)element).Key + ((SpNameElement)element).Name;
        }
    }
}