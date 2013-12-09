// --------------------------------------------------------------------------
// <copyright file="InsertOrUpdateConnectionViewModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.12.6">Created</change>
// </history>

namespace Fanex.Maintenance.ViewModel.Connection
{
    using System.Web.Mvc;
    using System.Collections.Generic;
    using Fanex.ConnectionString.Domain;
    using System.Linq;

    public class InsertOrUpdateConnectionViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int DatabaseId { get; set; }

        public int DbServerId { get; set; }

        public string LoginName { get; set; }

        public string Password { get; set; }

        public string FreeText { get; set; }

        public bool IsActive { get; set; }

        public List<Database> Databases { get; set; }

        public List<DatabaseServer> DatabaseServers { get; set; }

        public List<SelectListItem> SelectListDatabase
        {
            get
            {
                return Databases.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                    Selected = x.Id == DatabaseId
                }).ToList();
            }
        }

        public List<SelectListItem> SelectListDatabaseServer
        {
            get
            {
                return DatabaseServers.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                    Selected = x.Id == DbServerId
                }).ToList();
            }
        }
    }
}
