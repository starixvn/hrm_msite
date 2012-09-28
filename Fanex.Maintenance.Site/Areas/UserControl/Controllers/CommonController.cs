// --------------------------------------------------------------------------
// <copyright file="CommonController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.8.28">Created</change>
// </history>

namespace Fanex.NAP.Admin.Areas.UserControl.Controllers
{
    using System.Web.Mvc;

    public class CommonController : Controller
    {
        //
        // GET: /UserControl/Common/    

        public ActionResult Unauthorize()
        {
            return View();
        }

        public ActionResult Error(string aspxErrorPath)
        {
            if (!string.IsNullOrEmpty(aspxErrorPath))
            {
                return RedirectToAction("Error");
            }

            return View();
        }

        public ActionResult NotFound(string aspxErrorPath, string url)
        {
            if (!string.IsNullOrEmpty(aspxErrorPath))
            {
                ViewBag.RequestedUrl = aspxErrorPath;

                return RedirectToAction("NotFound", new { url = aspxErrorPath });
            }

            ViewBag.RequestedUrl = url;

            return View();
        }
    }
}
