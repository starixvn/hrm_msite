// --------------------------------------------------------------------------
// <copyright file="SqlJobManagementController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.8">Created</change>
// </history>


namespace Fanex.Maintenance.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using Fanex.Maintenance.Core.Configurations;
    using Fanex.Maintenance.Service.Impl;
    using Fanex.Maintenance.Service.Interface;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SqlJobManagementController : Controller
    {
        private readonly IJobService _jobService = JobService.Create();

        #region Pages
        public ActionResult MainContentPage()
        {
            return View("~/Views/SqlJobManagement/MainContentPage.cshtml");
        }

        public ActionResult LeftMenuPage()
        {
            return View("~/Views/SqlJobManagement/LeftMenuPage.cshtml", DatabaseServersSection.Instance.Servers);
        }

        public ActionResult ScheduleManagementPage(string serverName = "")
        {
            ViewBag.ServerName = serverName;
            return View("~/Views/SqlJobManagement/ScheduleManagement.cshtml");
        }

        [HttpGet]
        public ActionResult JobManagementPage()
        {
            ViewBag.ListServer = GetServer();

            ViewBag.ListJob = _jobService.GetJobs();

            return View("~/Views/SqlJobManagement/JobManagement.cshtml");
        }

        [HttpPost]
        public ActionResult JobManagementPage(string job_name, int? enabled, int? execution_tatus, string server = "")
        {
            ViewBag.ListServer = GetServer();

            ViewBag.ListJob = _jobService.WithJob(job_name)
                .WithStatus(enabled)
                .WithCurrentExecuteStatus(execution_tatus)
                .FromServer(server)
                .GetJobs();

            return View("~/Views/SqlJobManagement/JobManagement.cshtml");
        }

        #endregion

        #region Private

        private List<SelectListItem> GetServer()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (ServerElement item in DatabaseServersSection.Instance.Servers)
            {
                list.Add(new SelectListItem
                {
                    Text = item.ServerName,
                    Value = item.ServerName
                });
            }
            return list;
        }

        #endregion
    }
}
