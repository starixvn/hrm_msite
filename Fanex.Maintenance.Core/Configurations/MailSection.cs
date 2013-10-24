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
    /// Mail section.
    /// </summary>
    public class MailSection : ConfigurationSection
    {
        /// <summary>
        /// Gets static instance for Reference.
        /// </summary>
        /// <value>Mail section.</value>
        public static MailSection Instance
        {
            get
            {
                var mailSection = ConfigurationManager.GetSection("MailSection") as MailSection;
                return mailSection;
            }
        }
        
        /// <summary>
        /// Gets list Sp Name.
        /// </summary>
        /// <value>Mail collection.</value>
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
    }
}