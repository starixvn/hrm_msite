// --------------------------------------------------------------------------
// <copyright file="SharedController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.8.28">Created</change>
// </history>

namespace Fanex.NAP.Admin.Areas.UserControl.Controllers
{
    using System.Web.Mvc;

    using Core.Web.Attribute;

    [NapSuperAuth]
    public class SharedController : BaseController
    {
        //
        // GET: /UserControl/Shared/

        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult _Menu()
        {
            ViewBag.MenuItemList = _currentSession.MenuItemList;

            return PartialView("~/Areas/UserControl/Views/Shared/_Menu.cshtml");
        }
    }
}
