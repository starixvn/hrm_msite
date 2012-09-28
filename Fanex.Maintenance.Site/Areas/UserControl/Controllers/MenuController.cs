// --------------------------------------------------------------------------
// <copyright file="MenuController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.11">Created</change>
// </history>

namespace Fanex.NAP.Admin.Areas.UserControl.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Fanex.Logging;

    using Core.Business;
    using Core.Criteria.MenuItem;
    using Core.Domain;
    using Core.Model;
    using Core.Service.Impl;
    using Core.Service.Interface;
    using Core.Web;
    using Core.Web.Attribute;

    [NapSuperAuth]
    public class MenuController : BaseController
    {
        #region Properties

        private readonly IMenuItemService _menuItemService;
        private readonly ISiteService _siteService;
        private readonly IFunctionService _functionService;
        private readonly IUserService _userService;

        #endregion

        #region Contructors

        public MenuController()
            : this(null, null, null, null) { }

        public MenuController(IMenuItemService menuitemService, ISiteService siteService, 
            IFunctionService functionService, IUserService userService)
        {
            _menuItemService = menuitemService ?? new MenuItemService { ServiceContext = this._serviceContext };
            _siteService = siteService ?? new SiteService { ServiceContext = this._serviceContext };
            _functionService = functionService ?? new FunctionService { ServiceContext = this._serviceContext };
            _userService = userService ?? new UserService { ServiceContext = this._serviceContext };
        }

        #endregion

        #region Manage Menu item

        [HttpGet]
        [NapAuth(Function = Functions.Manage_Menu_1, Policy = Constant.Policy.Read)]
        public ActionResult Manage()
        {
            var criteria = new GetBySiteMenuItemCriteria
            {
                SiteId = this._siteId,
                Environment = this._environment
            };

            try
            {
                if (_currentSession.Level == (int)Constant.Level.PortalAdmin)
                {
                    ViewBag.Site = _siteService.GetAllSelectListItem(criteria.Environment, true, criteria.SiteId);
                    ViewBag.ViewFlags.HasEditRight = true;
                }
                else
                {
                    ViewBag.Site =
                        _siteService.GetSelectListItemByUserAndFunctionKeyIncludeGroup(_currentSession.UserId,
                        FunctionKeys.Manage_Menu.ToString(), criteria.Environment, policy: Constant.Policy.Read,
                        selectedSiteId: criteria.SiteId);
                    // get user policy

                    int policy = _userService.GetUserSiteFunctionKeyPolicy(_currentSession.UserId,
                        criteria.SiteId, FunctionKeys.Manage_Menu.ToString());

                    ViewBag.ViewFlags.HasEditRight = policy >= (int)Constant.Policy.Host;
                }

                ViewBag.MenuItemList = _menuItemService.GetBySiteInHierarchy(criteria.SiteId, criteria.Environment);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message, ex);
                throw ex;
            }

            return View(criteria);
        }

        [HttpPost]
        [NapAuth(Function = Functions.Manage_Menu_1, Policy = Constant.Policy.Read)]
        public ActionResult Manage(GetBySiteMenuItemCriteria criteria)
        {
            try
            {
                if (_currentSession.Level == (int)Constant.Level.PortalAdmin)
                {
                    ViewBag.Site = _siteService.GetAllSelectListItem(criteria.Environment, true, criteria.SiteId);
                }
                else
                {
                    ViewBag.Site =
                        _siteService.GetSelectListItemByUserAndFunctionKeyIncludeGroup(_currentSession.UserId,
                        FunctionKeys.Manage_Menu.ToString(), criteria.Environment, policy: Constant.Policy.Read,
                        selectedSiteId: criteria.SiteId);
                }
                ViewBag.MenuItemList = _menuItemService.GetBySiteInHierarchy(criteria.SiteId, criteria.Environment);

                // get user policy

                int policy = _userService.GetUserSiteFunctionKeyPolicy(_currentSession.UserId,
                    criteria.SiteId, FunctionKeys.Manage_Menu.ToString());

                ViewBag.ViewFlags.HasEditRight = policy >= (int)Constant.Policy.Host;
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message, ex);
                throw ex;
            }

            return View(criteria);
        }

        #endregion

        #region Add New Menu

        [HttpGet]
        [NapAuth(Function = Functions.Manage_Menu_1, Policy = Constant.Policy.Host)]
        public ActionResult AddNewMenuItem()
        {
            var criteria = new InsertMenuItemCriteria
            {
                Environment = this._environment,
                Policy = (int)Constant.Policy.Read,
                SiteId = this._siteId
            };

            ViewBag.Parent = _menuItemService.GetAllSelectListItem(this._environment, isActive: true);
            ViewBag.Function = _functionService.GetSelectListItemBySite(criteria.SiteId, this._environment, isActive: true);
            if (_currentSession.Level == (int)Constant.Level.PortalAdmin)
            {
                ViewBag.Site = _siteService.GetAllSelectListItem(criteria.Environment, true, criteria.SiteId);
            }
            else
            {
                ViewBag.Site =
                    _siteService.GetSelectListItemByUserAndFunctionKeyIncludeGroup(_currentSession.UserId,
                    FunctionKeys.Manage_Menu.ToString(), criteria.Environment, policy: Constant.Policy.Host,
                    selectedSiteId: criteria.SiteId);
            }

            return View(criteria);
        }

        [HttpPost]
        [NapAuth(Function = Functions.Manage_Menu_1, Policy = Constant.Policy.Host)]
        public ActionResult AddNewMenuItem(InsertMenuItemCriteria criteria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    #region Check Duplicate MenuItem Name

                    IMenuItem menuItem = _menuItemService.GetByNameAndSite(criteria.MenuItemName, criteria.SiteId);
                    if (!ReferenceEquals(menuItem, null))
                    {
                        ModelState.AddModelError("MenuItemName",
                           string.Format(ViewResources.DuplicateMsg, criteria.MenuItemName));

                        return View(criteria);
                    }

                    #endregion

                    #region Add new

                    criteria.CreatedBy = _currentSession.UserId;
                    criteria.CreatedDate = DateTime.Now;
                    criteria.ParentId = criteria.ParentId == 0 ? null : criteria.ParentId;
                    criteria.ParentName = criteria.ParentId == 0 || criteria.ParentId == null ? null 
                        : criteria.ParentName;

                    var result = _menuItemService.Insert(criteria);

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

            ViewBag.Parent = _menuItemService.GetAllSelectListItem(this._environment, isActive: true);
            ViewBag.Function = _functionService.GetSelectListItemBySite(criteria.SiteId, this._environment, isActive: true);
            if (_currentSession.Level == (int)Constant.Level.PortalAdmin)
            {
                ViewBag.Site = _siteService.GetAllSelectListItem(criteria.Environment, true, criteria.SiteId);
            }
            else
            {
                ViewBag.Site =
                    _siteService.GetSelectListItemByUserAndFunctionKeyIncludeGroup(_currentSession.UserId,
                    FunctionKeys.Manage_Menu.ToString(), criteria.Environment, policy: Constant.Policy.Host,
                    selectedSiteId: criteria.SiteId);
            }

            return View(criteria);
        }

        #endregion

        #region Edit Menu Item

        [HttpGet]
        [NapAuth(Function = Functions.Manage_Menu_1, Policy = Constant.Policy.Host)]
        public ActionResult EditMenuItem(int menuItemId)
        {
            var criteria = new UpdateMenuItemCriteria();

            try
            {
                var menuItem = (MenuItem)_menuItemService.GetById(menuItemId);

                if (menuItem == null)
                {
                    return RedirectToAction("Manage");
                }

                criteria.MenuItemId = menuItem.Id;
                criteria.MenuItemName = menuItem.Name;
                criteria.Description = menuItem.Description;
                criteria.DisplayOrder = menuItem.DisplayOrder;
                criteria.FunctionId = menuItem.Function.Id;
                criteria.IsActive = menuItem.IsActive;
                criteria.ParentId = menuItem.Parent != null ? menuItem.Parent.Id : 0;
                criteria.ParentName = menuItem.Parent != null ? menuItem.Parent.Name : string.Empty;
                criteria.Policy = menuItem.Policy;
                criteria.SiteId = menuItem.Site.Id;
                criteria.SiteName = menuItem.Site.Name;
                criteria.Url = menuItem.Url;
                criteria.Environment = menuItem.Environment;
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message, ex);
                throw ex;
            }

            ViewBag.Function = _functionService.GetSelectListItemBySite(criteria.SiteId, criteria.Environment, isActive: true, 
                selectedFunctionId: criteria.FunctionId);

            if (criteria.ParentId == null)
            {
                ViewBag.Parent = _menuItemService.GetParentSelectListItemById(criteria.MenuItemId);
            }
            else
            {
                ViewBag.Parent = _menuItemService.GetParentSelectListItemById(criteria.MenuItemId, (int)criteria.ParentId);
            }

            return View(criteria);
        }

        [HttpPost]
        [NapAuth(Function = Functions.Manage_Menu_1, Policy = Constant.Policy.Host)]
        public ActionResult EditMenuItem(UpdateMenuItemCriteria criteria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    #region Check Duplicate MenuItem Name

                    IMenuItem menuItem = _menuItemService.GetByNameAndSite(criteria.MenuItemName, criteria.SiteId);

                    if (!ReferenceEquals(menuItem, null))
                    {
                        if (menuItem.Id != criteria.MenuItemId)
                        {
                            ModelState.AddModelError("MenuItemName",
                               string.Format(ViewResources.DuplicateMsg, criteria.MenuItemName));

                            return View(criteria);
                        }
                    }

                    #endregion

                    #region Update menu item

                    criteria.ModifierId = _currentSession.UserId;
                    criteria.ModifiedDate = DateTime.Now;
                    criteria.ParentId = criteria.ParentId == 0 ? null : criteria.ParentId;
                    criteria.ParentName = criteria.ParentId == 0 || criteria.ParentId == null ? null 
                        : criteria.ParentName;

                    var result = _menuItemService.Update(criteria);

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

            ViewBag.Function = _functionService.GetSelectListItemBySite(criteria.SiteId, criteria.Environment, 
                isActive: true, selectedFunctionId: criteria.FunctionId);

            if (criteria.ParentId == null)
            {
                ViewBag.Parent = _menuItemService.GetParentSelectListItemById(criteria.MenuItemId);
            }
            else
            {
                ViewBag.Parent = _menuItemService.GetParentSelectListItemById(criteria.MenuItemId, (int)criteria.ParentId);
            }

            return View(criteria);
        }
        #endregion

        #region AjaxRequest

        /// <summary>
        /// Get list menu item by site id
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        [HttpPost]
        [NapAuth(Function = Functions.Manage_Menu_1, Policy = Constant.Policy.Host)]
        public ActionResult ListMenuItemBySite(int siteId)
        {
            if (Request.IsAjaxRequest())
            {
                List<IMenuItem> menuItemList = new List<IMenuItem>();

                try
                {
                    menuItemList.Add(new MenuItem { Id = 0, Name = ViewResources.None });
                    menuItemList.AddRange(_menuItemService.GetBySite(siteId, this._environment).ToList());
                }
                catch (Exception ex)
                {
                    LogService.Error(ex.Message, ex);
                    throw ex;
                }

                return Json(menuItemList);
            }

            return Json(new List<IMenuItem>());
        }

        /// <summary>
        /// Get list function by site id
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        [HttpPost]
        [NapAuth(Function = Functions.Manage_Menu_1, Policy = Constant.Policy.Host)]
        public ActionResult ListFunctionBySite(int siteId)
        {
            if (Request.IsAjaxRequest())
            {
                try
                {
                    return Json(_functionService.GetBySite(siteId, this._environment, isActive: true));
                }
                catch (Exception ex)
                {
                    LogService.Error(ex.Message, ex);
                    throw ex;
                }
            }

            return Json(null);
        }

        /// <summary>
        /// Update status menu 
        /// </summary>
        /// <param name="menuItemId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        [HttpPost]
        [NapAuth(Function = Functions.Manage_Menu_1, Policy = Constant.Policy.Host)]
        public ActionResult Activate(int id, bool active)
        {
            if (Request.IsAjaxRequest())
            {
                try
                {
                    var result = _menuItemService.UpdateStatus(new UpdateStatusMenuItemCriteria
                    {
                        IsActive = active,
                        MenuItemId = id,
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
    }
}