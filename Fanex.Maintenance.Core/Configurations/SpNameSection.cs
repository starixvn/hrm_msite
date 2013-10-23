// --------------------------------------------------------------------------
// <copyright file="SpNameSection.cs" company="Nexcel Solutions Vietnam">
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
    /// SP name section.
    /// </summary>
    public class SpNameSection : ConfigurationSection
    {
        /// <summary>
        /// Static instance for Reference.
        /// </summary>
        public static SpNameSection Instance
        {
            get
            {
                var storeProcedureNameSection = ConfigurationManager.GetSection("SpNameSection") as SpNameSection;

                return storeProcedureNameSection;
            }
        }
        
        /// <summary>
        /// List Sp Name.
        /// </summary>
        [ConfigurationProperty("spList", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(SpNameCollection), AddItemName = "add")]
        public SpNameCollection SpNames
        {
            get
            {
                var storeProcedureNameCollection = (SpNameCollection)base["spList"];

                return storeProcedureNameCollection;
            }
        }
    }

    /// <summary>
    /// Sp Name Collection.
    /// </summary>
    public class SpNameCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SpNameCollection()
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
        /// Get Element with index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
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
        /// Base Add.
        /// </summary>
        /// <param name="element"></param>
        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        /// <summary>
        /// Create new Element.
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new SpNameElement();
        }

        /// <summary>
        /// Get Element Key.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SpNameElement)element).Key + ((SpNameElement)element).Name;
        }
    }

    /// <summary>
    /// Sp name element.
    /// </summary>
    public class SpNameElement : ConfigurationElement
    {
        [ConfigurationProperty("Key", IsRequired = true, IsKey = true)]
        public string Key
        {
            get
            {
                return (string)this["Key"];
            }

            set
            {
                this["Key"] = value;
            }
        }

        [ConfigurationProperty("Name", IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)this["Name"];
            }

            set
            {
                this["Name"] = value;
            }
        }
    }
}