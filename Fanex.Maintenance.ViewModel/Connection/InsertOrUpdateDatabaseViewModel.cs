// --------------------------------------------------------------------------
// <copyright file="InsertOrUpdateAliasDatabaseViewModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.12.5">Created</change>
// </history>

namespace Fanex.Maintenance.ViewModel.Connection
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Fanex.ConnectionString.Domain;

    public class InsertOrUpdateDatabaseViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ServerAlias { get; set; }

        public bool IsActive { get; set; }

        public int DbServerId { get; set; }

        public List<DatabaseServer> DatabaseServers { get; set; }

        public List<SelectListItem> SelectListDatabaseServer
        {
            get
            {
                return DatabaseServers.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = x.Id == DbServerId }).ToList();
            }
        }
    }
}
