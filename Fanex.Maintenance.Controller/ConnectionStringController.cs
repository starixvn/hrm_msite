// --------------------------------------------------------------------------
// <copyright file="ConnectionStringController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.21">Created</change>
// </history>

namespace Fanex.Maintenance.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Fanex.ConnectionString.Core.Criteria.LogExecSp;
    using Fanex.ConnectionString.Core.Service.Impl;
    using Fanex.ConnectionString.Core.Service.Interface;
    using Fanex.ConnectionString.Domain;
    using Fanex.ConnectionString.Model;
    using Fanex.Maintenance.ViewModel.Connection;
    using Fanex.NAP.Core.Helpers;
    using Fanex.NAP.Core.Web;

    /// <summary>
    /// Connection string controller.
    /// </summary>
    [Authorize]
    public class ConnectionStringController : BaseController
    {
        #region Member

        private readonly IDatabaseServerService _databaseServerService;

        private readonly ISiteService _siteService;

        private readonly IConnectionService _connectionService;

        private readonly ILogService _logService;

        private readonly IDatabaseService _databaseService;

        #endregion Member

        #region Ctor

        public ConnectionStringController()
            : this(null, null, null, null, null)
        {
        }

        public ConnectionStringController(
            IDatabaseServerService databaseServerService,
            ISiteService siteService,
            IConnectionService connectionService,
            ILogService logServer,
            IDatabaseService aliasDatabaseService)
        {
            _databaseServerService = databaseServerService ?? new DatabaseServerService { ServiceContext = ServiceContext };
            _siteService = siteService ?? new SiteService { ServiceContext = ServiceContext };
            _connectionService = connectionService ?? new ConnectionService { ServiceContext = ServiceContext };
            _logService = logServer ?? new LogService { ServiceContext = ServiceContext };
            _databaseService = aliasDatabaseService ?? new DatabaseService { ServiceContext = ServiceContext };
        }

        #endregion Ctor

        [HttpGet]
        public ActionResult Connection(SearchConnectionViewModel model)
        {
            if (ReferenceEquals(model, null))
            {
                model = new SearchConnectionViewModel
                {
                    DbServerId = 0
                };
            }

            var listConnection = _connectionService.GetAll(model.DbServerId);
            var listDatabase = _databaseServerService.GetAll(isActive: true);

            model.DatabaseServers = listDatabase.ToList();
            model.Connections = listConnection.ToList();

            return View(model);
        }

        [HttpGet]
        public ActionResult CreateConnection()
        {
            var listDatabaseServer = _databaseServerService.GetDatabaseServerHasDatabases();
            var databaseServer = listDatabaseServer.FirstOrDefault();
            var listDatabase = _databaseService.GetByDbServerId(databaseServer.Id);
            var database = listDatabaseServer.FirstOrDefault();

            var model = new ConnectionViewModel
            {
                DatabaseServers = listDatabaseServer.ToList(),
                Databases = listDatabase.ToList(),
                DatabaseId = database.Id
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateConnection(ConnectionViewModel model)
        {
            var listDatabaseServer = _databaseServerService.GetDatabaseServerHasDatabases();
            var database = _databaseService.GetById(model.DatabaseId);
            var listDatabase = _databaseService.GetByDbServerId(model.DbServerId);

            model.DatabaseServers = listDatabaseServer.ToList();
            model.Databases = listDatabase.ToList();
            model.Name = database.ServerAlias + "." + database.Name + "." + model.LoginName;

            if (ModelState.IsValid)
            {
                var connection = new Connection
                {
                    DatabaseServer = new DatabaseServer { Id = model.DbServerId },
                    Database = new Database { Id = model.DatabaseId },
                    FreeText = model.FreeText,
                    IsActive = model.IsActive,
                    LoginName = model.LoginName,
                    Name = model.Name,
                    Password = model.Password
                };

                var command = _connectionService.Insert(connection);

                ViewBag.Success = command.IsSuccessful;
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult EditConnection(int id)
        {
            var connection = _connectionService.GetById(id);

            var listDatabaseServer = _databaseServerService.GetDatabaseServerHasDatabases();
            var listDatabase = _databaseService.GetByDbServerId(connection.DatabaseServer.Id);

            var model = new ConnectionViewModel
            {
                DatabaseServers = listDatabaseServer.ToList(),
                Databases = listDatabase.ToList(),
                DatabaseId = connection.Database.Id,
                DbServerId = connection.DatabaseServer.Id,
                FreeText = connection.FreeText,
                Id = connection.Id,
                IsActive = connection.IsActive,
                LoginName = connection.LoginName,
                Name = connection.Name,
                Password = connection.Password
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult EditConnection(ConnectionViewModel model)
        {
            var listDatabaseServer = _databaseServerService.GetDatabaseServerHasDatabases();
            var listDatabase = _databaseService.GetByDbServerId(model.DbServerId);
            var database = _databaseService.GetById(model.DatabaseId);

            model.DatabaseServers = listDatabaseServer.ToList();
            model.Databases = listDatabase.ToList();

            if (ModelState.IsValid)
            {
                var connection = new Connection
                {
                    DatabaseServer = new DatabaseServer { Id = model.DbServerId },
                    Database = new Database { Id = model.DatabaseId },
                    FreeText = model.FreeText,
                    IsActive = model.IsActive,
                    LoginName = model.LoginName,
                    Name = database.ServerAlias + "." + database.Name + "." + model.LoginName,
                    Password = model.Password,
                    Id = model.Id
                };

                var command = _connectionService.Update(connection);

                ViewBag.Success = command.IsSuccessful;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateStatusConnection(int id, bool isActive)
        {
            if (Request.IsAjaxRequest())
            {
                var command = _connectionService.UpdateStatus(id, isActive);

                return Json(command.IsSuccessful.ToString().ToLower());
            }

            return Json(null);
        }

        [HttpGet]
        public ActionResult SwitchConnection(SwitchConnectionViewModel model)
        {
            var listDatabase = _databaseServerService.GetAll(isActive: true);

            if (ReferenceEquals(model, null) || model.FromDbServerId == 0)
            {
                model = new SwitchConnectionViewModel
                {
                    FromDbServerId = 0,
                    DatabaseServers = listDatabase.ToList()
                };

                return View(model);
            }

            var listConnection = _connectionService.GetAll(model.FromDbServerId);

            model.DatabaseServers = listDatabase.ToList();
            model.Connections = listConnection.ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult SwithConnectionDbServer(string connectionIds, int toDbServerId)
        {
            if (Request.IsAjaxRequest())
            {
                if (!string.IsNullOrWhiteSpace(connectionIds)
                    && toDbServerId != 0)
                {
                    var command = _connectionService.SwitchDbServer(connectionIds, toDbServerId);

                    return Json(command.IsSuccessful);
                }
            }

            return Json(null);
        }

        [HttpGet]
        public ActionResult GetDatabaseByDbServerId(int dbServerId)
        {
            if (Request.IsAjaxRequest())
            {
                var listDatabase = _databaseService.GetByDbServerId(dbServerId);

                return Json(listDatabase, JsonRequestBehavior.AllowGet);
            }

            return Json(null);
        }

        #region Manage Site

        /// <summary>
        /// Site list.
        /// </summary>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult Site(SearchSiteViewModel model)
        {
            if (ReferenceEquals(model, null))
            {
                model = new SearchSiteViewModel { Name = string.Empty };
            }

            // get all site with number of alias by site
            var listSite = _siteService.GetAll(model.Name);
            model.Sites = listSite.ToList();

            return View(model);
        }

        /// <summary>
        /// Create site.
        /// </summary>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult CreateSite()
        {
            return View();
        }

        /// <summary>
        /// Create site.
        /// </summary>
        /// <param name="model">The criteria.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult CreateSite(SiteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var listSite = _siteService.GetAll();
                var site = listSite.Where(x => x.Name.ToLower() == model.Name.ToLower()).FirstOrDefault();

                if (!ReferenceEquals(site, null))
                {
                    ModelState.AddModelError("Name",
                            string.Format(ViewResources.DuplicateMsg, site.Name));

                    return View(model);
                }

                var command = _siteService.Insert(
                    new Site
                    {
                        Name = model.Name,
                        Description = model.Description,
                        IsActive = model.IsActive
                    });

                ViewBag.Success = command.IsSuccessful;
            }

            return View(model);
        }

        /// <summary>
        /// Update site.
        /// </summary>
        /// <param name="id">Site object ID.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult EditSite(int id)
        {
            // get site by id
            var site = _siteService.GetById(id);

            var model = new SiteViewModel
            {
                Id = site.Id,
                Name = site.Name,
                Description = site.Description,
                IsActive = site.IsActive
            };

            return View(model);
        }

        /// <summary>
        /// Update site.
        /// </summary>
        /// <param name="model">The criteria.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult EditSite(SiteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var listSite = _siteService.GetAll();
                var site = listSite.Where(x => x.Name.ToLower() == model.Name.ToLower() && x.Id != model.Id).FirstOrDefault();

                if (!ReferenceEquals(site, null))
                {
                    ModelState.AddModelError("Name",
                            string.Format(ViewResources.DuplicateMsg, site.Name));

                    return View(model);
                }

                var command = _siteService.Update(
                    new Site
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Description = model.Description,
                        IsActive = model.IsActive
                    });

                ViewBag.Success = command.IsSuccessful;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateStatusSite(int siteId, bool isActive)
        {
            if (Request.IsAjaxRequest())
            {
                var command = _siteService.UpdateStatus(siteId, isActive);

                return Json(command.IsSuccessful);
            }

            return Json(null);
        }

        #endregion Site

        #region Database Server

        /// <summary>
        /// Database server list.
        /// </summary>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult DatabaseServer(SearchDatabaseServerViewModel model)
        {
            if (ReferenceEquals(model, null))
            {
                model = new SearchDatabaseServerViewModel { Name = string.Empty };
            }

            var listDatabase = _databaseServerService.GetAll(model.Name);
            model.DatabaseServers = listDatabase.ToList();

            return View(model);
        }

        /// <summary>
        /// Create database server.
        /// </summary>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult CreateDatabaseServer()
        {
            return View();
        }

        /// <summary>
        /// Create database server.
        /// </summary>
        /// <param name="model">The criteria.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult CreateDatabaseServer(DatabaseServerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var listDatabaseServer = _databaseServerService.GetAll();
                var databaseServer = listDatabaseServer.Where(x => x.Name.ToLower() == model.Name.ToLower()).FirstOrDefault();

                if (!ReferenceEquals(databaseServer, null))
                {
                    ModelState.AddModelError("Name",
                              string.Format(ViewResources.DuplicateMsg, databaseServer.Name));

                    return View(model);
                }

                var command = _databaseServerService.Insert(new DatabaseServer
                {
                    Description = model.Description,
                    Id = model.Id,
                    IsActive = model.IsActive,
                    Name = model.Name,
                    ServerIP = model.ServerIP
                });

                if (!command.IsSuccessful)
                {
                    return View(model);
                }

                ViewBag.Success = command.IsSuccessful;
            }

            return View(model);
        }

        /// <summary>
        /// Edit database server.
        /// </summary>
        /// <param name="id">DB server ID.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult EditDatabaseServer(int id)
        {
            // get by id
            var databaseServer = _databaseServerService.GetById(id);

            var model = new DatabaseServerViewModel
            {
                Id = databaseServer.Id,
                Description = databaseServer.Description,
                IsActive = databaseServer.IsActive,
                Name = databaseServer.Name,
                ServerIP = databaseServer.ServerIP
            };

            return View(model);
        }

        /// <summary>
        /// Edit database server.
        /// </summary>
        /// <param name="model">The criteria.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult EditDatabaseServer(DatabaseServerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var listDatabaseServer = _databaseServerService.GetAll();
                var databaseServer = listDatabaseServer.Where(x => x.Name.ToLower() == model.Name.ToLower() && x.Id != model.Id).FirstOrDefault();

                if (!ReferenceEquals(databaseServer, null))
                {
                    ModelState.AddModelError("Name",
                              string.Format(ViewResources.DuplicateMsg, databaseServer.Name));

                    return View(model);
                }

                var command = _databaseServerService.Update(new DatabaseServer
                {
                    Description = model.Description,
                    Id = model.Id,
                    IsActive = model.IsActive,
                    Name = model.Name,
                    ServerIP = model.ServerIP
                });

                if (!command.IsSuccessful)
                {
                    return View(model);
                }

                ViewBag.Success = command.IsSuccessful;
            }

            return View(model);
        }

        /// <summary>
        /// Update database server status.
        /// </summary>
        /// <param name="databaseServerId">Database server ID.</param>
        /// <param name="isActive">Is active or not.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult UpdateStatusDatabaseServer(int databaseId, bool isActive)
        {
            if (Request.IsAjaxRequest())
            {
                var command = _databaseServerService.UpdateStatus(databaseId, isActive);

                return Json(command.IsSuccessful.ToString().ToLower());
            }

            return Json(null);
        }

        #endregion DB Server

        #region Database

        [HttpGet]
        public ActionResult Database(SearchDatabaseViewModel model)
        {
            if (ReferenceEquals(model, null))
            {
                model = new SearchDatabaseViewModel { DbServerId = 0 };
            }

            var listDatabase = _databaseService.GetAll(model.DbServerId);
            var listDatabaseServer = _databaseServerService.GetAll(isActive: true);

            model.Database = listDatabase.ToList();
            model.DatabaseServers = listDatabaseServer.ToList();

            return View(model);
        }

        [HttpGet]
        public ActionResult CreateDatabase(int databaseId)
        {
            var listDatabaseServer = _databaseServerService.GetAll(isActive: true);

            var model = new DatabaseViewModel
            {
                DbServerId = databaseId,
                DatabaseServers = listDatabaseServer.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateDatabase(DatabaseViewModel model)
        {
            var listDatabaseServer = _databaseServerService.GetAll(isActive: true);
            model.DatabaseServers = listDatabaseServer.ToList();

            if (ModelState.IsValid)
            {
                var listDatabase = _databaseService.GetAll(model.DbServerId);
                var database = listDatabase.Where(x => x.Name.ToLower() == model.Name.ToLower()).FirstOrDefault();

                if (!ReferenceEquals(database, null))
                {
                    ModelState.AddModelError("Name",
                              string.Format(ViewResources.DuplicateMsg, database.Name));
                    ModelState.AddModelError("DbServerId",
                              string.Format(ViewResources.DuplicateMsg, database.DatabaseServer.Name));

                    return View(model);
                }

                var command = _databaseService.Insert(new Database
                                {
                                    Name = model.Name,
                                    ServerAlias = model.ServerAlias,
                                    IsActive = model.IsActive,
                                    DatabaseServer = new DatabaseServer { Id = model.DbServerId }
                                });

                ViewBag.Success = command.IsSuccessful;
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult EditDatabase(int id)
        {
            var model = new DatabaseViewModel();
            var database = _databaseService.GetById(id);
            var listDatabaseServer = _databaseServerService.GetAll(isActive: true);
            model.DatabaseServers = listDatabaseServer.ToList();
            model.Id = database.Id;
            model.Name = database.Name;
            model.ServerAlias = database.ServerAlias;
            model.IsActive = database.IsActive;
            model.DbServerId = database.DatabaseServer.Id;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditDatabase(DatabaseViewModel model)
        {
            var listDatabaseServer = _databaseServerService.GetAll(isActive: true);
            model.DatabaseServers = listDatabaseServer.ToList();

            if (ModelState.IsValid)
            {
                var listDatabase = _databaseService.GetAll(model.DbServerId);
                var aliasDatabase = listDatabase.Where(x => x.Name.ToLower() == model.Name.ToLower() && x.Id != model.Id).FirstOrDefault();

                if (!ReferenceEquals(aliasDatabase, null))
                {
                    ModelState.AddModelError("Name",
                              string.Format(ViewResources.DuplicateMsg, aliasDatabase.Name));

                    return View(model);
                }

                var command = _databaseService.Update(new Database
                {
                    Id = model.Id,
                    Name = model.Name,
                    ServerAlias = model.ServerAlias,
                    IsActive = model.IsActive,
                    DatabaseServer = new DatabaseServer { Id = model.DbServerId }
                });

                ViewBag.Success = command.IsSuccessful;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateStatusDatabase(int databaseId, bool isActive)
        {
            if (Request.IsAjaxRequest())
            {
                var command = _databaseService.UpdateStatus(databaseId, isActive);

                return Json(command.IsSuccessful);
            }

            return Json(false);
        }

        #endregion

        #region Log management

        /// <summary>
        /// View log page.
        /// </summary>
        /// <param name="id">Page index.</param>
        /// <param name="size">Number of items per page.</param>
        /// <param name="q">Log name filter.</param>
        /// <param name="username">Username filter.</param>
        /// <param name="from">From DateTime.</param>
        /// <param name="to">To DateTime.</param>
        /// <param name="filterBy">Draw filter.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult Log(
            int id = 1,
            int size = 20,
            string q = "",
            string username = "",
            DateTime? from = null,
            DateTime? to = null,
            int filterBy = 1)
        {
            var criteria = new LogExecSpSearchCriteria
            {
                PageIndex = id,
                PageSize = size,
                Name = q,
                Username = username,
                From = from ?? DateTime.Now,
                To = to ?? DateTime.Now.AddDays(1),
                FilterType = filterBy
            };

            int totalLogs = 0;

            criteria.Name = criteria.Name ?? string.Empty;

            IQueryable<LogExecSp> listLog = _logService.SearchExecSp(
                                                criteria.PageIndex,
                                                criteria.PageSize,
                                                out totalLogs,
                                                criteria.Name,
                                                criteria.Username,
                                                criteria.From,
                                                criteria.To,
                                                criteria.FilterType);
            ViewBag.ListLog = new PagerList<LogExecSp>(criteria.PageIndex, criteria.PageSize, totalLogs, listLog);

            return View(criteria);
        }

        /// <summary>
        /// Search log.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult Log(LogExecSpSearchCriteria criteria)
        {
            return RedirectToAction(
             "Log",
             new
             {
                 id = criteria.PageIndex,
                 size = criteria.PageSize,
                 q = criteria.Name,
                 username = criteria.Username,
                 from = criteria.From.ToString("MMM dd,yyyy"),
                 to = criteria.To.ToString("MMM dd,yyyy"),
                 filterBy = criteria.FilterType
             });
        }

        #endregion Log management

        #region Partial view

        /// <summary>
        /// Insert connection log alias.
        /// </summary>
        /// <param name="log">Log execute SP.</param>
        /// <returns>Partial view result.</returns>
        public PartialViewResult _ConnectionLog_Alias_Insert(LogExecSp log)
        {
            var listSite = new List<LogData>();
            var listData = _logService.GetValueAliasInsert(log, ref listSite);

            ViewBag.ListSite = listSite;

            return PartialView(listData);
        }

        /// <summary>
        /// Update connection log alias.
        /// </summary>
        /// <param name="log">Log execute SP.</param>
        /// <returns>Partial view result.</returns>
        public PartialViewResult _ConnectionLog_Alias_Update(LogExecSp log)
        {
            var listSite = new List<LogData>();
            var listOldSite = new List<LogData>();

            var listData = _logService.GetValueAliasUpdate(log, ref listSite, ref listOldSite);

            ViewBag.ListSite = listSite;
            ViewBag.ListOldSite = listOldSite;

            return PartialView(listData);
        }

        /// <summary>
        /// View log data.
        /// </summary>
        /// <param name="log">Log execute SP.</param>
        /// <returns>Partial view result.</returns>
        public PartialViewResult _ConnectionLog_LogData(LogExecSp log)
        {
            var listData = new List<LogData>();

            switch (log.ActionId)
            {
                case (int)ConLogAction.ConnectionString_ConnectionString_Insert:
                    listData = _logService.GetValueConnectionInsert(log);
                    ViewBag.IsNew = true;

                    return PartialView(listData);

                case (int)ConLogAction.ConnectionString_ConnectionString_Update:
                case (int)ConLogAction.ConnectionString_ConnectionString_UpdateDefaultConnection:
                    listData = _logService.GetValueConnectionUpdate(log);
                    ViewBag.IsNew = false;

                    return PartialView(listData);

                case (int)ConLogAction.ConnectionString_DatabaseServer_Insert:
                    listData = _logService.GetValueDatabaseServerInsert(log);
                    ViewBag.IsNew = true;

                    return PartialView(listData);

                case (int)ConLogAction.ConnectionString_DatabaseServer_Update:
                    listData = _logService.GetValueDatabaseServerUpdate(log);
                    ViewBag.IsNew = false;

                    return PartialView(listData);

                case (int)ConLogAction.ConnectionString_Site_Insert:
                    listData = _logService.GetValueSiteInsert(log);
                    ViewBag.IsNew = true;

                    return PartialView(listData);

                case (int)ConLogAction.ConnectionString_Site_Update:
                    listData = _logService.GetValueSiteUpdate(log);
                    ViewBag.IsNew = false;

                    return PartialView(listData);
            }

            listData = _logService.GetValueUpdateStatus(log);
            ViewBag.IsNew = false;

            return PartialView(listData);
        }

        /// <summary>
        /// Change connection.
        /// </summary>
        /// <param name="log">Execute SP log.</param>
        /// <returns>Partial view result.</returns>
        public PartialViewResult _ConnectionLog_LogData_ChangeConnection(LogExecSp log)
        {
            var listOld = new List<Connection>();
            var listConnection = _logService.GetValueChangeConnection(log, ref listOld);

            ViewBag.ListOld = listOld;

            return PartialView(listConnection);
        }

        #endregion Partial view

        #region Private

        /// <summary>
        /// Get select list item of all database server.
        /// </summary>
        /// <param name="selectedId">Selected ID.</param>
        /// <returns>List of selected list item.</returns>
        private List<SelectListItem> GetSelectListDatabaseServer(int? selectedId = null)
        {
            // get all
            var listDatabaseServer = _databaseServerService.GetAll();

            return listDatabaseServer.Select(d => new SelectListItem
            {
                Text = string.Format("{0} ({1})", d.ServerIP, d.Name),
                Value = d.Id.ToString(),
                Selected = selectedId != null ? d.Id == selectedId : false
            }).ToList();
        }

        /// <summary>
        /// Get select list item of all site.
        /// </summary>
        /// <param name="selectedId">Selected ID.</param>
        /// <returns>List of selected list item.</returns>
        private List<SelectListItem> GetSelectListSite(int? selectedId = null)
        {
            // get all
            var listSite = _siteService.GetAll();

            return listSite.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString(),
                Selected = selectedId != null ? s.Id == selectedId : false
            }).ToList();
        }

        #endregion Private
    }
}