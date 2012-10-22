// --------------------------------------------------------------------------
// <copyright file="SqlJobManagementController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.8">Created</change>
//   <change who="Ricky.Nguyen" date="2012.10.16">StyleCop</change>
// </history>

namespace Fanex.Maintenance.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Fanex.Maintenance.Core.Configurations;
    using Fanex.Maintenance.Service.Impl.JobManagement;
    using Fanex.Maintenance.Service.Interface.JobManagement;
    using System.Globalization;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Criteria.Job;
    using Fanex.Maintenance.Entity.Domain;
    using Fanex.Maintenance.Core.Web;
    using Fanex.Maintenance.Core;

    /// <summary>zz
    /// TODO: Update summary.
    /// </summary>
    [Authorize]
    public class JobManagementController : Controller
    {
        /// <summary>
        /// Job Service
        /// </summary>
        private readonly IJobService _jobService = JobService.Create();

        /// <summary>
        /// Schedule Service
        /// </summary>
        private readonly IScheduleService _scheduleService = ScheduleService.Create();

        /// <summary>
        /// Step Service
        /// </summary>
        private readonly IStepService _stepService = StepService.Create();

        /// <summary>
        /// Server Log Service
        /// </summary>
        private readonly ISqlServerLogService _sqlServerLogService = SqlServerLogService.Create();

        #region Pages

        /// <summary>
        /// Main Content Page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(string type = "", string server = "", string jobname = "")
        {
            ViewBag.Type = type;
            ViewBag.Server = server;
            ViewBag.JobName = jobname;

            return View();
        }

        /// <summary>
        /// Left menu Page
        /// </summary>
        /// <returns></returns>
        public ActionResult LeftMenuPage()
        {
            return View(DatabaseServersSection.Instance.Servers);
        }

        #region ScheduleManagement

        /// <summary>
        /// Schedule Management Page
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ScheduleManagement(string server)
        {
            ViewBag.ListServer = GetServer();
            ViewBag.ListSchedule = _scheduleService.WithServer(server)
                .GetSchedules();
            ViewBag.DateFrom = DateTime.Now.ToString("MMM dd, yyyy");
            ViewBag.DateTo = DateTime.Now.ToString("MMM dd, yyyy");

            return View(new GetAllScheduleCriteria
                {
                    From = DateTime.Now,
                    To = DateTime.Now
                });
        }

        /// <summary>
        /// Get Schedule List With Filter
        /// </summary>
        /// <param name="getAllScheduleCriteria"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ScheduleManagement(GetAllScheduleCriteria getAllScheduleCriteria)
        {
            ViewBag.ListServer = GetServer();

            //DateTime fromDate = getAllScheduleCriteria.From == null ? DateTime.Now : getAllScheduleCriteria.From;
            //DateTime toDate = string.IsNullOrEmpty(To) ? DateTime.Now : DateTime.ParseExact(To, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            getAllScheduleCriteria.From = getAllScheduleCriteria.From <= DateTime.MinValue ? DateTime.Now : getAllScheduleCriteria.From;
            getAllScheduleCriteria.To = getAllScheduleCriteria.To <= DateTime.MinValue ? DateTime.Now : getAllScheduleCriteria.To;

            ViewBag.DateFrom = getAllScheduleCriteria.From.ToString("MMM dd, yyyy");
            ViewBag.DateTo = getAllScheduleCriteria.To.ToString("MMM dd, yyyy");

            ViewBag.ListSchedule = _scheduleService.WithScheduleName(getAllScheduleCriteria.Schedule_Name)
                .WithServer(getAllScheduleCriteria.Server)
                .WithSearchDateType(getAllScheduleCriteria.SearchDateType)
                .WithDateRange(getAllScheduleCriteria.From, getAllScheduleCriteria.To)
                .WithFreqType(getAllScheduleCriteria.FreqType)
                .WithFreqSubType(getAllScheduleCriteria.FreqSubType)
                .GetSchedules();

            return View(getAllScheduleCriteria);
        }
        #endregion

        #region JobManagement

        /// <summary>
        /// Job Management Page
        /// </summary>
        /// <param name="server"></param>
        /// <param name="schedule"></param>
        /// <returns></returns>
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
                .WithSchedule(schedule)
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

        /// <summary>
        /// Get List Job with filter
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
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

            return View(criteria);
        }

        /// <summary>
        /// Start a job
        /// </summary>
        /// <param name="server"></param>
        /// <param name="jobName"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult StartJob(string server,
            string jobName,
            string stepName)
        {
            CommandResult commandResult = _jobService.FromServer(server)
                .WithJob(jobName)
                .WithStepName(stepName)
                .StartJob();

            return Json(new
            {
                Status = commandResult
            });
        }

        /// <summary>
        /// Stop a job
        /// </summary>
        /// <param name="server"></param>
        /// <param name="jobName"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult StopJob(string server, string jobName)
        {
            CommandResult commandResult = _jobService.FromServer(server)
                .WithJob(jobName)
                .StopJob();
            return Json(new
            {
                Status = commandResult
            });
        }

        /// <summary>
        /// Get Job Information after start or stop
        /// </summary>
        /// <param name="server"></param>
        /// <param name="jobName"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetJob(string server, string jobName)
        {
            JobSchedule jobSchedule = _jobService.FromServer(server)
                .WithJob(jobName)
                .GetJobs()
                .FirstOrDefault();
            string executeStatus = string.Empty;
            string lastRunDate = string.Empty;
            string nextRunDate = string.Empty;
            if (jobSchedule != null)
            {
                executeStatus = jobSchedule.ExecuteStatus.ToExecutionString().ToHtmlString();
                lastRunDate = jobSchedule.LastRunDate == 0 ? ViewResource.N_A : jobSchedule.LastRunDate.ToFullDateString(jobSchedule.LastRunTime).ToHtmlString();
                nextRunDate = jobSchedule.NextRunDate == 0 ? ViewResource.N_A : jobSchedule.NextRunDate.ToFullDateString(jobSchedule.NextRunTime).ToHtmlString();
                jobSchedule.ServerIp = jobSchedule.ServerIp.Replace('.', '_');
            }

            return Json(new
            {
                JobUpdated = jobSchedule,
                ExecuteStatus = executeStatus,
                LastRunDate = lastRunDate,
                NextRunDate = nextRunDate
            });
        }

        #endregion

        #region StepManagement

        /// <summary>
        /// Get Step
        /// </summary>
        /// <param name="job"></param>
        /// <param name="server"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get Job Log
        /// </summary>
        /// <param name="job"></param>
        /// <param name="server"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult JobLogManagement(string job, string server, int top = 0)
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

            ViewBag.Top = top;

            return View(criteria);
        }

        /// <summary>
        /// Get Job log with Filter
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult JobLogManagement(GetListJobCriteria criteria)
        {                  
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

            ViewBag.Top = 0;

            //// check datetime
            if (ReferenceEquals(criteria.From, null) || ReferenceEquals(criteria.To, null))
            {
                criteria.From = DateTime.Now;
                criteria.To = DateTime.Now.AddDays(1);
                return View(criteria);
            }  

            //// get log history 
            List<JobLog> listJobLog = _jobService
                               .WithJob(criteria.Job_Name)
                               .WithStartRunDate(criteria.From)
                               .WithEndRunDate(((DateTime)criteria.To).AddDays(1))
                               .WithCurrentExecuteStatus(criteria.Execution_Status)
                               .FromServer(criteria.Server)
                               .GetJobLogs().ToList();

            ViewBag.ListLogJob = listJobLog;

            return View(criteria);
        }

        #endregion

        #region Sql Server Log Management
        /// <summary>
        /// Get Server Log
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ServerLogManagement(string server)
        {
            InitPageSize();
            ViewBag.ListServer = GetServer();
            ViewBag.SqlServerLogs = _sqlServerLogService.WithServer(server)
                .FromDate(DateTime.Now)
                .ToDate(DateTime.Now)
                .GetSqlServerLogs().Take(100);
            ViewBag.DateFrom = DateTime.Now.ToString("MMM dd, yyyy");
            ViewBag.DateTo = DateTime.Now.ToString("MMM dd, yyyy");

            return View(new GetSqlServerLogCriteria
                {
                    PageIndex = 0,
                    PageSize = 100,
                    Server = server
                });
        }

        /// <summary>
        /// Get Server Log With Filter
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ServerLogManagement(GetSqlServerLogCriteria criteria)
        {
            InitPageSize();
            ViewBag.ListServer = GetServer();
            ViewBag.DateFrom = criteria.From.ToString("MMM dd, yyyy");
            ViewBag.DateTo = criteria.To.ToString("MMM dd, yyyy");

            int pageSize = criteria.PageSize == null ? 0 : (int)criteria.PageSize;
            int pageIndex = criteria.PageIndex == null ? 0 : (int)criteria.PageIndex;

            ViewBag.SqlServerLogs = _sqlServerLogService.WithServer(criteria.Server)
                .FromDate(criteria.From)
                .ToDate(criteria.To)
                .GetSqlServerLogs().Skip(pageSize * pageIndex).Take(pageSize);

            return View(new GetSqlServerLogCriteria
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Server = criteria.Server
            });
        }
        #endregion

        #region Sql Agent Log Management
        /// <summary>
        /// Get Sql Server Agent Log
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AgentLogManagement(string server, int top = 0)
        {
            InitPageSize();
            ViewBag.ListServer = GetServer();
            ViewBag.SqlServerLogs = _sqlServerLogService.WithServer(server)
                .FromDate(DateTime.Now)
                .ToDate(DateTime.Now)
                .GetSqlServerLogs((int)LogType.SqlAgentLog).Take(100);
            ViewBag.DateFrom = DateTime.Now.ToString("MMM dd, yyyy");
            ViewBag.DateTo = DateTime.Now.ToString("MMM dd, yyyy");

            ViewBag.Top = top;

            return View(new GetSqlServerLogCriteria
            {
                PageIndex = 0,
                PageSize = 100,
                Server = server
            });
        }

        /// <summary>
        /// Get Sql Server Agent Log With Filter
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AgentLogManagement(GetSqlServerLogCriteria criteria)
        {
            InitPageSize();
            ViewBag.ListServer = GetServer();
            ViewBag.DateFrom = criteria.From.ToString("MMM dd, yyyy");
            ViewBag.DateTo = criteria.To.ToString("MMM dd, yyyy");

            int pageSize = criteria.PageSize == null ? 0 : (int)criteria.PageSize;
            int pageIndex = criteria.PageIndex == null ? 0 : (int)criteria.PageIndex;

            ViewBag.SqlServerLogs = _sqlServerLogService.WithServer(criteria.Server)
                .FromDate(criteria.From)
                .ToDate(criteria.To)
                .WithErrorLevel(criteria.ErrorLevel)
                .GetSqlServerLogs((int)LogType.SqlAgentLog).Skip(pageSize * pageIndex).Take(pageSize);

            ViewBag.Top = 0;

            return View(new GetSqlServerLogCriteria
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Server = criteria.Server
            });
        }
        #endregion

        #region Server Management

        /// <summary>
        /// Server Management Page
        /// </summary>
        /// <returns></returns>       
        public ActionResult ServerManagement()
        {
            return View();
        }

        #endregion

        #endregion

        #region Private

        /// <summary>
        /// Get List Server From Config File
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Init for Pagesize
        /// </summary>
        private void InitPageSize()
        {
            List<SelectListItem> listPageSize = new List<SelectListItem>();
            listPageSize.Add(new SelectListItem
            {
                Text = "10",
                Value = "10"
            });

            listPageSize.Add(new SelectListItem
            {
                Text = "30",
                Value = "30"
            });

            listPageSize.Add(new SelectListItem
            {
                Text = "50",
                Value = "50"
            });

            listPageSize.Add(new SelectListItem
            {
                Text = "100",
                Value = "100"
            });

            ViewBag.PageSize = listPageSize;
        }

        #endregion

        #region Ajax

        /// <summary>
        /// Get Schedule Ajax
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
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
