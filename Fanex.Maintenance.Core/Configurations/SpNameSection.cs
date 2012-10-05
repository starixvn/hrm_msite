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
    /// TODO: Update summary.
    /// </summary>
    public class SpNameSection : ConfigurationSection
    {
        [ConfigurationProperty("spList", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(SpNameCollection),
            AddItemName = "add")]
        public SpNameCollection SpNames
        {
            get
            {
                var spNameCollection =
                    (SpNameCollection)base["spList"];
                return spNameCollection;
            }
        }

        public static SpNameSection Instance
        {
            get
            {
                var spNameSection =
                 ConfigurationManager.GetSection("SpNameSection") as SpNameSection;
                return spNameSection;
            }
        }
    }

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

        public void Add(SpNameElement mappingElement)
        {
            BaseAdd(mappingElement);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new SpNameElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SpNameElement)element).Key + ((SpNameElement)element).Name;
        }

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
    }

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