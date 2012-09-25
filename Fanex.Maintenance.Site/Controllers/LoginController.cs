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
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using Fanex.Common;
    using Fanex.Maintenance.Core.Criteria.User;
    using Fanex.Maintenance.Core.Model;
    using Fanex.Maintenance.Core.Service.Impl;
    using Fanex.Maintenance.Core.Service.Interface;

    public class LoginController : Controller
    {
        #region Member

        private readonly IUserService _userService;

        #endregion

        #region Ctor

        public LoginController()
            : this(null)
        {
        }

        public LoginController(IUserService userService)
        {
            _userService = userService ?? new UserService();
        }

        #endregion        

        #region Action

        [HttpGet]
        public ActionResult Index()
        {
            return View(new UserLoginCriteria());
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(UserLoginCriteria criteria)
        {
            ViewBag.Message = string.Empty;
            if (ModelState.IsValid)
            {
                criteria.UserPwd = criteria.UserPwd != null ? Cryptography.CfsEnCode(criteria.UserPwd) : string.Empty;
                criteria.LoginIp = HttpContext.Request.UserHostAddress;
                criteria.From = HttpContext.Request.UrlReferrer.AbsoluteUri;
                criteria.CustId = 0;
                criteria.PageAccess = string.Empty;
                criteria.RoleId = 0;
                CommandResult command = (CommandResult)_userService.Login(criteria);
                ViewBag.Message = command.Message;

                if (command.IsSuccessful)
                {
                    FormsAuthentication.SetAuthCookie(criteria.Username, false);
                    FormsAuthentication.RedirectFromLoginPage(criteria.Username, false);
                }

                return View(criteria);
            }

            return View(criteria);
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction(string.Empty);
        }

        #endregion        
    }
}
