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
    using Fanex.ConnectionString.Domain;
    using Fanex.Maintenance.Criteria.ConnectionString.Alias;
    using Fanex.Maintenance.Criteria.ConnectionString.Connection;
    using Fanex.Maintenance.Criteria.ConnectionString.DatabaseServer;
    using Fanex.Maintenance.Criteria.ConnectionString.LogExecSp;
    using Fanex.Maintenance.Criteria.ConnectionString.Site;
    using Fanex.Maintenance.Entity.Model.ConnectionString;
    using Fanex.Maintenance.Service.Impl.ConnectionString;
    using Fanex.Maintenance.Service.Interface.ConnectionString;
    using Fanex.NAP.Core.Helpers;

    /// <summary>
    /// Connection string controller.
    /// </summary>
    [Authorize]
    public class ConnectionStringController : BaseController
    {
        #region Member

        private readonly IDatabaseServerService _databaseServerService;

        private readonly IAliasService _aliasService;

        private readonly ISiteService _siteService;

        private readonly IConnectionService _connectionService;

        private readonly ILogService _logService;

        #endregion Member

        #region Ctor

        public ConnectionStringController()
            : this(null, null, null, null, null)
        {
        }

        public ConnectionStringController(
            IAliasService aliasService,
            IDatabaseServerService databaseServerService,
            ISiteService siteService,
            IConnectionService connectionService,
            ILogService logServer)
        {
            _aliasService = aliasService ?? new AliasService { ServiceContext = ServiceContext };
            _databaseServerService = databaseServerService ?? new DatabaseServerService { ServiceContext = ServiceContext };
            _siteService = siteService ?? new SiteService { ServiceContext = ServiceContext };
            _connectionService = connectionService ?? new ConnectionService { ServiceContext = ServiceContext };
            _logService = logServer ?? new LogService { ServiceContext = ServiceContext };
        }

        #endregion Ctor

        #region Manage Alias

        #region Mange

        /// <summary>
        /// Alias list.
        /// </summary>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult Alias()
        {
            // get all database server
            ViewBag.DatabaseServer = GetSelectListDatabaseServer();

            // get all site
            ViewBag.Site = GetSelectListSite();

            // get all alias
            var listAlias = _aliasService.GetAll();
            ViewBag.ListAlias = listAlias;

            return View();
        }

        /// <summary>
        /// Alias list.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult Alias(AliasGetAllCriteria criteria)
        {
            // get all database server with selected current database server
            ViewBag.DatabaseServer = GetSelectListDatabaseServer(criteria.DbServerId);

            // get all site with selected current site
            ViewBag.Site = GetSelectListSite(criteria.SiteId);

            // get alias filter by site and database server
            var listAlias = _aliasService.GetAll(criteria.Alias, criteria.DbServerId, criteria.SiteId);
            ViewBag.ListAlias = listAlias;

            return View(criteria);
        }

        #endregion Mange

        #region Create

        /// <summary>
        /// Create alias.
        /// </summary>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult CreateAlias()
        {
            // get list site
            var listSite = _siteService.GetAll();
            ViewBag.Site = listSite.ToList();

            return View();
        }

        /// <summary>
        /// Create alias.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult CreateAlias(AliasInsertCriteria criteria)
        {
            // get list site
            var listSite = _siteService.GetAll();
            ViewBag.Site = listSite.ToList();

            criteria.Creator = CurrentSession.UserId;
            criteria.CreatedDate = DateTime.Now;

            // insert new alias
            var command = _aliasService.InsertOrUpdate(criteria);

            if (!command.IsSuccessful)
            {
                return View(criteria);
            }

            ViewBag.Success = command.IsSuccessful;

            return View(criteria);
        }

        #endregion Create

        #region Edit

        /// <summary>
        /// Update alias.
        /// </summary>
        /// <param name="id">Alias object ID.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult EditAlias(int id)
        {
            // get alias by id
            var alias = _aliasService.Get(id);
            if (ReferenceEquals(alias, null))
            {
                return RedirectToAction("NotFound", "CommonError");
            }

            ViewBag.Alias = alias;

            // get list site
            var listSite = _siteService.GetAll();
            ViewBag.Site = listSite.ToList();

            // get list site with alias
            var listSiteAlias = _siteService.GetByAlias(id);
            ViewBag.SiteAlias = listSiteAlias;

            return View();
        }

        /// <summary>
        /// Update alias.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult EditAlias(AliasUpdateCriteria criteria)
        {
            // get all site
            var listSite = _siteService.GetAll();
            ViewBag.Site = listSite.ToList();

            criteria.Modifier = CurrentSession.UserId;
            criteria.ModifiedDate = DateTime.Now;

            // update alias
            var command = _aliasService.InsertOrUpdate(criteria);

            if (!command.IsSuccessful)
            {
                return View(criteria);
            }

            ViewBag.Success = command.IsSuccessful;

            var alias = _aliasService.Get(criteria.AliasId);
            ViewBag.Alias = alias;

            var listSiteAlias = _siteService.GetByAlias(criteria.AliasId);
            ViewBag.SiteAlias = listSiteAlias;

            return View();
        }

        #endregion Edit

        #endregion Manage Alias

        #region Connection

        #region Manage

        /// <summary>
        /// Connection list.
        /// </summary>
        /// <param name="aliasId">Alias object ID.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult Connection(int aliasId)
        {
            // get all connection by alias id
            var listConnection = _connectionService.GetByAlias(aliasId);

            var alias = _aliasService.Get(aliasId);
            ViewBag.Alias = alias;

            return View(listConnection);
        }

        #endregion Manage

        #region Create

        /// <summary>
        /// Create connection.
        /// </summary>
        /// <param name="aliasId">Alias object ID.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult CreateConnection(int aliasId)
        {
            // get alias by id
            var alias = _aliasService.Get(aliasId);

            if (ReferenceEquals(alias, null))
            {
                return RedirectToAction("NotFound", "CommonError");
            }

            ViewBag.Alias = alias;
            ViewBag.DbServer = GetSelectListDatabaseServer();

            // get connection for select secondary connection string
            var listConnection = _connectionService.GetByAlias(aliasId);
            ViewBag.ListConnection = listConnection.Select(c => new SelectListItem
            {
                Value = c.ConnectionString.Id.ToString(),
                Text = c.ConnectionString.Connection
            }).ToList();

            return View(new ConnectionInsertCriteria
            {
                AliasId = aliasId
            });
        }

        /// <summary>
        /// Create connection.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult CreateConnection(ConnectionInsertCriteria criteria)
        {
            var alias = _aliasService.Get(criteria.AliasId);
            if (ReferenceEquals(alias, null))
            {
                return RedirectToAction("NotFound", "CommonError");
            }

            var connection = _connectionService.GetByAlias(alias.Id);
            if (!connection.Any())
            {
                criteria.IsDefault = true;
            }

            ViewBag.Alias = alias;

            // get database server
            ViewBag.DbServer = GetSelectListDatabaseServer();

            // get list connection for select secondary connection string
            var listConnection = _connectionService.GetByAlias(criteria.AliasId);
            ViewBag.ListConnection = listConnection.Select(c => new SelectListItem
            {
                Value = c.ConnectionString.Id.ToString(),
                Text = c.ConnectionString.Connection
            }).ToList();

            criteria.Creator = CurrentSession.UserId;
            criteria.CreatedDate = DateTime.Now;

            // insert new alias
            var command = _connectionService.InsertOrUpdate(criteria);
            if (!command.IsSuccessful)
            {
                return View(criteria);
            }

            ViewBag.Success = command.IsSuccessful;

            return View(criteria);
        }

        #endregion Create

        #region Edit

        /// <summary>
        /// Edit connection.
        /// </summary>
        /// <param name="id">Connection ID.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult EditConnection(int id)
        {
            // get connection by id
            var connection = _connectionService.GetById(id);

            if (ReferenceEquals(connection, null))
            {
                return RedirectToAction("NotFound", "CommonError");
            }

            // get database server
            ViewBag.DbServer = GetSelectListDatabaseServer(connection.Server.Id);

            // get list connection by alias
            var listConnection = _connectionService.GetByAlias(connection.Alias.Id);
            ViewBag.ListConnection = listConnection.Select(c => new SelectListItem
            {
                Value = c.ConnectionString.Id.ToString(),
                Text = c.ConnectionString.Connection,
                Selected = connection.PrimaryConnection != null ? c.ConnectionString.Id == connection.PrimaryConnection.Id : false
            }).ToList();

            // view model
            var model = new ConnectionUpdateCriteria
            {
                AliasId = connection.Alias.Id,
                ConnectionId = id,
                FreeText = connection.FreeText,
                InitialCatalog = connection.InitialCatalog,
                IntegratedSecurity = connection.IntegratedSecurity,
                IsDefault = connection.IsDefault,
                Password = connection.Password,
                SecondaryConnectionId = connection.PrimaryConnection != null ? connection.PrimaryConnection.Id : 0,
                ServerId = connection.Server.Id,
                UserID = connection.UserID
            };

            return View(model);
        }

        /// <summary>
        /// Edit connection.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult EditConnection(ConnectionUpdateCriteria criteria)
        {
            // get connection by id
            var connection = _connectionService.GetById(criteria.ConnectionId);
            if (ReferenceEquals(connection, null))
            {
                return RedirectToAction("NotFound", "CommonError");
            }

            ViewBag.DbServer = GetSelectListDatabaseServer(connection.Server.Id);

            var listConnection = _connectionService.GetByAlias(connection.Alias.Id);
            ViewBag.ListConnection = listConnection.Select(c => new SelectListItem
            {
                Value = c.ConnectionString.Id.ToString(),
                Text = c.ConnectionString.Connection,
                Selected = connection.PrimaryConnection != null ? c.ConnectionString.Id == connection.PrimaryConnection.Id : false
            }).ToList();

            criteria.Modifier = CurrentSession.UserId;
            criteria.ModifiedDate = DateTime.Now;

            // update connection string
            var command = _connectionService.InsertOrUpdate(criteria);
            if (!command.IsSuccessful)
            {
                return View(criteria);
            }

            ViewBag.Success = command.IsSuccessful;

            return View(criteria);
        }

        #endregion Edit

        #endregion Connection

        #region Site

        #region Mange

        /// <summary>
        /// Site list.
        /// </summary>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult Site()
        {
            // get all site with number of alias by site
            var listSite = _siteService.GetSiteWithNumOfAlias();

            // get all database server
            ViewBag.DatabaseServer = GetSelectListDatabaseServer();

            ViewBag.ListSite = listSite;

            return View();
        }

        /// <summary>
        /// Site list.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult Site(SiteGetAllCriteria criteria)
        {
            // get all site by name and database server
            var listSite = _siteService.GetSiteWithNumOfAlias(criteria.Name, criteria.DbServerId);

            // get all database server
            ViewBag.DatabaseServer = GetSelectListDatabaseServer();

            ViewBag.ListSite = listSite;

            return View(criteria);
        }

        #endregion Mange

        #region Create

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
        /// <param name="criteria">The criteria.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult CreateSite(SiteInsertCriteria criteria)
        {
            criteria.Creator = CurrentSession.UserId;
            criteria.CreatedDate = DateTime.Now;

            // insert new site
            var command = _siteService.InsertOrUpdate(criteria);
            if (!command.IsSuccessful)
            {
                return View(criteria);
            }

            ViewBag.Success = command.IsSuccessful;

            return View(criteria);
        }

        #endregion Create

        #region Edit

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

            if (ReferenceEquals(site, null))
            {
                return RedirectToAction("NotFound", "CommonError");
            }

            var model = new SiteUpdateCriteria
            {
                SiteId = site.Id,
                Name = site.Name,
                Description = site.Description,
                IsActive = site.IsActive
            };

            return View(model);
        }

        /// <summary>
        /// Update site.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult EditSite(SiteUpdateCriteria criteria)
        {
            criteria.Modifier = CurrentSession.UserId;
            criteria.ModifiedDate = DateTime.Now;

            // update site
            var command = _siteService.InsertOrUpdate(criteria);
            if (!command.IsSuccessful)
            {
                return View(criteria);
            }

            ViewBag.Success = command.IsSuccessful;

            return View(criteria);
        }

        #endregion Edit

        #region Connection

        /// <summary>
        /// View connection.
        /// </summary>
        /// <param name="siteId">Site object ID.</param>
        /// <returns>View result.</returns>
        public ViewResult ViewConnection(int siteId)
        {
            var list = _aliasService.GetAll(string.Empty, null, siteId);

            return View(list);
        }

        #endregion
        #endregion Site

        #region DB Server

        #region Manage

        /// <summary>
        /// Database server list.
        /// </summary>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult DatabaseServer()
        {
            // get all database server with number of site by database
            var listDbServer = _databaseServerService.GetWithNumOfSite();
            ViewBag.ListDbServer = listDbServer;

            return View();
        }

        /// <summary>
        /// Database server list.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult DatabaseServer(DatabaseServerGetAllCriteria criteria)
        {
            // get all by name
            var listDbServer = _databaseServerService.GetWithNumOfSite(criteria.Name);
            ViewBag.ListDbServer = listDbServer;

            return View();
        }

        #endregion Manage

        #region Create

        /// <summary>
        /// Create database server.
        /// </summary>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult CreateDbServer()
        {
            return View();
        }

        /// <summary>
        /// Create database server.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult CreateDbServer(DatabaseServerInsertCriteria criteria)
        {
            criteria.Creator = CurrentSession.UserId;
            criteria.CreatedDate = DateTime.Now;

            // insert new
            var command = _databaseServerService.InsertOrUpdate(criteria);

            if (!command.IsSuccessful)
            {
                return View(criteria);
            }

            ViewBag.Success = command.IsSuccessful;

            return View(criteria);
        }

        #endregion Create

        #region Edit

        /// <summary>
        /// Edit database server.
        /// </summary>
        /// <param name="id">DB server ID.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult EditDbServer(int id)
        {
            // get by id
            var databaseServer = _databaseServerService.GetById(id);

            if (ReferenceEquals(databaseServer, null))
            {
                return RedirectToAction("NotFound", "CommonError");
            }

            var model = new DatabaseServerUpdateCriteria
            {
                DbServerId = databaseServer.Id,
                Description = databaseServer.Description,
                IsActive = databaseServer.IsActive,
                Name = databaseServer.Name,
                ServerIp = databaseServer.ServerIP
            };

            return View(model);
        }

        /// <summary>
        /// Edit database server.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult EditDbServer(DatabaseServerUpdateCriteria criteria)
        {
            criteria.Modifier = CurrentSession.UserId;
            criteria.ModifiedDate = DateTime.Now;

            var command = _databaseServerService.InsertOrUpdate(criteria);

            if (!command.IsSuccessful)
            {
                return View(criteria);
            }

            ViewBag.Success = command.IsSuccessful;

            return View(criteria);
        }

        #endregion Edit

        #endregion DB Server

        #region Quick change connection from db server to other db server

        /// <summary>
        /// Change connection.
        /// </summary>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult ChangeConnection()
        {
            ViewBag.DatabaseServer = GetSelectListDatabaseServer();

            return View();
        }

        /// <summary>
        /// Change connection.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult ChangeConnection(DatabaseServerGetConnectionFromDbServerCriteria criteria)
        {
            ViewBag.DatabaseServer = GetSelectListDatabaseServer();

            var list = _aliasService.GetSwitchConnection(criteria.FromDbServerId, criteria.ToDbServerId);
            ViewBag.ListConnection = list;
            return View(criteria);
        }

        #endregion Quick change connection from db server to other db server

        #region Quick change second (backup) connection from db server

        /// <summary>
        /// Change second connection.
        /// </summary>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult ChangeSecondConnection()
        {
            var listDatabaseServer = _databaseServerService.GetAll();
            ViewBag.DatabaseServer = GetSelectListDatabaseServer();

            return View();
        }

        /// <summary>
        /// Change second connection.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult ChangeSecondConnection(DatabaseServerGetSecondConnectionFromDbServerCriteria criteria)
        {
            var listDatabaseServer = _databaseServerService.GetAll();
            ViewBag.DatabaseServer = GetSelectListDatabaseServer();

            var list = _aliasService.GetSwitchSecondConnection(criteria.DbServerId);
            ViewBag.ListConnection = list;
            return View(criteria);
        }

        #endregion Quick change second (backup) connection from db server

        #region Log management

        /// <summary>
        /// View log.
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

        #region Ajax

        /// <summary>
        /// Get site by alias.
        /// </summary>
        /// <param name="aliasId">Alias object ID.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult GetSiteByAlias(int aliasId)
        {
            var listSite = new List<Site>();

            if (Request.IsAjaxRequest())
            {
                listSite = _siteService.GetByAlias(aliasId).ToList();
            }

            return View(listSite);
        }

        /// <summary>
        /// Get connection by alias.
        /// </summary>
        /// <param name="aliasId">Alias object ID.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult GetConnectionByAlias(int aliasId)
        {
            if (Request.IsAjaxRequest())
            {
                var listConnection = _connectionService.GetByAlias(aliasId);
                ViewBag.AliasId = aliasId;

                return View(listConnection);
            }

            return View();
        }

        /// <summary>
        /// Update default connection.
        /// </summary>
        /// <param name="aliasId">Connection string alias ID.</param>
        /// <param name="conectionId">Connection ID.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult UpdateDefaultConnection(int aliasId, int conectionId)
        {
            if (Request.IsAjaxRequest())
            {
                var model = new ConnectionUpdateDefaultCriteria
                {
                    AliasId = aliasId,
                    ConnectionId = conectionId,
                    Modifier = CurrentSession.UserId,
                    ModifiedDate = DateTime.Now
                };

                var command = _connectionService.InsertOrUpdate(model);

                return Json(command.IsSuccessful.ToString().ToLower());
            }

            return Json(null);
        }

        /// <summary>
        /// Update alias status.
        /// </summary>
        /// <param name="aliasId">Connection string alias ID.</param>
        /// <param name="isActive">Is active or not.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult UpdateStatusAlias(int aliasId, bool isActive)
        {
            if (Request.IsAjaxRequest())
            {
                var model = new AliasUpdateStatusCriteria
                {
                    AliasId = aliasId,
                    IsActive = isActive,
                    ModifiedDate = DateTime.Now,
                    Modifier = CurrentSession.UserId
                };
                var command = _aliasService.InsertOrUpdate(model);

                return Json(command.IsSuccessful.ToString().ToLower());
            }

            return Json(null);
        }

        /// <summary>
        /// Get secondary connection.
        /// </summary>
        /// <param name="id">Connection ID.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult GetSecondaryConnection(int id)
        {
            var listConnection = new List<ConnectionString>();

            if (Request.IsAjaxRequest())
            {
                listConnection = _connectionService.GetSecondaryConnection(id).ToList();
            }

            return View(listConnection);
        }

        /// <summary>
        /// Get alias by site.
        /// </summary>
        /// <param name="id">Site object ID.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult GetAliasBySite(int id)
        {
            var listAlias = new List<Alias>();

            if (Request.IsAjaxRequest())
            {
                listAlias = _aliasService.GetBySite(id).ToList();
            }

            return View(listAlias);
        }

        /// <summary>
        /// Update site status.
        /// </summary>
        /// <param name="siteId">Site object ID.</param>
        /// <param name="isActive">Is active or not.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult UpdateStatusSite(int siteId, bool isActive)
        {
            if (Request.IsAjaxRequest())
            {
                var model = new SiteUpdateStatusCriteria
                {
                    IsActive = isActive,
                    ModifiedDate = DateTime.Now,
                    Modifier = CurrentSession.UserId,
                    SiteId = siteId
                };

                var command = _siteService.InsertOrUpdate(model);

                return Json(command.IsSuccessful.ToString().ToLower());
            }

            return Json(null);
        }

        /// <summary>
        /// Update database server status.
        /// </summary>
        /// <param name="databaseServerId">Database server ID.</param>
        /// <param name="isActive">Is active or not.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult UpdateStatusDbServer(int databaseServerId, bool isActive)
        {
            if (Request.IsAjaxRequest())
            {
                var model = new DatabaseServerUpdateStatusCriteria
                {
                    DbServerId = databaseServerId,
                    IsActive = isActive,
                    Modifier = CurrentSession.UserId,
                    ModifiedDate = DateTime.Now
                };

                var command = _databaseServerService.InsertOrUpdate(model);

                return Json(command.IsSuccessful.ToString().ToLower());
            }

            return Json(null);
        }

        /// <summary>
        /// Get site by DB server.
        /// </summary>
        /// <param name="id">Database server ID.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult GetSiteByDbServer(int id)
        {
            var listSite = new List<Site>();

            if (Request.IsAjaxRequest())
            {
                listSite = _siteService.GetByDbServer(id).ToList();
            }

            return View(listSite);
        }

        /// <summary>
        /// Change connection from DB server to another DB server.
        /// </summary>
        /// <param name="aliasConnection">Alias connection.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult ChangeConnectionFromDbServerToDbServer(string aliasConnection)
        {
            if (Request.IsAjaxRequest())
            {
                var criteria = new ConnectionChangeConnectionFromDbServerCriteria
                {
                    AliasConnection = aliasConnection,
                    ModifiedDate = DateTime.Now,
                    Modifier = CurrentSession.UserId
                };

                return Json(_connectionService.InsertOrUpdate(criteria));
            }

            return Json(false);
        }

        /// <summary>
        /// Log detail page.
        /// </summary>
        /// <param name="id">Log object ID.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult LogDetail(int id)
        {
            if (Request.IsAjaxRequest())
            {
                var log = _logService.GetById(id);

                return View(log);
            }

            return View(new LogExecSp());
        }

        #endregion Ajax

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

            if (log.ActionId == (int)ConLogAction.ConnectionString_ConnectionString_Insert)
            {
                listData = _logService.GetValueConnectionInsert(log);
                ViewBag.IsNew = true;

                return PartialView(listData);
            }

            if (log.ActionId == (int)ConLogAction.ConnectionString_ConnectionString_Update
                || log.ActionId == (int)ConLogAction.ConnectionString_ConnectionString_UpdateDefaultConnection)
            {
                listData = _logService.GetValueConnectionUpdate(log);
                ViewBag.IsNew = false;

                return PartialView(listData);
            }

            if (log.ActionId == (int)ConLogAction.ConnectionString_DatabaseServer_Insert)
            {
                listData = _logService.GetValueDatabaseServerInsert(log);
                ViewBag.IsNew = true;

                return PartialView(listData);
            }

            if (log.ActionId == (int)ConLogAction.ConnectionString_DatabaseServer_Update)
            {
                listData = _logService.GetValueDatabaseServerUpdate(log);
                ViewBag.IsNew = false;

                return PartialView(listData);
            }

            if (log.ActionId == (int)ConLogAction.ConnectionString_Site_Insert)
            {
                listData = _logService.GetValueSiteInsert(log);
                ViewBag.IsNew = true;

                return PartialView(listData);
            }

            if (log.ActionId == (int)ConLogAction.ConnectionString_Site_Update)
            {
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
            var listOld = new List<ConnectionString>();
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