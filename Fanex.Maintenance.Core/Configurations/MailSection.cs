// --------------------------------------------------------------------------
// <copyright file="MailSection.cs" company="Nexcel Solutions Vietnam">
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
    public class MailSection : ConfigurationSection
    {
        /// <summary>
        /// List Sp Name
        /// </summary>
        [ConfigurationProperty("Mails", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(MailCollection), AddItemName = "add")]
        public MailCollection Mails
        {
            get
            {
                var mailCollection = (MailCollection)base["Mails"];

                return mailCollection;
            }
        }

        /// <summary>
        /// Static instance for Reference
        /// </summary>
        public static MailSection Instance
        {
            get
            {
                var mailSection = ConfigurationManager.GetSection("MailSection") as MailSection;
                return mailSection;
            }
        }
    }

    /// <summary>
    /// Sp Name Collection
    /// </summary>
    public class MailCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MailCollection()
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
        public void Add(MailElement mappingElement)
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
        /// Create new Element
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new MailElement();
        }

        /// <summary>
        /// Get Element Key
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((MailElement)element).From + ((MailElement)element).To + ((MailElement)element).BCC + ((MailElement)element).CC;
        }

        /// <summary>
        /// Get Element with index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public MailElement this[int index]
        {
            get
            {
                return (MailElement)BaseGet(index);
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
    /// Sp name element
    /// </summary>
    public class MailElement : ConfigurationElement
    {
        /// <summary>
        /// Key Attribute
        /// </summary>
        [ConfigurationProperty("To", IsRequired = true, IsKey = true)]
        public string To
        {
            get
            {
                return (string)this["To"];
            }

            set
            {
                this["To"] = value;
            }
        }

        /// <summary>
        /// Name Attribute
        /// </summary>
        [ConfigurationProperty("From", IsRequired = true)]
        public string From
        {
            get
            {
                return (string)this["From"];
            }

            set
            {
                this["From"] = value;
            }
        }

        [ConfigurationProperty("CC", IsRequired = true)]
        public string CC
        {
            get
            {
                return (string)this["CC"];
            }

            set
            {
                this["CC"] = value;
            }
        }

        [ConfigurationProperty("BCC", IsRequired = true)]
        public string BCC
        {
            get
            {
                return (string)this["BCC"];
            }

            set
            {
                this["BCC"] = value;
            }
        }
    }
}