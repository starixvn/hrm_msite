// --------------------------------------------------------------------------
// <copyright file="LoginController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.16">Created</change>
// </history>

namespace Fanex.NAP.Hub.Areas.UserLogin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using Fanex.NAP.Core.Criteria.User;
    using Fanex.NAP.Core.Model;
    using Fanex.NAP.Core.Service.Impl;
    using Fanex.NAP.Core.Service.Interface;
    using Fanex.NAP.Core.Web;
    using Fanex.NAP.Core.Web.Auth;
    using Fanex.NAP.Core.Web.Session;
    using Fanex.Common;

    /// <summary>
    /// 
    /// </summary>
    public class LoginController : BaseController
    {
        #region members
        private IUserService _userService;
        private ISiteService _siteService;
        private IMenuItemService _menuItemService;
        private ICurrentSession _currentSession;
        #endregion

        #region ctors
        public LoginController()
            : this(null, null, null, null) { }

        public LoginController(IUserService userService, ISiteService siteService,
            IMenuItemService menuItemService, ICurrentSession currentSession)
        {
            _userService = userService ?? new UserService();
            _siteService = siteService ?? new SiteService();
            _menuItemService = menuItemService ?? new MenuItemService();
            _currentSession = currentSession ?? new CurrentSession();
        }
        #endregion

        #region helpers
        private void CreateUserMenu(User user)
        {
            _currentSession.MenuItemList = (IList<MenuItem>)_menuItemService.GetByUserAndSiteIncludeGroup(user.Id,
                _siteId, _environment, true);
        }

        private void CreateAuthInfo(User user)
        {
            var authInfo = new AuthInfo();
            authInfo.UserId = user.Id;
            authInfo.OnUserId = this._currentSession.OnUserId;

            var authTicket = new FormsAuthenticationTicket(
                1, 
                user.Username, 
                DateTime.Now,
                DateTime.Now.AddMinutes(20000), //????
                false, 
                authInfo.ToXml(), 
                FormsAuthentication.FormsCookiePath);

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true
            };

            Response.Cookies.Add(authCookie);
        }
        #endregion

        #region sign in
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            var user = _userService.GetByUsername(username) as User;

            if (!ReferenceEquals(user, null))
            {
                password = Cryptography.CfsEnCode(password);

                if (string.Compare(password, user.Password) == 0)
                {
                    var criteria = new UserCheckInUserCriteria
                    {
                        UserId = user.Id,
                        SiteId = this._siteId,
                        NetSessionId = Session.SessionID,
                        Ip = _currentContext.IP
                    };

                    _userService.UserCheckIn(ref criteria);

                    _currentSession.Level = _userService.GetUserSiteLevel(user.Id, this._siteId);
                    _currentSession.UserId = user.Id;
                    _currentSession.Username = username;
                    _currentSession.OnUserId = criteria.OnUserId;
                    _currentSession.NetSessionId = Session.SessionID.ToString();

                    if (this._siteId != 0)
                    {
                        CreateUserMenu(user);
                    }

                    CreateAuthInfo(user);

                    if (this.IsSiteLogin == 1)
                    {
                        return Redirect(FormsAuthentication.DefaultUrl);
                    }
                    else
                    {
                        return RedirectToAction("Site", "UserSite");
                    }
                }
                else
                {
                    ViewBag.Message = ViewResources.Invalid_Username_or_Password;
                }
            }
            else
            {
                ViewBag.Message = ViewResources.Invalid_Username_or_Password;
            }

            return View();
        }
        #endregion

        public ActionResult LogOut()
        {
            var criteria = new LogOutUserCriteria
                {
                    OnUserId = _currentSession.OnUserId
                };

            _userService.UserLogOut(criteria);
            FormsAuthentication.SignOut();
            _currentSession.ClearAll();

            return Redirect(FormsAuthentication.LoginUrl);
        }

        [HttpPost]
        public ActionResult ReCheckLogin()
        {
            UserReCheckInUserCriteria criteria = new UserReCheckInUserCriteria();
            criteria.OnUserId = Convert.ToInt32(_currentSession.OnUserId);
            _userService.ReUserCheckIn(ref criteria);

            return Json(criteria.IsKickedOut ? "1" : "0");
        }
    }
}