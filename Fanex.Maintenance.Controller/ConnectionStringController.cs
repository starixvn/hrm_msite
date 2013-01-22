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

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ConnectionStringController : BaseController
    {
        #region Member

        private readonly IDatabaseServerService _databaseServerService;

        private readonly IAliasService _aliasService;

        private readonly ISiteService _siteService;

        #endregion

        #region Ctor

        public ConnectionStringController()
            : this(null, null, null)
        {
        }

        public ConnectionStringController(IAliasService aliasService, IDatabaseServerService databaseServerService, ISiteService siteService)
        {
            _aliasService = aliasService ?? new AliasService();
            _databaseServerService = databaseServerService ?? new DatabaseServiceService();
            _siteService = siteService ?? new SiteService();
        }

        #endregion

        #region Manage Alias

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

        [HttpGet]
        public ActionResult CreateAlias()
        {
            var listSite = _siteService.GetAll();

            ViewBag.Site = listSite.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult CreateAlias(Alias model)
        {
            var listSite = _siteService.GetAll();

            ViewBag.Site = listSite.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString(),
                Selected = model.Site.Id == s.Id
            }).ToList();



            return View(model);
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

        #endregion
    }
}
