// --------------------------------------------------------------------------
// <copyright file="DatabaseReportController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.16">StyleCop</change>
// </history>

namespace Fanex.Maintenance.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.Web.Mvc;
    using Fanex.Maintenance.Criteria.DatabaseReport;
    using Fanex.Maintenance.Service.Impl;
    using Fanex.Maintenance.Service.Interface;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Database report controller
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class DatabaseReportController : Controller
    {
        #region Member

        /// <summary>
        /// Database report service
        /// </summary>
        private readonly IDatabaseReportService _databaseReportService;

        #endregion Member

        #region Ctor

        /// <summary>
        /// Parameter less constructor
        /// </summary>
        public DatabaseReportController()
            : this(null)
        {
        }

        /// <summary>
        /// Constructor with service input
        /// </summary>
        /// <param name="databaseReportService"></param>
        public DatabaseReportController(IDatabaseReportService databaseReportService)
        {
            _databaseReportService = databaseReportService ?? new DatabaseReportService();
        }

        #endregion Ctor

        /// <summary>
        /// Data Integrity Return view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DataIntegrity()
        {
            var criteria = new GetBalanceUpDayCriteria();
            
            DataTable balanceUpDayTable = _databaseReportService.GetBalanceUpDay(criteria);
            var moveBetsDay = (DateTime)balanceUpDayTable.Rows[0][1];

            var sinceLastMonth = new DateTime(moveBetsDay.AddMonths(-1).Year, moveBetsDay.AddMonths(-1).Month, 1);
            ViewBag.MinDate = string.Format("{0:dd/MM/yyyy}", sinceLastMonth);
            ViewBag.MaxDate = string.Format("{0:dd/MM/yyyy}", moveBetsDay);

            //ViewBag.BodyLicenseeData = _databaseReportService.GenerateTemplateHtmlLicensee(moveBetsDay, 1);
            //ViewBag.BodyCreditData = _databaseReportService.GenerateTemplateHtmlCredit(moveBetsDay, 1);

            ViewBag.BodyLicenseeData = string.Empty;
            ViewBag.BodyCreditData = string.Empty;

            List<SelectListItem> listDrawFilter = new List<SelectListItem>();
            listDrawFilter.Add(new SelectListItem
            {
                Text = "Normal",
                Value = "1"
            });
            listDrawFilter.Add(new SelectListItem
            {
                Text = "No Draw",
                Value = "2"
            });

            listDrawFilter.Add(new SelectListItem
            {
                Text = "No Draw for Turnover Only",
                Value = "3"
            });

            ViewBag.ListDrawFilter = listDrawFilter;

            return View(new DataIntegrityViewModel
            {
                Date = moveBetsDay.ToString("dd/MM/yyyy")
            });
        }

        /// <summary>
        /// Data Integrity Post return
        /// </summary>
        /// <param name="modelView"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DataIntegrity(DataIntegrityViewModel modelView)
        {
            var criteria = new GetBalanceUpDayCriteria();
            DataTable balanceUpDayTable = _databaseReportService.GetBalanceUpDay(criteria);
            var moveBetsDay = (DateTime)balanceUpDayTable.Rows[0][1];
            var sinceLastMonth = new DateTime(moveBetsDay.AddMonths(-1).Year, moveBetsDay.AddMonths(-1).Month, 1);
            ViewBag.MinDate = string.Format("{0:dd/MM/yyyy}", sinceLastMonth);
            ViewBag.MaxDate = string.Format("{0:dd/MM/yyyy}", moveBetsDay);

            ViewBag.BodyLicenseeData = _databaseReportService.GenerateTemplateHtmlLicensee(modelView.GetDate(), modelView.DrawFilter);
            ViewBag.BodyCreditData = _databaseReportService.GenerateTemplateHtmlCredit(modelView.GetDate(), modelView.DrawFilter);

            List<SelectListItem> listDrawFilter = new List<SelectListItem>();
            listDrawFilter.Add(new SelectListItem
            {
                Text = "Normal",
                Value = "1"
            });
            listDrawFilter.Add(new SelectListItem
            {
                Text = "No Draw",
                Value = "2"
            });

            listDrawFilter.Add(new SelectListItem
            {
                Text = "No Draw for Turnover Only",
                Value = "3"
            });

            ViewBag.ListDrawFilter = listDrawFilter;

            return View(modelView);
        }

        /// <summary>
        /// User Online Daily
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UserOnlineDaily()
        {
            DataTable userDailyTable = _databaseReportService.GetUserOnlineDaily(DateTime.Now);
            ViewBag.UserDailyData = _databaseReportService.GenerateTemplateUserDaily(userDailyTable);
            ViewBag.DateNow = DateTime.Now.ToString("dd/MM/yyyy");
            return View();
        }

        /// <summary>
        /// User Online Daily by date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserOnlineDaily(string date)
        {
            DateTime dateTime = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DataTable userDailyTable = _databaseReportService.GetUserOnlineDaily(dateTime);
            ViewBag.UserDailyData = _databaseReportService.GenerateTemplateUserDaily(userDailyTable);
            ViewBag.DateNow = DateTime.Now.ToString("dd/MM/yyyy");
            return View();
        }

        /// <summary>
        /// User online
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UserOnline()
        {
            DataTable userOnlineTable = _databaseReportService.GetUserOnline(30);
            ViewBag.UserOnlineData = _databaseReportService.GenerateTemplateUser(userOnlineTable, 30);
            return View();
        }

        /// <summary>
        /// Get list of user online with size
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserOnline(int size)
        {
            if (Request.IsAjaxRequest())
            {
                DataTable userOnlineTable = _databaseReportService.GetUserOnline(size);
                return Json(_databaseReportService.GenerateTemplateUser(userOnlineTable, size));
            }

            return Json(null);
        }

        /// <summary>
        /// User online chart report
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UserOnlineChart()
        {
            return View();
        }

        /// <summary>
        /// User online chart report with size
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserOnlineChart(int size)
        {
            if (Request.IsAjaxRequest())
            {
                DataTable userOnlineTable = _databaseReportService.GetUserOnline(size);
                return Json(_databaseReportService.GenerateDataChart(userOnlineTable, size));
            }

            return Json(null);
        }
    }
}