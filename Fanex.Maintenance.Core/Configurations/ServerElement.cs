// --------------------------------------------------------------------------
// <copyright file="ServerElement.cs" company="Nexcel Solutions Vietnam">
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

        public int SqlServerAgentStatus { get; set; }
    }
}