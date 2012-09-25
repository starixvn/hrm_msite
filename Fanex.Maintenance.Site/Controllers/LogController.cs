// --------------------------------------------------------------------------
// <copyright file="LogController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.20">Created</change>
// </history>

namespace Fanex.Maintenance.Site.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Web.Mvc;
    using Fanex.Maintenance.Core.Criteria.Log;
    using Fanex.Maintenance.Core.Model;
    using Fanex.Maintenance.Core.Service.Impl;

    [Authorize]
    public class LogController : Controller
    {
        #region Member

        private readonly LogService _logService;
        private readonly CategoryService _categoryService;

        #endregion

        #region Ctor

        public LogController()
            : this(null, null)
        {
        }

        public LogController(LogService logService, CategoryService categoryService)
        {
            _logService = logService ?? LogService.Create();
            _categoryService = categoryService ?? CategoryService.Create();
        }

        #endregion

        #region Private

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

        /// <summary>
        /// Init data for View
        /// </summary>
        private void InitDataView()
        {
            ////Init Serverrity
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem
            {
                Text = "All",
                Value = "All"
            });
            list.Add(new SelectListItem
            {
                Text = "Critical",
                Value = "Critical"
            });
            list.Add(new SelectListItem
            {
                Text = "Error",
                Value = "Error"
            });
            list.Add(new SelectListItem
            {
                Text = "Verbose",
                Value = "Verbose"
            });
            list.Add(new SelectListItem
            {
                Text = "Warning",
                Value = "Warning"
            });
            ViewBag.ListSeverity = list;

            ////Get Category from DB
            IEnumerable<Categorys> listCategory = _categoryService.GetAll();
            ViewBag.ListCategory = listCategory.Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.CategoryID.ToString()
            }).ToList();
        }

        #endregion

        #region Action

        /// <summary>
        /// Get log
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            InitDataView();

            InitPageSize();

            ////Get logs by date
            IEnumerable<Logs> listLogs = _logService.With(0, "All")
                .From(DateTime.Now)
                .To(DateTime.Now.AddDays(1))
                .WithPagination(0, 10)
                .GetLogs();

            ViewBag.ListLog = listLogs;

            GetLogCriteria criteria = new GetLogCriteria
            {
                CategoryId = 0,
                From = DateTime.Now,
                To = DateTime.Now,
                Page = 0,
                Size = 10,
                Severity = "All"
            };

            return View(criteria);
        }

        /// <summary>
        /// Get log
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(GetLogCriteria criteria, string from, string to, int size = 0)
        {
            InitDataView();

            InitPageSize();

            criteria.From = DateTime.ParseExact(from, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            criteria.To = DateTime.ParseExact(to, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            ////Get logs by date
            IEnumerable<Logs> listLogs = _logService.With(criteria.CategoryId, criteria.Severity)
                .From(criteria.From)
                .To(criteria.To)
                .WithPagination(0, size)
                .GetLogs();

            ViewBag.ListLog = listLogs;

            return View(criteria);
        }

        /// <summary>
        /// Get details message
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetLogMessage(int id)
        {
            if (Request.IsAjaxRequest())
            {
                string message = string.Empty;
                message = _logService.GetMessage(id);
                return Json(message);
            }

            return Json(null);
        }

        #endregion

        #region Ajax

        [HttpPost]
        public ActionResult RemoveLog()
        {
            if (Request.IsAjaxRequest())
            {
                return Json(_logService.From(DateTime.Now.AddDays(-5)).RemoveLogs());
            }
            return Json(0);
        }

        #endregion
    }
}
