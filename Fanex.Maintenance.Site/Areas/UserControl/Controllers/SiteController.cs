// --------------------------------------------------------------------------
// <copyright file="SiteController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.8.28">Created</change>
// </history>

namespace Fanex.NAP.Admin.Areas.UserControl.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Fanex.Logging;    

    using Core.Business;
    using Core.Criteria.Site;
    using Core.Service.Impl;
    using Core.Service.Interface;
    using Core.Web;
    using Core.Web.Attribute;

    [NapSuperAuth]
    public class SiteController : BaseController
    {
        #region Members

        private readonly ISiteService _siteService;
        private readonly IUserService _userService;

        #endregion

        #region Constructors

        public SiteController() : this(null, null) { }

        public SiteController(ISiteService siteService = null, IUserService userService = null)
        {
            _siteService = siteService ?? new SiteService { ServiceContext = _serviceContext };
            _userService = userService ?? new UserService { ServiceContext = _serviceContext };
        }

        #endregion

        #region Manage

        [NapAuth(Function = Functions.Manage_Site_1, Policy = Constant.Policy.Read)]
        public ActionResult Manage(GetAllSiteCriteria criteria)
        {
            try
            {
                int policy = _userService.GetUserFunctionPolicy(_currentSession.UserId, (int)Functions.Manage_Site_1);

                ViewBag.ViewFlags.HasEditRight = policy >= (int)Constant.Policy.Host;
                ViewBag.SiteList = _siteService.GetAllWithNumOfUsers(this._environment).ToList();
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message, ex);
                throw ex;
            }

            return View(criteria);
        }

        #endregion

        #region Add New

        [HttpGet]
        [NapAuth(Function = Functions.Manage_Site_1, Policy = Constant.Policy.Host)]
        public ActionResult AddNew()
        {
            var criteria = new InsertSiteCriteria
            {
                Environment = this._environment
            };

            return View(criteria);
        }

        [HttpPost]
        [NapAuth(Function = Functions.Manage_Site_1, Policy = Constant.Policy.Host)]
        public ActionResult AddNew(InsertSiteCriteria criteria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    #region validation
                    
                    // checks SiteName duplication

                    string siteName = criteria.SiteName = criteria.SiteName.Trim();
                    var site = _siteService.GetByName(siteName);

                    if (site != null)
                    {
                        ModelState.AddModelError("SiteName", string.Format(ViewResources.DuplicateMsg, siteName));

                        return View(criteria);
                    }

                    #endregion

                    #region add new site

                    var result = _siteService.Insert(criteria);

                    if (result != null && result.IsSuccessful)
                        ViewBag.Success = true;

                    #endregion
                }
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message, ex);
                throw ex;
            }

            return View(criteria);
        }

        #endregion

        #region Edit

        [HttpGet]
        [NapAuth(Function = Functions.Manage_Site_1, Policy = Constant.Policy.Host)]
        public ActionResult Edit(int id)
        {
            var criteria = new UpdateSiteCriteria();
            
            try
            {
                var site = _siteService.GetById(id);

                if (site == null)
                {
                    return RedirectToAction("Manage");
                }

                criteria.SiteId = site.Id;
                criteria.SiteName = site.Name;
                criteria.Description = site.Description;
                criteria.Url = site.Url;
                criteria.TestUrl = site.TestUrl;
                criteria.IsActive = site.IsActive;
                criteria.Environment = site.Environment;
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message, ex);
                throw ex;
            }

            return View(criteria);
        }

        [HttpPost]
        [NapAuth(Function = Functions.Manage_Site_1, Policy = Constant.Policy.Host)]
        public ActionResult Edit(UpdateSiteCriteria criteria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    #region validation

                    // checks SiteName duplication

                    string siteName = criteria.SiteName = criteria.SiteName.Trim();
                    var site = _siteService.GetByName(siteName);

                    if (site != null && site.Id != criteria.SiteId)
                    {
                        ModelState.AddModelError("SiteName", string.Format(ViewResources.DuplicateMsg, siteName));

                        return View(criteria);
                    }

                    #endregion

                    #region update site

                    var result = _siteService.Update(criteria);

                    if (result != null && result.IsSuccessful)
                        ViewBag.Success = true;

                    #endregion
                }
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message, ex);
                throw ex;
            }

            return View(criteria);
        }

        #endregion

        #region Activation

        [NapAuth(Function = Functions.Manage_Site_1, Policy = Constant.Policy.Host)]
        public ActionResult Activate(int id, bool active)
        {
            if (Request.IsAjaxRequest())
            {
                try
                {
                    var result = _siteService.UpdateStatus(new UpdateStatusSiteCriteria
                    {
                        SiteId = id,
                        IsActive = active
                    });

                    if (result != null && result.IsSuccessful)
                        return Json(Constant.OK);
                }
                catch (Exception ex)
                {
                    LogService.Error(ex.Message, ex);
                    throw ex;
                }
            }

            return new EmptyResult();
        }

        #endregion
    }
}