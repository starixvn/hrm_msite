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
    using System.Diagnostics.CodeAnalysis;
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

    [Authorize]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
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

        #region Private

        /// <summary>
        /// Get select list item of all database server
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
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
        /// Get select list item of all site
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
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

        #region Manage Alias

        #region Mange

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

        [HttpPost]
        public ActionResult Alias(AliasGetAllCriteria model)
        {
            // get all database server with selected current database server
            ViewBag.DatabaseServer = GetSelectListDatabaseServer(model.DbServerId);

            // get all site with selected current site
            ViewBag.Site = GetSelectListSite(model.SiteId);

            // get alias filter by site and database server
            var listAlias = _aliasService.GetAll(model.Alias, model.DbServerId, model.SiteId);
            ViewBag.ListAlias = listAlias;

            return View(model);
        }

        #endregion Mange

        #region Create

        [HttpGet]
        public ActionResult CreateAlias()
        {
            // get list site
            var listSite = _siteService.GetAll();
            ViewBag.Site = listSite.ToList();

            return View();
        }

        [HttpPost]
        public ActionResult CreateAlias(AliasInsertCriteria model)
        {
            // get list site
            var listSite = _siteService.GetAll();
            ViewBag.Site = listSite.ToList();

            model.Creator = CurrentSession.UserId;
            model.CreatedDate = DateTime.Now;

            // insert new alias
            var command = _aliasService.InsertOrUpdate(model);

            if (!command.IsSuccessful)
            {
                return View(model);
            }

            ViewBag.Success = command.IsSuccessful;

            return View(model);
        }

        #endregion Create

        #region Edit

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

        [HttpPost]
        public ActionResult EditAlias(AliasUpdateCriteria model)
        {
            // get all site
            var listSite = _siteService.GetAll();
            ViewBag.Site = listSite.ToList();

            model.Modifier = CurrentSession.UserId;
            model.ModifiedDate = DateTime.Now;

            // update alias
            var command = _aliasService.InsertOrUpdate(model);

            if (!command.IsSuccessful)
            {
                return View(model);
            }

            ViewBag.Success = command.IsSuccessful;

            var alias = _aliasService.Get(model.AliasId);
            ViewBag.Alias = alias;

            var listSiteAlias = _siteService.GetByAlias(model.AliasId);
            ViewBag.SiteAlias = listSiteAlias;

            return View();
        }

        #endregion Edit

        #endregion Manage Alias

        #region Connection

        #region Manage

        [HttpGet]
        public ActionResult Connection(int id)
        {
            // get all connection by alias id
            var listConnection = _connectionService.GetByAlias(id);

            var alias = _aliasService.Get(id);
            ViewBag.Alias = alias;

            return View(listConnection);
        }

        #endregion Manage

        #region Create

        [HttpGet]
        public ActionResult CreateConnection(int id)
        {
            // get alias by id
            var alias = _aliasService.Get(id);
            if (ReferenceEquals(alias, null))
            {
                return RedirectToAction("NotFound", "CommonError");
            }

            ViewBag.Alias = alias;

            // get database server
            ViewBag.DbServer = GetSelectListDatabaseServer();

            // get connection for select secondary connection string
            var listConnection = _connectionService.GetByAlias(id);
            ViewBag.ListConnection = listConnection.Select(c => new SelectListItem
            {
                Value = c.ConnectionString.Id.ToString(),
                Text = c.ConnectionString.Connection
            }).ToList();

            return View(new ConnectionInsertCriteria
            {
                AliasId = id
            });
        }

        [HttpPost]
        public ActionResult CreateConnection(ConnectionInsertCriteria model)
        {
            var alias = _aliasService.Get(model.AliasId);
            if (ReferenceEquals(alias, null))
            {
                return RedirectToAction("NotFound", "CommonError");
            }

            ViewBag.Alias = alias;

            // get database server
            ViewBag.DbServer = GetSelectListDatabaseServer();

            // get list connection for select secondary connection string
            var listConnection = _connectionService.GetByAlias(model.AliasId);
            ViewBag.ListConnection = listConnection.Select(c => new SelectListItem
            {
                Value = c.ConnectionString.Id.ToString(),
                Text = c.ConnectionString.Connection
            }).ToList();

            model.Creator = CurrentSession.UserId;
            model.CreatedDate = DateTime.Now;

            // insert new alias
            var command = _connectionService.InsertOrUpdate(model);
            if (!command.IsSuccessful)
            {
                return View(model);
            }

            ViewBag.Success = command.IsSuccessful;

            return View(model);
        }

        #endregion Create

        #region Edit

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

        [HttpPost]
        public ActionResult EditConnection(ConnectionUpdateCriteria model)
        {
            // get connection by id
            var connection = _connectionService.GetById(model.ConnectionId);
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

            model.Modifier = CurrentSession.UserId;
            model.ModifiedDate = DateTime.Now;

            // update connection string
            var command = _connectionService.InsertOrUpdate(model);
            if (!command.IsSuccessful)
            {
                return View(model);
            }

            ViewBag.Success = command.IsSuccessful;

            return View(model);
        }

        #endregion Edit

        #endregion Connection

        #region Site

        #region Mange

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

        [HttpPost]
        public ActionResult Site(SiteGetAllCriteria model)
        {
            // get all site by name and database server
            var listSite = _siteService.GetSiteWithNumOfAlias(model.Name, model.DbServerId);

            // get all database server
            ViewBag.DatabaseServer = GetSelectListDatabaseServer();

            ViewBag.ListSite = listSite;

            return View(model);
        }

        #endregion Mange

        #region Create

        [HttpGet]
        public ActionResult CreateSite()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateSite(SiteInsertCriteria model)
        {
            model.Creator = CurrentSession.UserId;
            model.CreatedDate = DateTime.Now;

            // insert new site
            var command = _siteService.InsertOrUpdate(model);
            if (!command.IsSuccessful)
            {
                return View(model);
            }

            ViewBag.Success = command.IsSuccessful;

            return View(model);
        }

        #endregion Create

        #region Edit

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

        [HttpPost]
        public ActionResult EditSite(SiteUpdateCriteria model)
        {
            model.Modifier = CurrentSession.UserId;
            model.ModifiedDate = DateTime.Now;

            // update site
            var command = _siteService.InsertOrUpdate(model);
            if (!command.IsSuccessful)
            {
                return View(model);
            }

            ViewBag.Success = command.IsSuccessful;

            return View(model);
        }

        #endregion Edit

        #endregion Site

        #region DB Server

        #region Manage

        [HttpGet]
        public ActionResult DatabaseServer()
        {
            // get all database server with number of site by database
            var listDbServer = _databaseServerService.GetWithNumOfSite();
            ViewBag.ListDbServer = listDbServer;

            return View();
        }

        [HttpPost]
        public ActionResult DatabaseServer(DatabaseServerGetAllCriteria model)
        {
            // get all by name
            var listDbServer = _databaseServerService.GetWithNumOfSite(model.Name);
            ViewBag.ListDbServer = listDbServer;

            return View();
        }

        #endregion Manage

        #region Create

        [HttpGet]
        public ActionResult CreateDbServer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateDbServer(DatabaseServerInsertCriteria model)
        {
            model.Creator = CurrentSession.UserId;
            model.CreatedDate = DateTime.Now;

            // insert new
            var command = _databaseServerService.InsertOrUpdate(model);

            if (!command.IsSuccessful)
            {
                return View(model);
            }

            ViewBag.Success = command.IsSuccessful;

            return View(model);
        }

        #endregion Create

        #region Edit

        [HttpGet]
        public ActionResult EditDbServer(int id)
        {
            // get by id
            var dbServer = _databaseServerService.GetById(id);
            if (ReferenceEquals(dbServer, null))
            {
                return RedirectToAction("NotFound", "CommonError");
            }

            var model = new DatabaseServerUpdateCriteria
            {
                DbServerId = dbServer.Id,
                Description = dbServer.Description,
                IsActive = dbServer.IsActive,
                Name = dbServer.Name,
                ServerIp = dbServer.ServerIP
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult EditDbServer(DatabaseServerUpdateCriteria model)
        {
            model.Modifier = CurrentSession.UserId;
            model.ModifiedDate = DateTime.Now;

            var command = _databaseServerService.InsertOrUpdate(model);

            if (!command.IsSuccessful)
            {
                return View(model);
            }

            ViewBag.Success = command.IsSuccessful;

            return View(model);
        }

        #endregion Edit

        #endregion DB Server

        #region Quick change connection from db server to other db server

        [HttpGet]
        public ActionResult ChangeConnection()
        {
            ViewBag.DatabaseServer = GetSelectListDatabaseServer();

            return View();
        }

        [HttpPost]
        public ActionResult ChangeConnection(DatabaseServerGetConnectionFromDbServerCriteria model)
        {
            ViewBag.DatabaseServer = GetSelectListDatabaseServer();

            var list = _aliasService.GetSwitchConnection(model.FromDbServerId, model.ToDbServerId);
            ViewBag.ListConnection = list;
            return View(model);
        }

        #endregion Quick change connection from db server to other db server

        #region Quick change second (backup) connection from db server

        [HttpGet]
        public ActionResult ChangeSecondConnection()
        {
            var listDatabaseServer = _databaseServerService.GetAll();
            ViewBag.DatabaseServer = GetSelectListDatabaseServer();

            return View();
        }

        [HttpPost]
        public ActionResult ChangeSecondConnection(DatabaseServerGetSecondConnectionFromDbServerCriteria model)
        {
            var listDatabaseServer = _databaseServerService.GetAll();
            ViewBag.DatabaseServer = GetSelectListDatabaseServer();

            var list = _aliasService.GetSwitchSecondConnection(model.DbServerId);
            ViewBag.ListConnection = list;
            return View(model);
        }

        #endregion Quick change second (backup) connection from db server

        #region Log management

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

        [HttpPost]
        public ActionResult Log(LogExecSpSearchCriteria model)
        {
            return RedirectToAction(
             "Log",
             new
             {
                 id = model.PageIndex,
                 size = model.PageSize,
                 q = model.Name,
                 username = model.Username,
                 from = model.From.ToString("MMM dd,yyyy"),
                 to = model.To.ToString("MMM dd,yyyy"),
                 filterBy = model.FilterType
             });
        }

        #endregion Log management

        #region Ajax

        [HttpGet]
        public ActionResult GetSiteByAlias(int id)
        {
            var listSite = new List<Site>();

            if (Request.IsAjaxRequest())
            {
                listSite = _siteService.GetByAlias(id).ToList();
            }

            return View(listSite);
        }

        [HttpGet]
        public ActionResult GetConnectionByAlias(int id)
        {
            if (Request.IsAjaxRequest())
            {
                var listConnection = _connectionService.GetByAlias(id);
                ViewBag.AliasId = id;

                return View(listConnection);
            }

            return View();
        }

        [HttpPost]
        public ActionResult UpdateDefaultConenction(int aliasId, int conectionId)
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

        [HttpGet]
        public ActionResult GetSecondaryConection(int id)
        {
            var listConection = new List<ConnectionString>();
            if (Request.IsAjaxRequest())
            {
                listConection = _connectionService.GetSecondaryConection(id).ToList();
            }

            return View(listConection);
        }

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

        [HttpPost]
        public ActionResult UpdateStatusDbServer(int dbServerId, bool isActive)
        {
            if (Request.IsAjaxRequest())
            {
                var model = new DatabaseServerUpdateStatusCriteria
                {
                    DbServerId = dbServerId,
                    IsActive = isActive,
                    Modifier = CurrentSession.UserId,
                    ModifiedDate = DateTime.Now
                };

                var command = _databaseServerService.InsertOrUpdate(model);

                return Json(command.IsSuccessful.ToString().ToLower());
            }

            return Json(null);
        }

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

        public PartialViewResult _ConnectionLog_Alias_Insert(LogExecSp log)
        {
            var listSite = new List<LogData>();
            var listData = _logService.GetValueAliasInsert(log, ref listSite);

            ViewBag.ListSite = listSite;

            return PartialView(listData);
        }

        public PartialViewResult _ConnectionLog_Alias_Update(LogExecSp log)
        {
            var listSite = new List<LogData>();
            var listOldSite = new List<LogData>();

            var listData = _logService.GetValueAliasUpdate(log, ref listSite, ref listOldSite);

            ViewBag.ListSite = listSite;
            ViewBag.ListOldSite = listOldSite;

            return PartialView(listData);
        }

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

        public PartialViewResult _ConnectionLog_LogData_ChangeConnection(LogExecSp log)
        {
            var listOld = new List<ConnectionString>();
            var listConnection = _logService.GetValueChangeConnection(log, ref listOld);

            ViewBag.ListOld = listOld;

            return PartialView(listConnection);
        }

        #endregion Partial view
    }
}