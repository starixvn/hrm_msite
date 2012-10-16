// --------------------------------------------------------------------------
// <copyright file="BaseController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.16">Created</change>
// </history>

namespace Fanex.NAP.Hub.Areas.UserLogin.Controllers
{
    using System;
    using System.Configuration;
    using System.Web.Mvc;
    using Fanex.NAP.Core.Business;
    using Fanex.NAP.Core.Web.Context;

    /// <summary>
    /// 
    /// </summary>
    public class BaseController : Controller
    {
        protected readonly CurrentContext _currentContext;

        protected readonly string _environment = ConfigurationManager.AppSettings[Constant.Environment] ?? Constant.UAT;

        protected readonly int _siteId = Convert.ToInt32(ConfigurationManager.AppSettings[Constant.SiteId].ToString());

        public BaseController()
        {
            _currentContext = new CurrentContext();
        }

        public int IsSiteLogin
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["IsSiteLogin"].ToString() ?? "0");
            }
        }        
    }
}