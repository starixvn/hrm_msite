// --------------------------------------------------------------------------
// <copyright file="LicenseeSettingController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Forrest.Nguyen" date="2013.10.23">Created</change>
// </history>

namespace Fanex.Maintenance.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Fanex.Maintenance.Core;
    using Fanex.Maintenance.Criteria;
    using Fanex.Maintenance.Criteria.LrfDomain;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Service.Impl;

    /// <summary>
    /// Licensee Setting Controller.
    /// </summary>
    [Authorize]
    public class LicenseeSettingController : Controller
    {
        #region Member

        /// <summary>
        /// Licensee Setting Service.
        /// </summary>
        private readonly LicenseeSettingService _licenseeSettingService;

        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of the LicenseeSettingController class.
        /// </summary>
        public LicenseeSettingController()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the LicenseeSettingController class.
        /// </summary>
        /// <param name="licenseeSettingService">Licensee Setting Service.</param>
        public LicenseeSettingController(LicenseeSettingService licenseeSettingService)
        {
            _licenseeSettingService = new LicenseeSettingService();
        }
        #endregion

        #region Action

        [HttpGet]
        public ActionResult LrfDomain()
        {
            var domains = _licenseeSettingService.GetAll();
            return View(domains);
        }

        [HttpPost]
        public ActionResult LrfDomain(int id)
        {
            _licenseeSettingService.ActiveDomain(id);
            var domains = _licenseeSettingService.GetAll();
            return View(domains);
        }

        #endregion

        #region Ajax

        #endregion

        #region Private

        #endregion
    }
}
