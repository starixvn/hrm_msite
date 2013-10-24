// --------------------------------------------------------------------------
// <copyright file="SpNameElement.cs" company="Nexcel Solutions Vietnam">
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