// --------------------------------------------------------------------------
// <copyright file="MailElement.cs" company="Nexcel Solutions Vietnam">
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
    /// Sp name element.
    /// </summary>
    public class MailElement : ConfigurationElement
    {
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