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
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Core.Business;
    using Core.Criteria.Function;
    using Core.Criteria.User;
    using Core.Domain;
    using Core.Model;
    using Core.Model.ValueObject;
    using Core.Service.Impl;
    using Core.Service.Interface;
    using Core.Web;
    using Core.Web.Attribute;
    using Fanex.Logging;
    using Fanex.Common;

    [NapSuperAuth]
    public class UserController : BaseController
    {
        #region Member

        private readonly IUserService _userService;
        private readonly IGroupService _groupService;
        private readonly ISiteService _siteService;
        private readonly IFunctionService _functionService;

        #endregion

        #region Ctor

        public UserController()
            : this(null, null, null, null) { }

        public UserController(IUserService userService, IGroupService groupService, ISiteService siteService,
            IFunctionService functionService)
        {
            _userService = userService ?? new UserService { ServiceContext = _serviceContext };
            _groupService = groupService ?? new GroupService { ServiceContext = _serviceContext };
            _siteService = siteService ?? new SiteService { ServiceContext = _serviceContext };
            _functionService = functionService ?? new FunctionService { ServiceContext = _serviceContext };
        }
        #endregion

        #region Private method

        private void InitViewFlag()
        {
            var viewFlag = (ViewFlags)ViewBag.ViewFlags;

            int policy = _userService.GetUserFunctionPolicy(_currentSession.UserId, (int)Functions.Manage_User_1);
            viewFlag.HasEditRight = policy.Equals((int)Constant.Policy.Host);

            policy = _userService.GetUserFunctionPolicy(_currentSession.UserId, (int)Functions.Assign_Site_To_User_1);
            viewFlag.HasAssignSiteRight = policy.Equals((int)Constant.Policy.Host);

            policy = _userService.GetUserFunctionPolicy(_currentSession.UserId, (int)Functions.Assign_Function_To_User_1);
            viewFlag.HasAssignFunctionRight = policy.Equals((int)Constant.Policy.Host);

            ViewBag.ViewFlags = viewFlag;

            
            
        }

        private void GetDataForAssignPermission(AssignFunctionUserCriteria criteria)
        {
            int policy = _userService.GetUserSiteFunctionKeyPolicy(_currentSession.UserId,
                criteria.SiteId, FunctionKeys.Assign_Function_To_User.ToString());

            ViewBag.ViewFlags.HasAssignFunctionRight = policy >= (int)Constant.Policy.Host;

            if (_currentSession.Level == (int)Constant.Level.PortalAdmin)
            {
                ViewBag.Sites = _siteService.GetAllSelectListItem(this._environment, true, criteria.SiteId);
            }
            else
            {
                ViewBag.Sites = _siteService.GetSelectListItemByUserAndFunctionKeyIncludeGroup(_currentSession.UserId,
                    FunctionKeys.Assign_Function_To_User.ToString(), this._environment, policy: Constant.Policy.Read,
                    selectedSiteId: criteria.SiteId);
            }

            ViewBag.FunctionList = _functionService
                .GetBySiteInHierarchy(criteria.SiteId, _environment, true).ToList();

            ViewBag.UserFuncPolicyList = _functionService.GetByUserAndSiteIncludeGroup(criteria.UserId, criteria.SiteId, 
                this._environment);

            ViewBag.CurrentUserFuncPolicyList =
                _functionService.GetByUserAndSiteIncludeGroup(_currentSession.UserId, criteria.SiteId, _environment);
        }

        #endregion

        #region Manage

        [HttpGet]
        [NapAuth(Function = Functions.Manage_User_1, Policy = Constant.Policy.Read)]
        public ActionResult Manage(int groupId = 0, int siteId = 0)
        {
            if (siteId == 0)
            {
                siteId = this._siteId;
            }

            int manageUserPolicy = _userService.GetUserFunctionPolicy(_currentSession.UserId,
                (int)Functions.Manage_User_1);
            int assignSitePolicy = _userService.GetUserFunctionPolicy(_currentSession.UserId, 
                (int)Functions.Assign_Site_To_User_1);

            ViewBag.ViewFlags.HasEditRight = manageUserPolicy >= (int)Constant.Policy.Host;
            ViewBag.ViewFlags.HasAssignSiteRight = assignSitePolicy >= (int)Constant.Policy.Host;
            ViewBag.ViewFlags.HasLevelRight = siteId != 0;

            if (this._siteId == NapAdminId)
            {
                IList<SelectListItem> siteItemList;
                
                if (_currentSession.Level == (int)Constant.Level.PortalAdmin)
                {
                    siteItemList = _siteService.GetAllSelectListItem(this._environment, true, siteId);
                }
                else
                {
                    siteItemList = _siteService.GetSelectListItemByUserAndFunctionKeyIncludeGroup(_currentSession.UserId,
                        FunctionKeys.Manage_User.ToString(), this._environment, policy: Constant.Policy.Read,
                        selectedSiteId: siteId);
                }

                siteItemList.Add(new SelectListItem
                {
                    Text = ViewResources.No_Site,
                    Value = "0"
                });

                ViewBag.Sites = siteItemList;
            }

            ViewBag.Groups = _groupService.GetAllSelectListItem(selectedGroupId: groupId);

            var listUser = _userService.SearchUser(string.Empty, groupId, siteId);
            
            ViewBag.SiteId = siteId;
            ViewBag.UserId = _currentSession.UserId;
            if (siteId != 0)
            {
                ViewBag.Level = _userService.GetUserSiteLevel(_currentSession.UserId, siteId);
            }

            return View(listUser);
        }

        [HttpPost]
        [NapAuth(Function = Functions.Manage_User_1, Policy = Constant.Policy.Read)]
        public ActionResult Manage(string username, int groupId = 0, int siteId = 0)
        {
            if (siteId == 0 && this._siteId != NapAdminId)
            {
                siteId = this._siteId;
            }

            int manageUserPolicy = _userService.GetUserFunctionPolicy(_currentSession.UserId,
                (int)Functions.Manage_User_1);
            int assignSitePolicy = _userService.GetUserFunctionPolicy(_currentSession.UserId,
                (int)Functions.Assign_Site_To_User_1);

            ViewBag.ViewFlags.HasEditRight = manageUserPolicy >= (int)Constant.Policy.Host;
            ViewBag.ViewFlags.HasAssignSiteRight = assignSitePolicy >= (int)Constant.Policy.Host;
            ViewBag.ViewFlags.HasLevelRight = siteId != 0;

            if (this._siteId == NapAdminId)
            {
                IList<SelectListItem> siteItemList;

                if (_currentSession.Level == (int)Constant.Level.PortalAdmin)
                {
                    siteItemList = _siteService.GetAllSelectListItem(this._environment, true, siteId);
                }
                else
                {
                    siteItemList = _siteService.GetSelectListItemByUserAndFunctionKeyIncludeGroup(_currentSession.UserId,
                        FunctionKeys.Manage_User.ToString(), this._environment, policy: Constant.Policy.Read,
                        selectedSiteId: siteId);
                }

                siteItemList.Add(new SelectListItem
                {
                    Text = ViewResources.No_Site,
                    Value = "0"
                });

                ViewBag.Sites = siteItemList;
            }

            ViewBag.Groups = _groupService.GetAllSelectListItem(selectedGroupId: groupId);

            IEnumerable<UserLevel> listUser = null;

            if (ModelState.IsValid)
            {
                listUser = _userService.SearchUser(username, groupId, siteId);
            }

            ViewBag.SiteId = siteId;
            ViewBag.UserId = _currentSession.UserId;
            if (siteId != 0)
            {
                ViewBag.Level = _userService.GetUserSiteLevel(_currentSession.UserId, siteId);
            }

            return View(listUser);
        }

        #endregion

        #region Assign Site

        [HttpGet]
        [NapAuth(Function = Functions.Assign_Site_To_User_1, Policy = Constant.Policy.Read)]
        public ActionResult AssignSite(int id)
        {
            InitViewFlag();

            ViewBag.UserId = _currentSession.UserId;
            ViewBag.IsPortalAdmin = _currentSession.Level == (int)Constant.Level.PortalAdmin;

            var siteList = new List<ISite>();

            List<SiteLevel> siteLevelList = _siteService
                .GetByUser(_currentSession.UserId, this._environment, true)
                .Where(sl => sl.Level == (int)Constant.Level.SiteAdmin ||
                    sl.Level == (int)Constant.Level.PortalAdmin ||
                    sl.Site.Id == this._siteId)
                .ToList();

            siteList.AddRange(siteLevelList.Select(sl => sl.Site));

            ViewBag.Sites = siteList;
            ViewBag.Level = _currentSession.Level;
            ViewBag.UserId = id;
            ViewBag.Username = _userService.GetById(id).Username;
            ViewBag.IsPortalAdminSelfEdit = _currentSession.UserId == id;

            return View();
        }

        #endregion

        #region User Info

        [HttpPost]
        [NapAuth(Function = Functions.Manage_User_1, Policy = Constant.Policy.Read)]
        public ActionResult UserInfo(int id)
        {
            try
            {
                return View(_userService.GetById(id));
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message, ex);
                throw ex;
            }
        }

        #endregion

        #region Add User

        [HttpGet]
        [NapAuth(Function = Functions.Manage_User_1, Policy = Constant.Policy.Host)]
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        [NapAuth(Function = Functions.Manage_User_1, Policy = Constant.Policy.Host)]
        public ActionResult AddUser(string username)
        {
            IEnumerable<IUser> listUser = null;

            try
            {
                if (ModelState.IsValid)
                {
                    listUser = _userService.SearchByUsername(username);
                }

                ViewBag.Groups = _groupService.GetAllSelectListItem();
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message, ex);
                throw ex;
            }

            return View(listUser);
        }

        #endregion

        #region Add New User

        [HttpGet]
        [NapAuth(Function = Functions.Manage_User_1, Policy = Constant.Policy.Host)]
        public ActionResult AddNew()
        {
            var criteria = new InsertCustCriteria();

            ViewBag.Groups = _groupService.GetAllSelectListItem();

            return View(criteria);
        }

        [HttpPost]
        [NapAuth(Function = Functions.Manage_User_1, Policy = Constant.Policy.Host)]
        public ActionResult AddNew(InsertCustCriteria criteria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    #region validation

                    // check username duplication
                    IUser objUser = _userService.GetByUsername(criteria.Username);

                    if (!ReferenceEquals(objUser, null))
                    {
                        ModelState.AddModelError("Username", string.Format(ViewResources.DuplicateMsg, criteria.Username));
                        return View(criteria);
                    }

                    #endregion

                    #region add new user

                    criteria.UserPwd = Cryptography.CfsEnCode(criteria.UserPwd);
                    criteria.FirstName = criteria.FirstName == null ? string.Empty : criteria.FirstName;
                    criteria.LastName = criteria.LastName == null ? string.Empty : criteria.LastName;
                    criteria.Email = criteria.Email == null ? string.Empty : criteria.Email;

                    var result = _userService.InsertOrUpdate(criteria);

                    if (result != null && result.IsSuccessful)
                    {
                        ViewBag.Success = true;
                    }

                    #endregion
                }

                ViewBag.Groups = _groupService.GetAllSelectListItem(selectedGroupId: criteria.GroupId);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message, ex);
                throw ex;
            }

            return View(criteria);
        }

        #endregion

        #region Edit User

        [HttpGet]
        [NapAuth(Function = Functions.Manage_User_1, Policy = Constant.Policy.Host)]
        public ActionResult Edit(int id)
        {
            var criteria = new UpdateCustCriteria();

            try
            {
                IUser user = _userService.GetById(id);

                if (user == null)
                {
                    return RedirectToAction("Manage");
                }

                criteria.CustId = user.Id;
                criteria.FirstName = user.FirstName;
                criteria.LastName = user.LastName;
                criteria.Email = user.Email;
                criteria.Username = user.Username;
                criteria.GroupId = user.Group != null ? user.Group.Id : 0;

                if (user.Group == null)
                {
                    ViewBag.Groups = _groupService.GetAllSelectListItem();
                }
                else
                {
                    ViewBag.Groups = _groupService.GetAllSelectListItem(selectedGroupId: user.Group.Id);
                }
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message, ex);
                throw ex;
            }

            return View(criteria);
        }

        [HttpPost]
        [NapAuth(Function = Functions.Manage_User_1, Policy = Constant.Policy.Host)]
        public ActionResult Edit(UpdateCustCriteria criteria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    criteria.FirstName = criteria.FirstName == null ? string.Empty : criteria.FirstName;
                    criteria.LastName = criteria.LastName == null ? string.Empty : criteria.LastName;
                    criteria.Email = criteria.Email == null ? string.Empty : criteria.Email;

                    var result = _userService.InsertOrUpdate(criteria);

                    if (result != null && result.IsSuccessful)
                    {
                        ViewBag.Success = true;
                    }
                }

                ViewBag.Groups = _groupService.GetAllSelectListItem(selectedGroupId: criteria.GroupId);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message, ex);
                throw ex;
            }

            return View(criteria);
        }

        #endregion

        #region Reset Password

        [HttpGet]
        [NapAuth(Function = Functions.Manage_User_1, Policy = Constant.Policy.Host)]
        public ActionResult ResetPassword(int id)
        {
            var criteria = new ResetPasswordCustCriteria();

            try
            {
                IUser user = _userService.GetById(id);

                if (user == null)
                {
                    return RedirectToAction("Manage");
                }

                criteria.CustId = user.Id;
                criteria.Username = user.Username;
                criteria.UserPwd = string.Empty;
                criteria.ConfirmUserPwd = string.Empty;
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message, ex);
                throw ex;
            }

            return View(criteria);
        }

        [HttpPost]
        [NapAuth(Function = Functions.Manage_User_1, Policy = Constant.Policy.Host)]
        public ActionResult ResetPassword(ResetPasswordCustCriteria criteria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IUser user = _userService.GetById(criteria.CustId);

                    if (user == null)
                    {
                        return RedirectToAction("Manage");
                    }

                    #region validation

                    // check password confirmation
                    if (!criteria.UserPwd.Equals(criteria.ConfirmUserPwd))
                    {
                        ModelState.AddModelError("Password", ViewResources.WrongPasswordConfirmation);

                        return View(criteria);
                    }

                    #endregion

                    criteria.UserPwd = Cryptography.CfsEnCode(criteria.UserPwd);

                    var result = _userService.InsertOrUpdate(criteria);

                    if (result != null && result.IsSuccessful)
                    {
                        ViewBag.Success = true;
                    }
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

        #region Assign Permission

        [HttpGet]
        [NapAuth(Function = Functions.Assign_Function_To_User_1, Policy = Constant.Policy.Read)]
        public ActionResult AssignPermission(int id, int siteId = 0)
        {
            var criteria = new AssignFunctionUserCriteria();

            try
            {
                if (siteId == 0)
                {
                    siteId = this._siteId;
                }

                var user = _userService.GetById(id);
                var site = _siteService.GetById(siteId);

                if (ReferenceEquals(user, null) || ReferenceEquals(site, null))
                {
                    return RedirectToAction("Manage");
                }

                criteria.UserId = id;
                criteria.Username = user.Username;
                criteria.SiteId = siteId;

                if (!ReferenceEquals(user.Group, null))
                {
                    ViewBag.GroupFuncPolicyList = _functionService.GetByGroupAndSite(user.Group.Id, 
                        criteria.SiteId, this._environment);
                }

                if (_currentSession.Level == (int)Constant.Level.PortalAdmin)
                {
                    ViewBag.ViewFlags.HasAssignFunctionRight = true;
                }
                else
                {
                    int policy = _userService.GetUserSiteFunctionKeyPolicy(_currentSession.UserId,
                    criteria.SiteId, FunctionKeys.Assign_Function_To_User.ToString());

                    ViewBag.ViewFlags.HasAssignFunctionRight = policy >= (int)Constant.Policy.Host;
                }

                if (_currentSession.Level == (int)Constant.Level.PortalAdmin)
                {
                    ViewBag.Sites = _siteService.GetAllSelectListItem(this._environment, true, criteria.SiteId);
                }
                else
                {
                    ViewBag.Sites = _siteService.GetSelectListItemByUserAndFunctionKeyIncludeGroup(_currentSession.UserId,
                        FunctionKeys.Assign_Function_To_User.ToString(), this._environment, policy: Constant.Policy.Read,
                        selectedSiteId: criteria.SiteId);
                }

                ViewBag.FunctionList = _functionService
                    .GetBySiteInHierarchy(criteria.SiteId, _environment, true).ToList();

                ViewBag.UserFuncPolicyList = _functionService.GetByUserAndSite(criteria.UserId, criteria.SiteId,
                    this._environment);

                ViewBag.CurrentUserFuncPolicyList =
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

        [HttpPost]
        [NapAuth(Function = Functions.Assign_Function_To_User_1, Policy = Constant.Policy.Read)]
        public ActionResult AssignPermission(AssignFunctionUserCriteria criteria)
        {
            try
            {
                var user = _userService.GetById(criteria.UserId);
                var site = _siteService.GetById(criteria.SiteId);

                if (user == null || site == null)
                {
                    return RedirectToAction("Manage");
                }

                criteria.Username = user.Username;
                criteria.ModifiedBy = _currentSession.UserId;
                criteria.ModifiedDate = DateTime.Now;

                var result = _userService.AssignFunction(criteria);

                if (result != null && result.IsSuccessful)
                {
                    ViewBag.Success = true;
                }

                if (!ReferenceEquals(user.Group, null))
                {
                    ViewBag.GroupFuncPolicyList = _functionService.GetByGroupAndSite(user.Group.Id,
                        criteria.SiteId, this._environment);
                }

                if (_currentSession.Level == (int)Constant.Level.PortalAdmin)
                {
                    ViewBag.ViewFlags.HasAssignFunctionRight = true;
                }
                else
                {
                    int policy = _userService.GetUserSiteFunctionKeyPolicy(_currentSession.UserId,
                    criteria.SiteId, FunctionKeys.Assign_Function_To_User.ToString());

                    ViewBag.ViewFlags.HasAssignFunctionRight = policy >= (int)Constant.Policy.Host;
                }

                if (_currentSession.Level == (int)Constant.Level.PortalAdmin)
                {
                    ViewBag.Sites = _siteService.GetAllSelectListItem(this._environment, true, criteria.SiteId);
                }
                else
                {
                    ViewBag.Sites = _siteService.GetSelectListItemByUserAndFunctionKeyIncludeGroup(_currentSession.UserId,
                        FunctionKeys.Assign_Function_To_User.ToString(), this._environment, policy: Constant.Policy.Read,
                        selectedSiteId: criteria.SiteId);
                }

                ViewBag.FunctionList = _functionService
                    .GetBySiteInHierarchy(criteria.SiteId, _environment, true).ToList();

                ViewBag.UserFuncPolicyList = _functionService.GetByUserAndSite(criteria.UserId, criteria.SiteId,
                    this._environment);

                ViewBag.CurrentUserFuncPolicyList =
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

        #region Ajax

        [HttpPost]
        [NapAuth(Function = Functions.Manage_User_1, Policy = Constant.Policy.Read)]
        public ActionResult UserList(int groupId, int siteId = 0)
        {
            if (Request.IsAjaxRequest())
            {
                try
                {
                    var userList = _userService.GetByGroupAndSite(groupId, siteId);

                    return View(userList);
                }
                catch (Exception ex)
                {
                    LogService.Error(ex.Message, ex);
                    throw ex;
                }
            }

            return new EmptyResult();
        }

        [HttpPost]
        [NapAuth(Function = Functions.Manage_User_1, Policy = Constant.Policy.Read)]
        public ActionResult UserLevelList(int siteId, int groupId = 0)
        {
            if (Request.IsAjaxRequest())
            {
                try
                {
                    var userList = _userService.SearchUser(string.Empty, groupId, siteId);

                    return View(userList);
                }
                catch (Exception ex)
                {
                    LogService.Error(ex.Message, ex);
                    throw ex;
                }
            }

            return new EmptyResult();
        }

        [HttpPost]
        [NapAuth(Function = Functions.Manage_User_1, Policy = Constant.Policy.Read)]
        public ActionResult GetUserSite(int userId)
        {
            if (Request.IsAjaxRequest())
            {
                try
                {
                    IEnumerable<SiteLevel> listSiteLive = _siteService.GetByUser(userId, this._environment);

                    return Json(listSiteLive);
                }
                catch (Exception ex)
                {
                    LogService.Error(ex.Message, ex);
                    throw ex;
                }
            }

            return Json(null);
        }

        [HttpPost]
        [NapAuth(Function = Functions.Assign_Site_To_User_1, Policy = Constant.Policy.Host)]
        public ActionResult AssignSiteToUser(int userId, string value)
        {
            if (Request.IsAjaxRequest())
            {
                try
                {
                    string xmlValue = "<ROOT>";
                    string[] strValueSiteLevel = value.Split(';');

                    foreach (var item in strValueSiteLevel)
                    {
                        if (item != string.Empty)
                        {
                            string[] strTemp = item.Split(',');
                            xmlValue += "<SiteLevel SiteId='" + strTemp[0] + "' Level='" + strTemp[1] + "' ></SiteLevel>";
                        }
                    }

                    xmlValue += "</ROOT>";

                    var criteria = new AssignSiteUserCriteria
                    {
                        ModifiedBy = _currentSession.UserId,
                        ModifiedDate = DateTime.Now,
                        UserId = userId,
                        Value = xmlValue
                    };

                    var result = _userService.AssignSite(criteria);

                    if (result != null && result.IsSuccessful)
                    {
                        return Json(true);
                    }
                }
                catch (Exception ex)
                {
                    LogService.Error(ex.Message, ex);
                    throw ex;
                }
            }

            return Json(null);
        }

        [HttpPost]
        public ActionResult GetUserFunctionPolicy(int userId)
        {
            if (Request.IsAjaxRequest())
            {
                try
                {
                    IEnumerable<FunctionPolicy> listFunctionPolicy = _functionService.GetByUser(userId, this._environment);

                    return Json(listFunctionPolicy);
                }
                catch (Exception ex)
                {
                    LogService.Error(ex.Message, ex);
                    throw ex;
                }
            }

            return Json(null);
        }

        [HttpPost]
        public ActionResult GetGroupFunctionPolicy(int groupId)
        {
            if (Request.IsAjaxRequest())
            {
                if (groupId != 0)
                {
                    try
                    {
                        IEnumerable<FunctionPolicy> listFunctionPolicy = _functionService.GetByGroup(groupId,
                            this._environment);

                        return Json(listFunctionPolicy);
                    }
                    catch (Exception ex)
                    {
                        LogService.Error(ex.Message, ex);
                        throw ex;
                    }
                }
            }

            return Json(null);
        }

        [HttpPost]
        [NapAuth(Function = Functions.Manage_User_1, Policy = Constant.Policy.Host)]
        public ActionResult AddUserToGroup(int userId, int groupId)
        {
            if (Request.IsAjaxRequest())
            {
                if (userId != 0 && groupId != 0)
                {
                    var criteria = new InsertUserCriteria
                    {
                        UserId = userId,
                        GroupId = groupId,
                        CreatedBy = _currentSession.UserId,
                        CreatedDate = DateTime.Now
                    };

                    try
                    {
                        var result = _userService.InsertOrUpdate(criteria);

                        if (result != null && result.IsSuccessful)
                        {
                            return Json(true);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogService.Error(ex.Message, ex);
                        throw ex;
                    }
                }
            }

            return Json(null);
        }

        [HttpPost]
        [NapAuth(Function = Functions.Manage_User_1, Policy = Constant.Policy.Read)]
        public ActionResult GetUserByGroup(int groupId)
        {
            if (Request.IsAjaxRequest())
            {
                try
                {
                    IEnumerable<IUser> listUser = _userService.GetByGroupAndSite(groupId);

                    return Json(listUser);
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
    }
}