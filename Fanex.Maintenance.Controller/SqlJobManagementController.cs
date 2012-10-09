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
    using Fanex.Maintenance.Service.Impl.JobManagement;
    using Fanex.Maintenance.Service.Interface.JobManagement;
    using System.Globalization;
    using Fanex.Maintenance.Core.Web;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Service.Interface.JobManagement;
    using Fanex.Maintenance.Service.Impl.JobManagement;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SqlJobManagementController : Controller
    {
        private readonly IJobService _jobService = JobService.Create();
        private readonly IScheduleService _scheduleService = ScheduleService.Create();

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
            ViewBag.ListServer = GetServer();
            ViewBag.ListSchedule = _scheduleService.GetSchedules();

            ViewBag.ServerName = serverName;
            return View("~/Views/SqlJobManagement/ScheduleManagement.cshtml");
        }

        #region JobManagement

        [HttpGet]
        public ActionResult JobManagementPage()
        {
            ViewBag.ListServer = GetServer();
            ViewBag.ListJob = _jobService.GetJobs();

            ViewBag.DateFrom = DateTime.Now.ToString("dd/MM/yyyy");
            ViewBag.DateTo = DateTime.Now.ToString("dd/MM/yyyy");            

            return View("~/Views/SqlJobManagement/JobManagement.cshtml");
        }

        [HttpPost]
        public ActionResult JobManagementPage(string job_name, int? enabled, int? execution_status, int? dateType, string server = "", string from = "", string to = "")
        {
            List<JobSchedule> listJob = new List<JobSchedule>();

            listJob = _jobService.WithJob(job_name)
                .WithStatus(enabled)
                .WithCurrentExecuteStatus(execution_status)
                .FromServer(server)
                .GetJobs().ToList();

            //// check datetype not all
            if (!ReferenceEquals(dateType, null))
            {
                DateTime dateFrom = DateTime.ParseExact(from, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dateTo = DateTime.ParseExact(to, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(1);

                //// filter by from date to date
                if (dateType.Equals(Constans.DateType.Last))
                {
                    listJob = listJob.Where(
                        j => j.LastRunDate >= dateFrom
                            && j.LastRunDate <= dateTo).ToList();
                }
                else
                {
                    listJob = listJob.Where(
                        j => j.NextRunDate >= dateFrom
                            && j.LastRunDate <= dateTo).ToList();
                }
            }

            ViewBag.ListJob = listJob;
            ViewBag.ListServer = GetServer();
            ViewBag.DateFrom = from;
            ViewBag.DateTo = to;

            return View("~/Views/SqlJobManagement/JobManagement.cshtml");
        }


        #endregion

        #region StepManagement

        [HttpGet]
        public ActionResult StepManagement(string job, string server)
        {
            List<StepDetail> listStep = _stepService
                                .WithJob(job)
                                .FromServer(server)
                                .GetSteps().ToList();
            return View(listStep);
        }

        #endregion
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
