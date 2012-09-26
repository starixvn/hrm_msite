namespace Fanex.Maintenance.Site.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Fanex.Maintenance.Core.Service.Impl;
    using Fanex.Maintenance.Core.Service.Interface;
    using System.Data;
    using Fanex.Maintenance.Core.Criteria.Licensee;
    using Fanex.Maintenance.Core.Model;

    public class DatabaseReportController : Controller
    {
        #region Member

        private readonly IDatabaseReportService _databaseReportService;

        #endregion

        #region Ctor

        public DatabaseReportController()
            : this(null)
        {
        }

        public DatabaseReportController(IDatabaseReportService databaseReportService)
        {
            _databaseReportService = databaseReportService ?? new DatabaseReportService();
        }

        #endregion

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
    }
}
