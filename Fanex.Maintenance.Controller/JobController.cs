// -----------------------------------------------------------------------
// <copyright file="JobController.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Maintenance.Controller
{
    using System.Web.Mvc;
    using Fanex.Maintenance.Service.Impl;
    using Fanex.Maintenance.Service.Interface;
    using System;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class JobController : Controller
    {
        private readonly IJobService _jobService = JobService.Create();

        public ActionResult Index()
        {
            _jobService.FromServer("10.19.10.71").GetJobs();
            return View();
        }
    }
}
