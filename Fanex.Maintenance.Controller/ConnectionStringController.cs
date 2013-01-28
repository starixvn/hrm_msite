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
    using Fanex.Maintenance.Criteria.ConnectionString.Site;
    using Fanex.Maintenance.Service.Impl.ConnectionString;
    using Fanex.Maintenance.Service.Interface.ConnectionString;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ConnectionStringController : BaseController
    {
        #region Member

        private readonly IDatabaseServerService _databaseServerService;

        private readonly IAliasService _aliasService;

        private readonly ISiteService _siteService;

        private readonly IConnectionService _connectionService;

        #endregion Member

        #region Ctor

        public ConnectionStringController()
            : this(null, null, null, null)
        {
        }

        public ConnectionStringController(
            IAliasService aliasService,
            IDatabaseServerService databaseServerService,
            ISiteService siteService,
            IConnectionService connectionService
            )
        {
            _aliasService = aliasService ?? new AliasService { ServiceContext = ServiceContext };
            _databaseServerService = databaseServerService ?? new DatabaseServiceService { ServiceContext = ServiceContext };
            _siteService = siteService ?? new SiteService { ServiceContext = ServiceContext };
            _connectionService = connectionService ?? new ConnectionService { ServiceContext = ServiceContext };
        }

        #endregion Ctor

        #region Manage Alias

        #region Mange

        [HttpGet]
        public ActionResult Alias()
        {
            var listDatabaseServer = _databaseServerService.GetAll();
            var listSite = _siteService.GetAll();

            ViewBag.DatabaseServer = listDatabaseServer.Select(d => new SelectListItem
            {
                Text = string.Format("{0} ({1})", d.ServerIP, d.Name),
                Value = d.Id.ToString()
            }).ToList();

            ViewBag.Site = listSite.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            var listAlias = _aliasService.GetAll();
            ViewBag.ListAlias = listAlias;

            return View();
        }

        [HttpPost]
        public ActionResult Alias(AliasGetAllCriteria model)
        {
            var listDatabaseServer = _databaseServerService.GetAll();
            var listSite = _siteService.GetAll();

            ViewBag.DatabaseServer = listDatabaseServer.Select(d => new SelectListItem
            {
                Text = string.Format("{0} ({1})", d.ServerIP, d.Name),
                Value = d.Id.ToString(),
                Selected = model.DbServerId == d.Id
            }).ToList();

            ViewBag.Site = listSite.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString(),
                Selected = model.SiteId == s.Id
            }).ToList();

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
                return RedirectToAction("NotFound", "CommondError");
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
            var listSite = _siteService.GetAll();
            ViewBag.Site = listSite.ToList();

            model.Modifier = CurrentSession.UserId;
            model.ModifiedDate = DateTime.Now;

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
            var alias = _aliasService.Get(id);
            ViewBag.Alias = alias;

            var listDbServer = _databaseServerService.GetAll();
            ViewBag.DbServer = listDbServer.Select(d => new SelectListItem
            {
                Text = string.Format("{0} ({1})", d.ServerIP, d.Name),
                Value = d.Id.ToString()
            }).ToList();

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
            ViewBag.Alias = alias;

            var listDbServer = _databaseServerService.GetAll();
            ViewBag.DbServer = listDbServer.Select(d => new SelectListItem
            {
                Text = string.Format("{0} ({1})", d.ServerIP, d.Name),
                Value = d.Id.ToString()
            }).ToList();

            var listConnection = _connectionService.GetByAlias(model.AliasId);
            ViewBag.ListConnection = listConnection.Select(c => new SelectListItem
            {
                Value = c.ConnectionString.Id.ToString(),
                Text = c.ConnectionString.Connection
            }).ToList();

            model.Creator = CurrentSession.UserId;
            model.CreatedDate = DateTime.Now;

            var commond = _connectionService.InsertOrUpdate(model);
            if (!commond.IsSuccessful)
            {
                return View(model);
            }
            ViewBag.Success = commond.IsSuccessful;

            return View(model);
        }

        #endregion Create

        #region Edit

        [HttpGet]
        public ActionResult EditConnection(int id)
        {
            var connection = _connectionService.GetById(id);
            if (ReferenceEquals(connection, null))
            {
                return RedirectToAction("NotFound", "CommondError");
            }

            var listDbServer = _databaseServerService.GetAll();
            ViewBag.DbServer = listDbServer.Select(d => new SelectListItem
            {
                Text = string.Format("{0} ({1})", d.ServerIP, d.Name),
                Value = d.Id.ToString(),
                Selected = d.Id == connection.Server.Id
            }).ToList();

            var listConnection = _connectionService.GetByAlias(connection.Alias.Id);
            ViewBag.ListConnection = listConnection.Select(c => new SelectListItem
            {
                Value = c.ConnectionString.Id.ToString(),
                Text = c.ConnectionString.Connection,
                Selected = connection.PrimaryConnection != null ? c.ConnectionString.Id == connection.PrimaryConnection.Id : false
            }).ToList();

            var model = new ConnectionUpdateCriteria
            {
                AliasId = connection.Alias.Id,
                ConnectionId = id,
                FreeText = connection.FreeText,
                InitialCatalog = connection.InitialCatalog,
                IntegratedSecurity = connection.IntegratedSecurity,
                IsDefault = connection.IsDefault,
                Password = connection.Password,
                PrimaryConnectionStringId = connection.PrimaryConnection != null ? connection.PrimaryConnection.Id : 0,
                ServerId = connection.Server.Id,
                UserID = connection.UserID
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult EditConnection(ConnectionUpdateCriteria model)
        {
            var connection = _connectionService.GetById(model.ConnectionId);

            var listDbServer = _databaseServerService.GetAll();
            ViewBag.DbServer = listDbServer.Select(d => new SelectListItem
            {
                Text = string.Format("{0} ({1})", d.ServerIP, d.Name),
                Value = d.Id.ToString(),
                Selected = d.Id == connection.Server.Id
            }).ToList();

            var listConnection = _connectionService.GetByAlias(connection.Alias.Id);
            ViewBag.ListConnection = listConnection.Select(c => new SelectListItem
            {
                Value = c.ConnectionString.Id.ToString(),
                Text = c.ConnectionString.Connection,
                Selected = connection.PrimaryConnection != null ? c.ConnectionString.Id == connection.PrimaryConnection.Id : false
            }).ToList();

            model.Modifier = CurrentSession.UserId;
            model.ModifiedDate = DateTime.Now;

            var commond = _connectionService.InsertOrUpdate(model);
            if (!commond.IsSuccessful)
            {
                return View(model);
            }
            ViewBag.Success = commond.IsSuccessful;

            return View(model);
        }

        #endregion Edit

        #endregion Connection

        #region Site

        #region Mange

        [HttpGet]
        public ActionResult Site()
        {
            var listSite = _siteService.GetSiteWithNumOfAlias();

            var listDatabaseServer = _databaseServerService.GetAll();

            ViewBag.DatabaseServer = listDatabaseServer.Select(d => new SelectListItem
            {
                Text = string.Format("{0} ({1})", d.ServerIP, d.Name),
                Value = d.Id.ToString()
            }).ToList();

            ViewBag.ListSite = listSite;

            return View();
        }

        [HttpPost]
        public ActionResult Site(SiteGetAllCriteria model)
        {
            var listSite = _siteService.GetSiteWithNumOfAlias(model.Name, model.DbServerId);

            var listDatabaseServer = _databaseServerService.GetAll();

            ViewBag.DatabaseServer = listDatabaseServer.Select(d => new SelectListItem
            {
                Text = string.Format("{0} ({1})", d.ServerIP, d.Name),
                Value = d.Id.ToString()
            }).ToList();

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

            var commond = _siteService.InsertOrUpdate(model);
            if (!commond.IsSuccessful)
            {
                return View(model);
            }
            ViewBag.Success = commond.IsSuccessful;

            return View(model);
        }

        #endregion Create

        #region Edit

        [HttpGet]
        public ActionResult EditSite(int id)
        {
            var site = _siteService.GetById(id);

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

            var commond = _siteService.InsertOrUpdate(model);
            if (!commond.IsSuccessful)
            {
                return View(model);
            }
            ViewBag.Success = commond.IsSuccessful;


            return View(model);
        }

        #endregion Edit

        #endregion Site

        #region DB Server

        #region Manage

        [HttpGet]
        public ActionResult DatabaseServer()
        {
            var listDbServer = _databaseServerService.GetWithNumOfSite();
            ViewBag.ListDbServer = listDbServer;

            return View();
        }

        [HttpPost]
        public ActionResult DatabaseServer(DatabaseServerGetAllCriteria model)
        {
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

            var commond = _databaseServerService.InsertOrUpdate(model);

            if (!commond.IsSuccessful)
            {
                return View(model);
            }

            ViewBag.Success = commond.IsSuccessful;

            return View(model);
        }

        #endregion Create

        #region Edit

        [HttpGet]
        public ActionResult EditDbServer(int id)
        {
            var dbServer = _databaseServerService.GetById(id);

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

            var commond = _databaseServerService.InsertOrUpdate(model);

            if (!commond.IsSuccessful)
            {
                return View(model);
            }

            ViewBag.Success = commond.IsSuccessful;

            return View(model);
        }

        #endregion Edit

        #endregion DB Server

        #region Quick change connection from db server to other db server

        [HttpGet]
        public ActionResult ChangeConnection()
        {
            var listDatabaseServer = _databaseServerService.GetAll();
            ViewBag.DatabaseServer = listDatabaseServer.Select(d => new SelectListItem
            {
                Text = string.Format("{0} ({1})", d.ServerIP, d.Name),
                Value = d.Id.ToString()
            }).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult ChangeConnection(DatabaseServerGetConnectionFromDbServerCriteria model)
        {
            var listDatabaseServer = _databaseServerService.GetAll();
            ViewBag.DatabaseServer = listDatabaseServer.Select(d => new SelectListItem
            {
                Text = string.Format("{0} ({1})", d.ServerIP, d.Name),
                Value = d.Id.ToString()
            }).ToList();

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
            ViewBag.DatabaseServer = listDatabaseServer.Select(d => new SelectListItem
            {
                Text = string.Format("{0} ({1})", d.ServerIP, d.Name),
                Value = d.Id.ToString()
            }).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult ChangeSecondConnection(DatabaseServerGetSecondConnectionFromDbServerCriteria model)
        {
            var listDatabaseServer = _databaseServerService.GetAll();
            ViewBag.DatabaseServer = listDatabaseServer.Select(d => new SelectListItem
            {
                Text = string.Format("{0} ({1})", d.ServerIP, d.Name),
                Value = d.Id.ToString()
            }).ToList();

            var list = _aliasService.GetSwitchSecondConnection(model.DbServerId);
            ViewBag.ListConnection = list;
            return View(model);
        }

        #endregion Quick change second (backup) connection from db server

        #region Log management

        [HttpGet]
        public ActionResult Log()
        {
            return View();
        }

        #endregion

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

                var check = _connectionService.UpdateDefaultConnection(model);

                return Json(check.ToString().ToLower());
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
                var commond = _aliasService.UpdateStatus(model);

                return Json(commond.IsSuccessful.ToString().ToLower());
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

                var command = _siteService.UpdateStatus(model);

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

                var commond = _databaseServerService.UpdateStatus(model);

                return Json(commond.IsSuccessful.ToString().ToLower());
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

        #endregion Ajax
    }
}