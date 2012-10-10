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
    using Fanex.Common;
    using Fanex.Maintenance.Criteria.Job;
    using Fanex.Maintenance.Entity.Domain;


    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SqlJobManagementController : Controller
    {
        private readonly IJobService _jobService = JobService.Create();
        private readonly IScheduleService _scheduleService = ScheduleService.Create();
        private readonly IStepService _stepService = StepService.Create();
        private readonly ISqlServerLogService _sqlServerLogService = SqlServerLogService.Create();
        
        #region Pages
        public ActionResult MainContentPage()
        {
            return View("~/Views/SqlJobManagement/MainContentPage.cshtml");
        }

        public ActionResult LeftMenuPage()
        {
            return View("~/Views/SqlJobManagement/LeftMenuPage.cshtml", DatabaseServersSection.Instance.Servers);
        }

        #region ScheduleManagement
        [HttpGet]
        public ActionResult ScheduleManagementPage(string server)
        {
            ViewBag.ListServer = GetServer();
            ViewBag.ListSchedule = _scheduleService.WithServer(server)
                .GetSchedules();
            ViewBag.DateFrom = DateTime.Now.ToString("MM/dd/yyyy");
            ViewBag.DateTo = DateTime.Now.ToString("MM/dd/yyyy");

            return View("~/Views/SqlJobManagement/ScheduleManagement.cshtml");
        }

        [HttpPost]
        public ActionResult ScheduleManagementPage(string Schedule_Name,
            string Server,
            int? SearchDateType,
            string From,
            string To,
            int? FreqType,
            int? FreqSubType)
        {
            ViewBag.ListServer = GetServer();

            DateTime fromDate = string.IsNullOrEmpty(From) ? DateTime.Now : DateTime.ParseExact(From, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            DateTime toDate = string.IsNullOrEmpty(To) ? DateTime.Now : DateTime.ParseExact(To, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            ViewBag.DateFrom = fromDate.ToString("MM/dd/yyyy");
            ViewBag.DateTo = toDate.ToString("MM/dd/yyyy");

            ViewBag.ListSchedule = _scheduleService.WithScheduleName(Schedule_Name)
                .WithServer(Server)
                .WithSearchDateType(SearchDateType)
                .WithDateRange(fromDate, toDate)
                .WithFreqType(FreqType)
                .WithFreqSubType(FreqSubType)
                .GetSchedules();

            return View("~/Views/SqlJobManagement/ScheduleManagement.cshtml");
        }
        #endregion

        #region JobManagement

        [HttpGet]
        public ActionResult JobManagement(string server = "", string schedule = "")
        {
            ViewBag.ListServer = GetServer();

            List<Schedule> listSchedule = _scheduleService             
                .WithServer(server)
                .GetSchedules().ToList();
            ViewBag.ListSchedule = listSchedule.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Name
            }).ToList();

            ViewBag.ListJob = _jobService
                .FromServer(server)
                .GetJobs();

            var criteria = new GetListJobCriteria
            {
                From = DateTime.Now,
                To = DateTime.Now.AddDays(1),
                Server = server,
                Schedule = schedule
            };

            return View(criteria);
        }

        [HttpPost]
        public ActionResult JobManagement(GetListJobCriteria criteria)
        {
            List<JobSchedule> listJob = new List<JobSchedule>();

            listJob = _jobService.WithJob(criteria.Job_Name)
                .WithSchedule(criteria.Schedule)
                .WithStatus(criteria.Enabled)
                .WithCurrentExecuteStatus(criteria.Execution_Status)
                .FromServer(criteria.Server)
                .GetJobs().ToList();

            List<Schedule> listSchedule = _scheduleService
              .WithServer(criteria.Server)
              .GetSchedules().ToList();
            ViewBag.ListSchedule = listSchedule.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Name
            }).ToList();


            ViewBag.ListJob = listJob;
            ViewBag.ListServer = GetServer();

            return View();
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

            ViewBag.JobName = job;

            return View(listStep);
        }

        #endregion

        #region Job Log

        [HttpGet]
        public ActionResult JobLogManagement(string job, string server)
        {
            //// get log history 
            List<JobLog> listJobLog = _jobService
                               .WithJob(job)
                               .WithStartRunDate(DateTime.Now)
                               .WithEndRunDate(DateTime.Now.AddDays(1))
                               .FromServer(server)
                               .GetJobLogs().ToList();

            ViewBag.ListLogJob = listJobLog;

            //// get list job

            List<JobSchedule> listJob = _jobService
                                        .WithJob(null)
                                        .FromServer(server)
                                        .GetJobs().ToList();

            ViewBag.ListJob = listJob.Select(j => new SelectListItem
            {
                Text = j.Job.Name,
                Value = j.Job.Name
            }).ToList();

            var criteria = new GetListJobCriteria
            {
                Job_Name = job,
                From = DateTime.Now,
                To = DateTime.Now.AddDays(1),
                Server = server
            };

            return View(criteria);
        }

        [HttpPost]
        public ActionResult JobLogManagement(GetListJobCriteria criteria)
        {
            //// get log history 
            List<JobLog> listJobLog = _jobService
                               .WithJob(criteria.Job_Name)
                               .WithStartRunDate(criteria.From)
                               .WithEndRunDate(criteria.To)
                               .WithCurrentExecuteStatus(criteria.Execution_Status)
                               .FromServer(criteria.Server)
                               .GetJobLogs().ToList();

            ViewBag.ListLogJob = listJobLog;

            //// get list job

            List<JobSchedule> listJob = _jobService
                                        .WithJob(null)
                                        .WithCurrentExecuteStatus(null)
                                        .FromServer(criteria.Server)
                                        .GetJobs().ToList();

            ViewBag.ListJob = listJob.Select(j => new SelectListItem
            {
                Text = j.Job.Name,
                Value = j.Job.Name
            }).ToList();

            return View(criteria);
        }


        #endregion


        #region Sql Server Log Management
        [HttpGet]
        public ActionResult SqlServerLogManagement(string serverIp)
        {
            ViewBag.ListServer = GetServer();
            ViewBag.SqlServerLogs = _sqlServerLogService.WithServer(serverIp)
                .FromDate(DateTime.Now)
                .ToDate(DateTime.Now)
                .GetSqlServerLogs().Take(100);
            ViewBag.DateFrom = DateTime.Now.ToString("MM/dd/yyyy");
            ViewBag.DateTo = DateTime.Now.ToString("MM/dd/yyyy");

            return View();
        }

        [HttpPost]
        public ActionResult SqlServerLogManagement(GetSqlServerLogCriteria criteria)
        {
            ViewBag.ListServer = GetServer();
            ViewBag.DateFrom = DateTime.Now.ToString("MM/dd/yyyy");
            ViewBag.DateTo = DateTime.Now.ToString("MM/dd/yyyy");

            ViewBag.SqlServerLogs = _sqlServerLogService.WithServer(criteria.Server)
                .FromDate(criteria.From)
                .ToDate(criteria.To)
                .GetSqlServerLogs().Take(100);
           

            return View();
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
                    Text = item.ServerIp,
                    Value = item.ServerIp
                });
            }
            return list;
        }

        #endregion

        #region Ajax

        [HttpPost]
        public ActionResult GetSchedule(string server)
        {
            if (Request.IsAjaxRequest())
            {
                IEnumerable<Schedule> list = _scheduleService
                    .WithServer(server)
                    .GetSchedules();

                return Json(list);
            }

            return null;
        }

        #endregion
    }
}
