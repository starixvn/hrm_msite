// --------------------------------------------------------------------------
// <copyright file="LogController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.20">Created</change>
//   <change who="Ricky.Nguyen" date="2012.10.16">Stylecop</change>
// </history>

namespace Fanex.Maintenance.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Web.Mvc;
    using Fanex.Maintenance.Criteria.Log;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Service.Impl;

    /// <summary>
    /// Log Controller
    /// </summary>
    [Authorize]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class LogController : BaseController
    {
        #region Member
        /// <summary>
        /// Log Service
        /// </summary>
        private readonly LogService _logService;

        /// <summary>
        /// Log Category Service
        /// </summary>
        private readonly LogCategoryService _categoryService;

        #endregion Member

        #region Ctor

        /// <summary>
        /// Parameter less Controller
        /// </summary>
        public LogController()
            : this(null, null)
        {
        }

        /// <summary>
        /// Log Controller with services defined
        /// </summary>
        /// <param name="logService"></param>
        /// <param name="categoryService"></param>
        public LogController(LogService logService, LogCategoryService categoryService)
        {
            _logService = logService ?? LogService.Create();
            _categoryService = categoryService ?? LogCategoryService.Create();
        }

        #endregion Ctor

        #region Private

        /// <summary>
        /// Init Page Size
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
            IEnumerable<LogCategory> listCategory = _categoryService.GetAll();
            ViewBag.ListCategory = listCategory.Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.CategoryID.ToString()
            }).ToList();
        }

        #endregion Private

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

            ViewBag.ListLog = null;

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
        public ActionResult Index(GetLogCriteria criteria)
        {
            InitDataView();

            InitPageSize();

            criteria.CategoryId = criteria.CategoryId ?? 0;

            var totalRows = 0;

            ////Get logs by date
            IEnumerable<Log> listLogs = _logService.With(criteria.CategoryId, criteria.Severity)
                .From(criteria.From)
                .To(criteria.To)
                .WithPagination(criteria.Page, criteria.Size)
                .GetLogs(ref totalRows);

            return Json(new
            {
                TotalRows = totalRows,
                ListLog = listLogs
            });
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

        #endregion Action

        #region Ajax

        /// <summary>
        /// Remove Log
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveLog()
        {
            if (Request.IsAjaxRequest())
            {
                return Json(_logService.From(DateTime.Now.AddDays(-5)).RemoveLogs());
            }

            return Json(0);
        }

        #endregion Ajax
    }
}