// --------------------------------------------------------------------------
// <copyright file="BaseController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.8.28">Add properties _currentContext</change>
// </history>

namespace Fanex.NAP.Admin.Areas.UserControl.Controllers
{
    using System;
    using System.Configuration;
    using System.Web.Mvc;

    using Core.Business;
    using Core.Service.Impl;
    using Core.Service.Interface;
    using Core.Web;
    using Core.Web.Context;
    using Core.Web.Session;

    public class BaseController : Controller
    {
        protected const int NapAdminId = 1;

        protected readonly string _environment = ConfigurationManager.AppSettings[Constant.Environment] ?? Constant.UAT;

        protected readonly int _siteId = Convert.ToInt32(ConfigurationManager.AppSettings[Constant.SiteId].ToString());

        protected readonly ViewFlags _viewFlags;

        protected readonly ICurrentContext _currentContext;

        protected readonly ICurrentSession _currentSession;

        protected readonly IServiceContext _serviceContext;

        public BaseController()
        {
            _currentContext = new CurrentContext();
            ViewBag.Session = _currentSession = new CurrentSession();
            _serviceContext = new ServiceContext
            {
                UserId = this._currentSession.UserId,
                SiteId = this._siteId,
                Ip = this._currentContext.IP
            };

            ViewBag.ViewFlags = _viewFlags = new ViewFlags
            {
                IsLive = _environment.Equals(Constant.LIVE),
                IsNapAdminSite = _siteId == NapAdminId,
                HasEditRight = false
            };
        }
    }
}