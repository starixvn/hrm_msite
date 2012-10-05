// --------------------------------------------------------------------------
// <copyright file="LogController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.20">Created</change>
// </history>

namespace Fanex.Maintenance.Controller
{
    using System.Web.Mvc;

    [Authorize]
    public class HomeController : Controller
    {        
        public ActionResult Index()
        {
            //home
            return View();
        }
    }
}
