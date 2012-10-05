namespace Fanex.Maintenance.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.Web.Mvc;
    using Fanex.Maintenance.Criteria.Licensee;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Service.Impl;
    using Fanex.Maintenance.Service.Interface;

    public class DatabaseReportController : Controller
    {
        #region Member

        private readonly IDatabaseReportService _databaseReportService;

        #endregion Member

        #region Ctor

        public DatabaseReportController()
            : this(null)
        {
        }

        public DatabaseReportController(IDatabaseReportService databaseReportService)
        {
            _databaseReportService = databaseReportService ?? new DatabaseReportService();
        }

        #endregion Ctor

        [HttpGet]
        public ActionResult DataIntegrity()
        {
            var criteria = new GetBalanceUpDayCriteria();
            DataTable dtBalanceUpDay = _databaseReportService.GetBalanceUpDay(criteria);
            var moveBetsDay = (DateTime)dtBalanceUpDay.Rows[0][1];
            var sinceLastMonth = new DateTime(moveBetsDay.AddMonths(-1).Year, moveBetsDay.AddMonths(-1).Month, 1);
            ViewBag.MinDate = string.Format("{0:dd/MM/yyyy}", sinceLastMonth);
            ViewBag.MaxDate = string.Format("{0:dd/MM/yyyy}", moveBetsDay);

            ViewBag.BodyLicenseeData = _databaseReportService.GenerateTemplateHtmlLicensee(moveBetsDay, 1);
            ViewBag.BodyCreditData = _databaseReportService.GenerateTemplateHtmlCredit(moveBetsDay, 1);

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

        [HttpPost]
        public ActionResult DataIntegrity(DataIntegrityViewModel modelView)
        {
            var criteria = new GetBalanceUpDayCriteria();
            DataTable dtBalanceUpDay = _databaseReportService.GetBalanceUpDay(criteria);
            var moveBetsDay = (DateTime)dtBalanceUpDay.Rows[0][1];
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

        [HttpGet]
        public ActionResult UserOnlineDaily()
        {
            DataTable dtUserDaily = _databaseReportService.GetUserOnlineDaily(DateTime.Now);
            ViewBag.UserDailyData = _databaseReportService.GenerateTemplateUserDaily(dtUserDaily);
            ViewBag.DateNow = DateTime.Now.ToString("dd/MM/yyyy");
            return View();
        }

        [HttpPost]
        public ActionResult UserOnlineDaily(string date)
        {
            DateTime dateTime = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DataTable dtUserDaily = _databaseReportService.GetUserOnlineDaily(dateTime);
            ViewBag.UserDailyData = _databaseReportService.GenerateTemplateUserDaily(dtUserDaily);
            ViewBag.DateNow = DateTime.Now.ToString("dd/MM/yyyy");
            return View();
        }

        [HttpGet]
        public ActionResult UserOnline()
        {
            DataTable dtUserOnline = _databaseReportService.GetUserOnline(30);
            ViewBag.UserOnlineData = _databaseReportService.GenerateTemplateUser(dtUserOnline, 30);
            return View();
        }

        [HttpPost]
        public ActionResult UserOnline(int size)
        {
            if (Request.IsAjaxRequest())
            {
                DataTable dtUserOnline = _databaseReportService.GetUserOnline(size);
                return Json(_databaseReportService.GenerateTemplateUser(dtUserOnline, size));
            }
            return Json(null);
        }

        [HttpGet]
        public ActionResult UserOnlineChart()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserOnlineChart(int size)
        {
            if (Request.IsAjaxRequest())
            {
                DataTable dtUserOnline = _databaseReportService.GetUserOnline(size);
                return Json(_databaseReportService.GenerateDataChart(dtUserOnline, size));
            }
            return Json(null);
        }
    }
}