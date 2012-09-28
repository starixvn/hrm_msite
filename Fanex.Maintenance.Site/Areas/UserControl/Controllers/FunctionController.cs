// --------------------------------------------------------------------------
// <copyright file="BaseController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.8.28">Add properties _currentContext</change>
// </history>

namespace Fanex.NAP.Admin.Areas.UserControl.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Core.Business;
    using Core.Criteria.Function;
    using Core.Model;
    using Core.Service.Impl;
    using Core.Service.Interface;
    using Core.Web;
    using Core.Web.Attribute;
    using Fanex.Logging;

    [NapSuperAuth]
    public class FunctionController : BaseController
    {
        #region Members

        private readonly IFunctionService _functionService;
        private readonly ISiteService _siteService;
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;

        #endregion

        #region Constructors

        public FunctionController()
            : this(null, null, null, null) { }

        public FunctionController(IFunctionService functionService, ISiteService siteService,
            IUserService userService, IGroupService groupService)
        {
            _functionService = functionService ?? new FunctionService { ServiceContext = _serviceContext };
            _siteService = siteService ?? new SiteService { ServiceContext = _serviceContext };
            _userService = userService ?? new UserService { ServiceContext = _serviceContext };
            _groupService = groupService ?? new GroupService { ServiceContext = _serviceContext };
        }

        #endregion

        #region Manage

        [HttpGet]
        [NapAuth(Function = Functions.Manage_Function_1, Policy = Constant.Policy.Read)]
        public ActionResult Manage()
        {
            var criteria = new GetBySiteFunctionCriteria
            {
                SiteId = this._siteId,
                Environment = this._environment
            };

            try
            {
                PrepareManageData(criteria);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message, ex);
                throw ex;
            }

            return View(criteria);
        }

        [HttpPost]
        [NapAuth(Function = Functions.Manage_Function_1, Policy = Constant.Policy.Read)]
        public ActionResult Manage(GetBySiteFunctionCriteria criteria)
        {
            try
            {
                PrepareManageData(criteria);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message, ex);
                throw ex;
            }

            return View(criteria);
        }

        private void PrepareManageData(GetBySiteFunctionCriteria criteria)
        {
            ViewBag.FunctionList = _functionService.GetBySiteInHierarchy(criteria.SiteId, criteria.Environment);
            if (_currentSession.Level == (int)Constant.Level.PortalAdmin)
            {
                ViewBag.SiteItemList = _siteService.GetAllSelectListItem(criteria.Environment, true, criteria.SiteId);
                ViewBag.ViewFlags.HasEditRight = true;
            }
            else
            {
                ViewBag.SiteItemList =
                    _siteService.GetSelectListItemByUserAndFunctionKeyIncludeGroup(_currentSession.UserId,
                    FunctionKeys.Manage_Function.ToString(), criteria.Environment, policy: Constant.Policy.Read,
                    selectedSiteId: criteria.SiteId);

                int policy = _userService.GetUserSiteFunctionKeyPolicy(_currentSession.UserId,
                criteria.SiteId, FunctionKeys.Manage_Function.ToString());

                ViewBag.ViewFlags.HasEditRight = policy >= (int)Constant.Policy.Host;
            }
        }

        #endregion

        #region Add new

        [HttpGet]
        [NapAuth(Function = Functions.Manage_Function_1, Policy = Constant.Policy.Host)]
        public ActionResult AddNew()
        {
            var criteria = new InsertFunctionCriteria
            {
                SiteId = this._siteId,
                Environment = this._environment
            };

            try
            {
                if (_currentSession.Level == (int)Constant.Level.PortalAdmin)
                {
                    ViewBag.SiteItemList = _siteService.GetAllSelectListItem(criteria.Environment, true, criteria.SiteId);
                }
                else
                {
                    ViewBag.SiteItemList =
                        _siteService.GetSelectListItemByUserAndFunctionKeyIncludeGroup(_currentSession.UserId,
                        FunctionKeys.Manage_Function.ToString(), criteria.Environment, policy: Constant.Policy.Host,
                        selectedSiteId: criteria.SiteId);
                }
                ViewBag.FunctionList = _functionService.GetSelectListItemBySite(criteria.SiteId, 
                    criteria.Environment, true);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message, ex);
                throw ex;
            }

            return View(criteria);
        }

        [HttpPost]
        [NapAuth(Function = Functions.Manage_Function_1, Policy = Constant.Policy.Host)]
        public ActionResult AddNew(InsertFunctionCriteria criteria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    #region checks duplicate function name

                    string functionName = criteria.FunctionName = criteria.FunctionName.Trim();
                    var function = _functionService.GetByNameAndSite(functionName, criteria.SiteId);

                    if (function != null)
                    {
                        ModelState.AddModelError("FunctionName",
                            string.Format(ViewResources.DuplicateMsg, functionName));

                        return View(criteria);
                    }

                    #endregion

                    #region checks duplicate function key

                    string functionKey = criteria.FunctionKey = criteria.FunctionName.Replace(" ", string.Empty);
                    function = _functionService.GetByKeyAndSite(functionKey, criteria.SiteId);

                    if (function != null)
                    {
                        ModelState.AddModelError("FunctionKey",
                            string.Format(ViewResources.DuplicateMsg, functionKey));

                        return View(criteria);
                    }

                    #endregion

                    #region add site

                    criteria.ParentId = criteria.ParentId == 0 ? null : criteria.ParentId;
                    criteria.ParentName = criteria.ParentId == null || criteria.ParentId == 0 ? null : 
                        criteria.ParentName;
                    criteria.CreatedBy = _currentSession.UserId;
                    criteria.CreatedDate = DateTime.Now;

                    var result = _functionService.Insert(criteria);

                    if (result != null && result.IsSuccessful)
                    {
                        ViewBag.Success = true;
                    }

                    #endregion
                }

                // get site list

                if (_currentSession.Level == (int)Constant.Level.PortalAdmin)
                {
                    ViewBag.SiteItemList = _siteService.GetAllSelectListItem(criteria.Environment, true, criteria.SiteId);
                }
                else
                {
                    ViewBag.SiteItemList =
                        _siteService.GetSelectListItemByUserAndFunctionKeyIncludeGroup(_currentSession.UserId,
                        FunctionKeys.Manage_Function.ToString(), criteria.Environment, policy: Constant.Policy.Host,
                        selectedSiteId: criteria.SiteId);
                }

                // get function list

                if (criteria.ParentId == null)
                {
                    ViewBag.FunctionList = _functionService.GetSelectListItemBySite(criteria.SiteId,
                        criteria.Environment, true);
                }
                else
                {
                    ViewBag.FunctionList = _functionService.GetSelectListItemBySite(criteria.SiteId,
                        criteria.Environment, isActive: true, selectedFunctionId: (int)criteria.ParentId);
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

        #region Update

        [HttpGet]
        [NapAuth(Function = Functions.Manage_Function_1, Policy = Constant.Policy.Host)]
        public ActionResult Edit(int id)
        {
            var criteria = new UpdateFunctionCriteria();
            
            try
            {
                var function = _functionService.GetById(id);

                if (ReferenceEquals(function, null))
                {
                    return RedirectToAction("Manage");
                }

                criteria.FunctionId = function.Id;
                criteria.SiteId = function.Site.Id;
                criteria.SiteName = function.Site.Name;
                criteria.FunctionName = function.Name;
                criteria.FunctionKey = function.Key;
                criteria.Description = function.Description;
                criteria.ParentId = function.Parent == null ? null : (int?)function.Parent.Id;
                criteria.ParentName = function.Parent == null ? null : function.Parent.Name;
                criteria.Crud = function.Crud;
                criteria.DisplayOrder = function.DisplayOrder;
                criteria.IsActive = function.IsActive;
                criteria.Environment = function.Environment;

                // get parent function list
                var functionList = _functionService.GetBySite(criteria.SiteId, criteria.Environment, true);

                ViewBag.ParentFunctionItemList = functionList
                    .Where(f => f.Id != id && (f.Parent == null || f.Parent.Id != id))
                    .Select(s => new SelectListItem
                    {
                        Text = s.Name,
                        Value = s.Id.ToString(),
                        Selected = function.Parent != null && s.Id == function.Parent.Id
                    }).ToList();
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message, ex);
                throw ex;
            }

            return View(criteria);
        }

        [HttpPost]
        [NapAuth(Function = Functions.Manage_Function_1, Policy = Constant.Policy.Host)]
        public ActionResult Edit(UpdateFunctionCriteria criteria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    #region checks Function Name duplication

                    string functionName = criteria.FunctionName = criteria.FunctionName.Trim();
                    var function = _functionService.GetByNameAndSite(functionName, criteria.SiteId);

                    if (function != null && function.Id != criteria.FunctionId)
                    {
                        ModelState.AddModelError("FunctionName", string.Format(ViewResources.DuplicateMsg, functionName));

                        return View(criteria);
                    }

                    #endregion

                    #region checks Function Key duplication

                    string functionKey = criteria.FunctionKey = criteria.FunctionName.Replace(" ", string.Empty);
                    function = _functionService.GetByKeyAndSite(functionKey, criteria.SiteId);

                    if (function != null && function.Id != criteria.FunctionId)
                    {
                        ModelState.AddModelError("FunctionKey", string.Format(ViewResources.DuplicateMsg, functionKey));

                        return View(criteria);
                    }

                    #endregion

                    #region update function

                    criteria.ParentId = criteria.ParentId == 0 ? null : criteria.ParentId;
                    criteria.ParentName = criteria.ParentId == null || criteria.ParentId == 0 ? null :
                        criteria.ParentName;
                    criteria.ModifiedBy = _currentSession.UserId;
                    criteria.ModifiedDate = DateTime.Now;

                    var result = _functionService.Update(criteria);

                    if (result != null && result.IsSuccessful)
                    {
                        ViewBag.Success = true;
                    }

                    #endregion
                }

                var functionList = _functionService.GetBySite(criteria.SiteId, criteria.Environment, true);

                ViewBag.ParentFunctionItemList = functionList.Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString(),
                    Selected = criteria.ParentId != null && s.Id == criteria.ParentId
                }).ToList();
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

        [NapAuth(Function = Functions.Manage_Function_1, Policy = Constant.Policy.Host)]
        public ActionResult Activate(int id, bool active)
        {
            if (Request.IsAjaxRequest())
            {
                try
                {
                    var result = _functionService.UpdateStatus(new UpdateStatusFunctionCriteria
                    {
                        FunctionId = id,
                        IsActive = active,
                        ModifiedBy = _currentSession.UserId,
                        ModifiedDate = DateTime.Now
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

        #region Detail Permission

        [NapAuth(Function = Functions.Manage_Function_1, Policy = Constant.Policy.Read)]
        public ActionResult GetAssigneeList(int functionId, int siteId)
        {
            if (Request.IsAjaxRequest())
            {
                try
                {
                    ViewBag.Users = _userService.GetByFunctionAndSite(functionId, siteId).ToList();
                    ViewBag.Groups = _groupService.GetByFunctionAndSite(functionId, siteId).ToList();
                }
                catch (Exception ex)
                {
                    LogService.Error(ex.Message, ex);
                    throw ex;
                }

                return View();
            }

            return new EmptyResult();
        }

        #endregion

        #region AjaxRequest

        [HttpPost]
        [NapAuth(Function = Functions.Manage_Function_1, Policy = Constant.Policy.Read)]
        public JsonResult GetFunctionBySite(int siteId)
        {
            if (Request.IsAjaxRequest())
            {
                try
                {
                    var functionList = (List<Function>)_functionService.GetBySite(siteId, this._environment, true);

                    functionList.Insert(0, new Function { Id = 0, Name = ViewResources.None });

                    return Json(functionList);
                }
                catch (Exception ex)
                {
                    LogService.Error(ex.Message, ex);
                    throw ex;
                }
            }

            return Json(null);
        }

        #endregion

        #region Helpers

        private void InsertChildFunction(ref List<Function> orderedFunctionList,
            List<Function> functionList, Function parent)
        {
            var childFunctionList = functionList.Where(f => f.Parent != null && f.Parent.Id == parent.Id)
                .OrderBy(f => f.DisplayOrder).ToList();

            foreach (var childFunction in childFunctionList)
            {
                orderedFunctionList.Add(childFunction);
                InsertChildFunction(ref orderedFunctionList, functionList, childFunction);
            }
        }

        #endregion
    }
}
