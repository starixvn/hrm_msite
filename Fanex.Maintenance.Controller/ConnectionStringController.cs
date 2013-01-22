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
    using System.Linq;
    using System.Web.Mvc;
    using Fanex.Maintenance.Service.Impl.ConnectionString;
    using Fanex.Maintenance.Service.Interface.ConnectionString;
    using Fanex.Maintenance.Criteria.ConnectionString.Alias;
    using System.Collections.Generic;
    using Fanex.ConnectionString.Domain;
    using System;

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

        #endregion

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
            _aliasService = aliasService ?? new AliasService();
            _databaseServerService = databaseServerService ?? new DatabaseServiceService();
            _siteService = siteService ?? new SiteService();
            _connectionService = connectionService ?? new ConnectionService();
        }

        #endregion

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

        #endregion

        #region Create

        [HttpGet]
        public ActionResult CreateAlias()
        {
            var listSite = _siteService.GetAll();
            ViewBag.Site = listSite.ToList();

            return View();
        }

        [HttpPost]
        public ActionResult CreateAlias(AliasInsertCriteria model)
        {
            var listSite = _siteService.GetAll();
            ViewBag.Site = listSite.ToList();

            model.Creator = CurrentSession.UserId;
            model.CreatedDate = DateTime.Now;

            ViewBag.Success = _aliasService.InsertOrUpdate(model);

            return View(model);
        }

        #endregion

        #region Edit

        #region Create

        [HttpGet]
        public ActionResult EditAlias(int id)
        {
            var listSite = _siteService.GetAll();
            ViewBag.Site = listSite.ToList();

            var alias = _aliasService.Get(id);
            ViewBag.Alias = alias;

            var listSiteAlias = _siteService.GetByAlias(id);
            ViewBag.SiteAlias = listSiteAlias;

            return View();
        }

        [HttpPost]
        public ActionResult EditAlias(AliasInsertCriteria model)
        {
            var listSite = _siteService.GetAll();
            ViewBag.Site = listSite.ToList();
            return View();
        }

        #endregion

        #endregion

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
                var check = _connectionService.UpdateDefaultConnection(aliasId, conectionId);

                return Json(check.ToString().ToLower());
            }

            return Json(null);
        }

        [HttpPost]
        public ActionResult UpdateStatusAlias(int aliasId, bool isActive)
        {
            if (Request.IsAjaxRequest())
            {
                var check = _aliasService.UpdateStatus(aliasId, isActive);

                return Json(check.ToString().ToLower());
            }

            return Json(null);
        }
        #endregion
    }
}
