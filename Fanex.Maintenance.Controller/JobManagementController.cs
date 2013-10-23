// --------------------------------------------------------------------------
// <copyright file="JobManagementController.cs" company="Nexcel Solutions Vietnam">
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
    using Fanex.Maintenance.Core;
    using Fanex.Maintenance.Core.Configurations;
    using Fanex.Maintenance.Core.Web;
    using Fanex.Maintenance.Criteria.Job;
    using Fanex.Maintenance.Entity.Domain;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Service.Impl.JobManagement;
    using Fanex.Maintenance.Service.Interface.JobManagement;

    /// <summary>
    /// Job management controller.
    /// </summary>
    [Authorize]
    public class JobManagementController : BaseController
    {
        /// <summary>
        /// Job Service.
        /// </summary>
        private readonly IJobService _jobService = JobService.Create();

        /// <summary>
        /// Schedule Service.
        /// </summary>
        private readonly IScheduleService _scheduleService = ScheduleService.Create();

        /// <summary>
        /// Step Service.
        /// </summary>
        private readonly IStepService _stepService = StepService.Create();

        /// <summary>
        /// Server Log Service.
        /// </summary>
        private readonly ISqlServerLogService _sqlServerLogService = SqlServerLogService.Create();

        private readonly IServerService _serverService = ServerService.Create();
        #region Pages

        /// <summary>
        /// Main Content Page.
        /// </summary>
        /// <param name="type">Job type.</param>
        /// <param name="server">Server name.</param>
        /// <param name="jobname">Job name.</param>
        /// <param name="id">Job object ID.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult Index(string type = "", string server = "", string jobname = "", string id = "")
        {
            ViewBag.Type = type;
            ViewBag.Server = server;
            ViewBag.JobName = jobname;
            ViewBag.Go = id;

            return View();
        }

        /// <summary>
        /// Left menu Page.
        /// </summary>
        /// <returns>Action result.</returns>
        public ActionResult LeftMenuPage()
        {
            return View(DatabaseServersSection.Instance.Servers);
        }

        #region ScheduleManagement

        /// <summary>
        /// Schedule Management Page.
        /// </summary>
        /// <param name="serverIp">Server IP address.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult ScheduleManagement(string serverIp)
        {
            ViewBag.ListServer = GetServer();
            ViewBag.ListSchedule = _scheduleService.WithServer(serverIp)
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
        /// Get Schedule List With Filter.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult ScheduleManagement(GetAllScheduleCriteria criteria)
        {
            ViewBag.ListServer = GetServer();

            criteria.From = criteria.From <= DateTime.MinValue ? DateTime.Now : criteria.From;
            criteria.To = criteria.To <= DateTime.MinValue ? DateTime.Now : criteria.To;

            ViewBag.DateFrom = criteria.From.ToString("MMM dd, yyyy");
            ViewBag.DateTo = criteria.To.ToString("MMM dd, yyyy");

            ViewBag.ListSchedule = _scheduleService.WithScheduleName(criteria.ScheduleName)
                .WithServer(criteria.Server)
                .WithSearchDateType(criteria.SearchDateType)
                .WithDateRange(criteria.From, criteria.To)
                .WithFreqType(criteria.FreqType)
                .WithFreqSubType(criteria.FreqSubType)
                .GetSchedules();

            return View(criteria);
        }
        #endregion

        #region JobManagement

        /// <summary>
        /// Job Management Page.
        /// </summary>
        /// <param name="scheduleId">Schedule ID.</param>
        /// <param name="server">Server IP address.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult JobManagement(int? scheduleId, string server = "")
        {
            ViewBag.ListServer = GetServer();

            List<ScheduleDetail> listSchedule = _scheduleService
                .WithServer(server)
                .GetSchedules().ToList();

            ViewBag.ListSchedule = listSchedule.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            ViewBag.ListJob = _jobService
                .WithScheduleId(scheduleId)
                .FromServer(server)
                .GetJobs();

            var criteria = new GetListJobCriteria
            {
                From = DateTime.Now,
                To = DateTime.Now.AddDays(1),
                Server = server,
                ScheduleId = scheduleId
            };

            return View(criteria);
        }

        /// <summary>
        /// Get List Job with filter.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult JobManagement(GetListJobCriteria criteria)
        {
            List<JobSchedule> listJob = new List<JobSchedule>();

            listJob = _jobService.WithJob(criteria.JobName)
                .WithScheduleId(criteria.ScheduleId)
                .WithStatus(criteria.Enabled)
                .WithCurrentExecuteStatus(criteria.ExecutionStatus)
                .FromServer(criteria.Server)
                .GetJobs().ToList();

            List<ScheduleDetail> listSchedule = _scheduleService
              .WithServer(criteria.Server)
              .GetSchedules().ToList();
            ViewBag.ListSchedule = listSchedule.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            ViewBag.ListJob = listJob;
            ViewBag.ListServer = GetServer();

            return View(criteria);
        }

        /// <summary>
        /// Start a job.
        /// </summary>
        /// <param name="server">Server name.</param>
        /// <param name="jobName">Job name.</param>
        /// <param name="stepName">Step name.</param>
        /// <returns>Json result.</returns>
        [HttpPost]
        public JsonResult StartJob(string server, string jobName, string stepName)
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
        /// Stop a job.
        /// </summary>
        /// <param name="serverName">Server name.</param>
        /// <param name="jobName">Job name.</param>
        /// <returns>Json result.</returns>
        [HttpPost]
        public JsonResult StopJob(string serverName, string jobName)
        {
            CommandResult commandResult = _jobService.FromServer(serverName)
                .WithJob(jobName)
                .StopJob();
            return Json(new
            {
                Status = commandResult
            });
        }

        /// <summary>
        /// Get Job Information after start or stop.
        /// </summary>
        /// <param name="serverName">Server name.</param>
        /// <param name="jobName">Job name.</param>
        /// <returns>Json result.</returns>
        [HttpPost]
        public JsonResult GetJob(string serverName, string jobName)
        {
            JobSchedule jobSchedule = _jobService.FromServer(serverName)
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
        /// Get Step.
        /// </summary>
        /// <param name="jobName">Job name.</param>
        /// <param name="serverName">Server name.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult StepManagement(string jobName, string serverName)
        {
            List<StepDetail> listStep = _stepService
                                .WithJob(jobName)
                                .FromServer(serverName)
                                .GetSteps().ToList();

            ViewBag.JobName = jobName;

            return View(listStep);
        }

        #endregion

        #region Job Log

        /// <summary>
        /// Get Job Log.
        /// </summary>
        /// <param name="top">Number of items.</param>
        /// <param name="job">Job name.</param>
        /// <param name="server">Server name.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult JobLogManagement(int top = 0, string job = "", string server = "")
        {
            ViewBag.ListServer = GetServer();

            //// get log history 
            var listJobLog = new List<JobLog>();
            if (!job.Equals(string.Empty) && !server.Equals(string.Empty))
            {
                listJobLog = _jobService.WithJob(job)
                               .WithStartRunDate(DateTime.Now)
                               .WithEndRunDate(DateTime.Now.AddDays(1))
                               .FromServer(server)
                               .GetJobLogs().ToList();
            }

            ViewBag.ListLogJob = listJobLog;

            //// get list job
            var listJob = new List<JobSchedule>();
            if (!server.Equals(string.Empty))
            {
                listJob = _jobService.WithJob(null)
                                             .FromServer(server)
                                             .GetJobs().ToList();
            }

            ViewBag.ListJob = listJob.Select(j => new SelectListItem
            {
                Text = j.Job.Name,
                Value = j.Job.Name
            }).ToList();

            var criteria = new GetListJobCriteria
            {
                JobName = job,
                From = DateTime.Now,
                To = DateTime.Now.AddDays(1),
                Server = server
            };

            ViewBag.Top = top;

            return View(criteria);
        }

        /// <summary>
        /// Get Job log with Filter.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult JobLogManagement(GetListJobCriteria criteria)
        {
            ViewBag.ListServer = GetServer();

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

            //// check server and job
            if (ReferenceEquals(criteria.Server, null) || ReferenceEquals(criteria.JobName, null))
            {
                return View(criteria);
            }

            //// get log history 
            List<JobLog> listJobLog = _jobService
                               .WithJob(criteria.JobName)
                               .WithStartRunDate(criteria.From)
                               .WithEndRunDate(((DateTime)criteria.To).AddDays(1))
                               .WithCurrentExecuteStatus(criteria.ExecutionStatus)
                               .FromServer(criteria.Server)
                               .GetJobLogs().ToList();

            ViewBag.ListLogJob = listJobLog;

            return View(criteria);
        }

        #endregion

        #region Sql Server Log Management
        /// <summary>
        /// Get Server Log.
        /// </summary>
        /// <param name="serverIp">Server IP address.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult ServerLogManagement(string serverIp)
        {
            InitPageSize();
            ViewBag.ListServer = GetServer();
            ViewBag.SqlServerLogs = _sqlServerLogService.WithServer(serverIp)
                .FromDate(DateTime.Now.Date)
                .ToDate(DateTime.Now.AddDays(1))
                .GetSqlServerLogs().Take(100);
            ViewBag.DateFrom = DateTime.Now.ToString("MMM dd, yyyy");
            ViewBag.DateTo = DateTime.Now.ToString("MMM dd, yyyy");

            return View(new GetSqlServerLogCriteria
                {
                    PageIndex = 0,
                    PageSize = 100,
                    Server = serverIp
                });
        }

        /// <summary>
        /// Get Server Log With Filter.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Action result.</returns>
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
                .ToDate(criteria.To.AddDays(1))
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
        /// Get SQL Server Agent Log.
        /// </summary>
        /// <param name="serverIp">Server IP address.</param>
        /// <param name="top">Number of items.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult AgentLogManagement(string serverIp, int top = 0)
        {
            InitPageSize();

            ViewBag.ListServer = GetServer();
            ViewBag.SqlServerLogs = _sqlServerLogService.WithServer(serverIp)
                .FromDate(DateTime.Now.Date)
                .ToDate(DateTime.Now.AddDays(1))
                .GetSqlServerLogs((int)LogType.SqlAgentLog).Take(100);
            ViewBag.DateFrom = DateTime.Now.ToString("MMM dd, yyyy");
            ViewBag.DateTo = DateTime.Now.ToString("MMM dd, yyyy");
            ViewBag.Top = top;

            return View(new GetSqlServerLogCriteria
            {
                PageIndex = 0,
                PageSize = 100,
                Server = serverIp
            });
        }

        /// <summary>
        /// Get SQL Server Agent Log With Filter.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Action result.</returns>
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
                .ToDate(criteria.To.AddDays(1))
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
        /// Server Management Page.
        /// </summary>
        /// <returns>Action result.</returns>
        public ActionResult ServerManagement()
        {
            List<ResourceInfo> leftArray = new List<ResourceInfo>();
            List<ResourceInfo> rightArray = new List<ResourceInfo>();
            int leftLength = (DatabaseServersSection.Instance.ResourceServers.Count + 1) / 2;
            int index = 0;
            foreach (ServerElement server in DatabaseServersSection.Instance.ResourceServers)
            {
                ResourceInfo resourceInfo = new ResourceInfo
                {
                    ServerIp = server.ServerIp,
                    ServerName = server.ServerIp.Replace(".", "_")
                };

                if (index < leftLength)
                {
                    leftArray.Add(resourceInfo);
                }
                else
                {
                    rightArray.Add(resourceInfo);
                }

                index++;
            }

            ViewBag.LeftArray = leftArray;
            ViewBag.RightArray = rightArray;

            return View();
        }

        /// <summary>
        /// Server detail.
        /// </summary>
        /// <param name="serverIp">Server IP address.</param>
        /// <returns>Action result.</returns>
        public ActionResult ServerDetail(string serverIp)
        {
            ViewBag.ServerIp = serverIp;
            ViewBag.ListServer = GetServer();
            ViewBag.ServerFullInfo = _serverService.GetServerFullInformation(serverIp);
            return View();
        }

        /// <summary>
        /// Get server resource info.
        /// </summary>
        /// <param name="serverIp">Server IP address.</param>
        /// <returns>Json result.</returns>
        [HttpPost]
        public JsonResult GetServerResourceInfo(string serverIp)
        {
            foreach (ServerElement server in DatabaseServersSection.Instance.ResourceServers)
            {
                if (server.ServerIp == serverIp)
                {
                    CPUInfo cpuInfo = _serverService.GetCpuUtilization(server.ConnectionString);
                    cpuInfo.ServerIp = server.ServerIp;
                    cpuInfo.ServerName = server.ServerIp.Replace(".", "_");

                    MemoryInfo memoryInfo = _serverService.GetMemoryUtilization(server.ConnectionString);
                    memoryInfo.ServerIp = server.ServerIp;
                    memoryInfo.ServerName = server.ServerIp.Replace(".", "_");

                    return Json(new
                        {
                            CpuUtilization = cpuInfo,
                            MemoryUtilization = memoryInfo
                        });
                }
            }

            return Json(null);
        }

        #endregion

        #endregion

        #region Ajax

        /// <summary>
        /// Get Schedule Ajax.
        /// </summary>
        /// <param name="server">Server IP address.</param>
        /// <returns>Action result.</returns>
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

        /// <summary>
        /// Get Job Ajax.
        /// </summary>
        /// <param name="server">Server name.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult GetJobByServer(string server)
        {
            if (Request.IsAjaxRequest())
            {
                //// get list job
                var listJob = new List<JobSchedule>();
                if (!server.Equals(string.Empty))
                {
                    listJob = _jobService.WithJob(null)
                                                 .FromServer(server)
                                                 .GetJobs().ToList();
                }

                return Json(listJob);
            }

            return null;
        }

        #endregion

        #region Private

        /// <summary>
        /// Get List Server From Config File.
        /// </summary>
        /// <returns>List of selected list item.</returns>
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
        /// Initialize for Page size.
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
    }
}
