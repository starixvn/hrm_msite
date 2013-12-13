// --------------------------------------------------------------------------
// <copyright file="SearchSiteViewModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.12.6">Created</change>
// </history>

namespace Fanex.Maintenance.ViewModel.Connection
{
    using System.Collections.Generic;
    using Fanex.ConnectionString.Domain;

    /// <summary>
    /// The view model for search site.
    /// </summary>
    public class SearchSiteViewModel
    {
        public SearchSiteViewModel()
        {
            Name = string.Empty;
            HasEditPermission = false;
        }

        public string Name { get; set; }

        public List<Site> Sites { get; set; }

        public bool HasEditPermission { get; set; }
    }
}
