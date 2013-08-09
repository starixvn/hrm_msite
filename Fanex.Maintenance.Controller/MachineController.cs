// --------------------------------------------------------------------------
// <copyright file="MachineController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.8.8">Created</change>
// </history>

namespace Fanex.Maintenance.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Web.Mvc;
    using Fanex.Maintenance.Core;
    using Fanex.Maintenance.Criteria.Log;
    using Fanex.Maintenance.Service.Impl;

    /// <summary>
    /// Machine Controller
    /// </summary>
    [Authorize]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class MachineController : BaseController
    {
        #region Member
        /// <summary>
        /// Machine Service
        /// </summary>
        private readonly MachineService _machineService;

        #endregion Member

        #region Constructor

        /// <summary>
        /// Parameter less Controller
        /// </summary>
        public MachineController()
            : this(null)
        {
        }

        /// <summary>
        /// Machine Controller with services defined
        /// </summary>
        /// <param name="machineService"></param>
        public MachineController(MachineService machineService)
        {
            _machineService = machineService ?? MachineService.Create();
        }

        #endregion Constructor

        #region Private

        /// <summary>
        /// Init Machine Types data
        /// </summary>
        private void InitMachineTypes()
        {
            List<SelectListItem> machineTypes = new List<SelectListItem>();
            machineTypes.Add(new SelectListItem
            {
                Text = ViewResource.All,
                Value = "null"
            });
            machineTypes.Add(new SelectListItem
            {
                Text = ViewResource.MachineType_Production,
                Value = "true"
            });
            machineTypes.Add(new SelectListItem
            {
                Text = ViewResource.MachineType_UAT,
                Value = "false"
            });

            ViewBag.MachineTypes = machineTypes;
        }

        #endregion Private

        #region Action

        /// <summary>
        /// Get machines
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.ListLog = null;

            GetLogCriteria criteria = new GetLogCriteria
            {
                CategoryId = 0,
                MachineId = 0,
                IsProduction = null,
                IsMonitored = null,
                From = DateTime.Now,
                To = DateTime.Now,
                Page = 0,
                Size = 10,
                Severity = "All"
            };

            return View(criteria);
        }
        
        #endregion Action

        #region Ajax

        /// <summary>
        /// Get machines
        /// </summary>
        /// <param name="machineType"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetMachinesByType(bool? machineType)
        {
            if (Request.IsAjaxRequest())
            {
                List<SelectListItem> machines = _machineService.GetByType(machineType).Select(m => new SelectListItem
                {
                    Text = m.MachineName,
                    Value = m.MachineId.ToString()
                }).ToList();
                machines.Insert(0, new SelectListItem
                {
                    Text = ViewResource.All,
                    Value = "0",
                    Selected = true
                });

                return Json(machines, JsonRequestBehavior.AllowGet);
            }

            return Json(0, JsonRequestBehavior.AllowGet);
        }

        #endregion Ajax
    }
}