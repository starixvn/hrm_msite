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
        /// Gets static instance for Reference.
        /// </summary>
        /// <value>SP name section.</value>
        public static SpNameSection Instance
        {
            get
            {
                var storeProcedureNameSection = ConfigurationManager.GetSection("SpNameSection") as SpNameSection;

                return storeProcedureNameSection;
            }
        }
        
        /// <summary>
        /// Gets list Sp Name.
        /// </summary>
        /// <value>SP name collection.</value>
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
}