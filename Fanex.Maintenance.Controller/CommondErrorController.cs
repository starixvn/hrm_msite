// --------------------------------------------------------------------------
// <copyright file="CommondErrorController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.25">Created</change>
// </history>

namespace Fanex.Maintenance.Controller
{
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Mvc;

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class CommondErrorController : Controller
    {
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
