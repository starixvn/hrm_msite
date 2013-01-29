// --------------------------------------------------------------------------
// <copyright file="Class1.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.4">Created</change>
// </history>

namespace Fanex.Maintenance.Controller
{
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Mvc;
    using Fanex.NAP.Core.Configuration;
    using Fanex.NAP.Core.Web;
    using Fanex.NAP.Core.Web.Context;
    using Fanex.NAP.Core.Web.Session;
    using Fanex.Maintenance.Service.Interface;
    using Fanex.Maintenance.Service.Impl;

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class BaseController : Controller
    {
        protected readonly string Environment = NapSettings.Settings.Environment;

        protected readonly int SiteId = NapSettings.Settings.SiteId;

        private int _issiteLogin = NapSettings.Settings.IsSiteLogin;

        protected readonly CurrentContext CurrentContext;

        protected readonly IServiceContext ServiceContext;

        protected readonly CurrentSession CurrentSession;

        private ViewFlags _viewFlags;

        protected ViewFlags ViewFlags
        {
            get
            {
                return ViewBag.ViewFlags;
            }

            set
            {
                ViewBag.ViewFlags = _viewFlags = value;
            }
        }

        public BaseController()
        {
            CurrentContext = new CurrentContext();
            CurrentSession = new CurrentSession();

            ServiceContext = new ServiceContext
            {
                UserId = this.CurrentSession.UserId,
                Ip = this.CurrentContext.IP
            };

            ViewFlags = new ViewFlags
            {
                IsReCheckin = NapSettings.Settings.ReCheckIn
            };
        }

        /// <summary>
        /// Indicates whether the current site allows users login directly.
        /// If "0", users must login through NAP Hub
        /// </summary>
        public int IsSiteLogin
        {
            get
            {
                return _issiteLogin;
            }
        }
    }
}
