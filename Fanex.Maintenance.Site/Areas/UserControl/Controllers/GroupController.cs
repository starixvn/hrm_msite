// --------------------------------------------------------------------------
// <copyright file="GroupController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.8.28">Add properties _currentContext</change>
// </history>

namespace Fanex.NAP.Admin.Areas.UserControl.Controllers
{
    using System;
    using System.Web.Mvc;

    using Fanex.Logging;

    using Core.Business;
    using Core.Criteria.Group;
    using Core.Service.Impl;
    using Core.Service.Interface;
    using Core.Web;
    using Core.Web.Attribute;

    [NapSuperAuth]
    public class GroupController : BaseController
    {
        #region Members

        private readonly IGroupService _groupService;
        private readonly ISiteService _siteService;
        private readonly IFunctionService _functionService;
        private readonly IUserService _userService;

        #endregion

        #region Ctors

        public GroupController()
            : this(null, null, null, null) { }

        public GroupController(IGroupService groupService, ISiteService siteService,
            IFunctionService functionService, IUserService userService)
        {
            _groupService = groupService ?? new GroupService { ServiceContext = _serviceContext };
            _siteService = siteService ?? new SiteService { ServiceContext = _serviceContext };
            _functionService = functionService ?? new FunctionService { ServiceContext = _serviceContext };
            _userService = userService ?? new UserService { ServiceContext = _serviceContext };
        }

        #endregion

        #region Manage

        [HttpGet]
        [NapAuth(Function = Functions.Manage_Group_1, Policy = Constant.Policy.Read)]
        public ActionResult Manage()
        {
            try
            {
                int policy = _userService.GetUserFunctionPolicy(_currentSession.UserId, (int)Functions.Manage_Group_1);

                ViewBag.ViewFlags.HasEditRight = policy >= (int)Constant.Policy.Host;
                ViewBag.GroupList = _groupService.GetAllWithNumOfUser();
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message, ex);
                throw ex;
            }

            return View();
        }

        #endregion

        #region Add New Group

        [HttpGet]
        [NapAuth(Function = Functions.Manage_Group_1, Policy = Constant.Policy.Host)]
        public ActionResult AddNew()
        {
            return View(new InsertGroupCriteria());
        }

        [HttpPost]
        [NapAuth(Function = Functions.Manage_Group_1, Policy = Constant.Policy.Host)]
        public ActionResult AddNew(InsertGroupCriteria criteria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    #region Check name exist

                    var group = _groupService.GetByName(criteria.GroupName);

                    if (!ReferenceEquals(group, null))
                    {
                        ModelState.AddModelError("SiteName",
                               string.Format(ViewResources.DuplicateMsg, criteria.GroupName));

                        return View(criteria);
                    }

                    #endregion

                    #region Add group

                    criteria.CreatedBy = _currentSession.UserId;
                    criteria.CreatedDate = DateTime.Now;

                    var result = _groupService.Insert(criteria);

                    if (result != null && result.IsSuccessful)
                    {
                        ViewBag.Success = true;
                    }

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

        #region Edit Group

        [HttpGet]
        [NapAuth(Function = Functions.Manage_Group_1, Policy = Constant.Policy.Host)]
        public ActionResult Edit(int id)
        {
            var criteria = new UpdateGroupCriteria();
            
            try
            {
                var group = _groupService.GetById(id);

                if (ReferenceEquals(group, null))
                {
                    return RedirectToAction("Manage");
                }

                criteria.GroupId = group.Id;
                criteria.Description = group.Description;
                criteria.GroupName = group.Name;
                criteria.IsActive = group.IsActive;
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message, ex);
                throw ex;
            }

            return View(criteria);
        }

        [HttpPost]
        [NapAuth(Function = Functions.Manage_Group_1, Policy = Constant.Policy.Host)]
        public ActionResult Edit(UpdateGroupCriteria criteria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    #region Check name exist

                    var group = _groupService.GetByName(criteria.GroupName);

                    if (!ReferenceEquals(group, null))
                    {
                        if (group.Id != criteria.GroupId)
                        {
                            ModelState.AddModelError("SiteName",
                                string.Format(ViewResources.DuplicateMsg, criteria.GroupName));

                            return View(criteria);
                        }
                    }

                    #endregion

                    #region update function

                    criteria.ModifiedBy = _currentSession.UserId;
                    criteria.ModifiedDate = DateTime.Now;

                    var result = _groupService.Update(criteria);

                    if (result != null && result.IsSuccessful)
                    {
                        ViewBag.Success = true;
                    }

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

        [NapAuth(Function = Functions.Manage_Group_1, Policy = Constant.Policy.Host)]
        public ActionResult Activate(int id, bool active)
        {
            if (Request.IsAjaxRequest())
            {
                try
                {
                    var result = _groupService.UpdateStatus(new UpdateStatusGroupCriteria
                    {
                        GroupId = id,
                        IsActive = active,
                        ModifiedBy = _currentSession.UserId,
                        ModifiedDate = DateTime.Now
                    });

                    if (result != null && result.IsSuccessful)
                    {
                        return Json(Constant.OK);
                    }
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

        #region Assign Permission

        [HttpGet]
        [NapAuth(Function = Functions.Assign_Function_To_Group_1, Policy = Constant.Policy.Read)]
        public ActionResult AssignPermission(int id, int siteId = 0)
        {
            var criteria = new AssignFunctionGroupCriteria();
            
            try
            {
                siteId = siteId == 0 || this._siteId != NapAdminId ? this._siteId : siteId;

                var group = _groupService.GetById(id);
                var site = _siteService.GetById(siteId);

                if (group == null || site == null)
                {
                    return RedirectToAction("Manage");
                }

                criteria.GroupId = group.Id;
                criteria.GroupName = group.Name;
                criteria.SiteId = site.Id;

                if (_currentSession.Level == (int)Constant.Level.PortalAdmin)
                {
                    ViewBag.ViewFlags.HasAssignFunctionRight = true;
                }
                else
                {
                    int policy = _userService.GetUserSiteFunctionKeyPolicy(_currentSession.UserId,
                        criteria.SiteId, FunctionKeys.Assign_Function_To_Group.ToString());

                    ViewBag.ViewFlags.HasAssignFunctionRight = policy >= (int)Constant.Policy.Host;
                }

                if (_siteId == NapAdminId)
                {
                    ViewBag.SiteSelectList = _siteService.GetAllSelectListItem(this._environment, true, criteria.SiteId);
                }

                ViewBag.FunctionList = _functionService.GetBySiteInHierarchy(criteria.SiteId, _environment, true);
                ViewBag.GroupFuncPolicyList = _functionService.GetByGroupAndSite(criteria.GroupId, criteria.SiteId,
                    _environment);
                ViewBag.UserFuncPolicyList =
                    _functionService.GetByUserAndSiteIncludeGroup(_currentSession.UserId, criteria.SiteId, _environment); ;
                ViewBag.UserLevel = _currentSession.Level;
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message, ex);
                throw ex;
            }

            return View(criteria);
        }

        [HttpPost]
        [NapAuth(Function = Functions.Assign_Function_To_Group_1, Policy = Constant.Policy.Read)]
        public ActionResult AssignPermission(AssignFunctionGroupCriteria criteria)
        {   
            try
            {
                var group = _groupService.GetById(criteria.GroupId);
                var site = _siteService.GetById(criteria.SiteId);

                if (group == null || site == null)
                {
                    return RedirectToAction("Manage");
                }

                criteria.ModifiedBy = _currentSession.UserId;
                criteria.ModifiedDate = DateTime.Now;

                var result = _groupService.AssignFunction(criteria);

                if (result != null && result.IsSuccessful)
                {
                    ViewBag.Success = true;
                }

                if (_currentSession.Level == (int)Constant.Level.PortalAdmin)
                {
                    ViewBag.ViewFlags.HasAssignFunctionRight = true;
                }
                else
                {
                    int policy = _userService.GetUserSiteFunctionKeyPolicy(_currentSession.UserId,
                        criteria.SiteId, FunctionKeys.Assign_Function_To_Group.ToString());

                    ViewBag.ViewFlags.HasAssignFunctionRight = policy >= (int)Constant.Policy.Host;
                }

                if (_siteId == NapAdminId)
                {
                    ViewBag.SiteSelectList = _siteService.GetAllSelectListItem(this._environment, true, criteria.SiteId);
                }

                ViewBag.FunctionList = _functionService.GetBySiteInHierarchy(criteria.SiteId, _environment, true);
                ViewBag.GroupFuncPolicyList = _functionService.GetByGroupAndSite(criteria.GroupId, criteria.SiteId,
                    _environment);
                ViewBag.UserFuncPolicyList =
                    _functionService.GetByUserAndSiteIncludeGroup(_currentSession.UserId, criteria.SiteId, _environment);
                ViewBag.UserLevel = _currentSession.Level;
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message, ex);
                throw ex;
            }

            return View(criteria);
        }

        #endregion

        #region Group Info

        [HttpPost]
        [NapAuth(Function = Functions.Manage_Group_1, Policy = Constant.Policy.Read)]
        public ActionResult GroupInfo(int id)
        {
            try
            {
                return View(_groupService.GetById(id));
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message, ex);
                throw ex;
            }
        }

        #endregion
    }
}