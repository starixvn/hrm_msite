using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Fanex.Common;
using Fanex.NAP.Core.Criteria.User;
using Fanex.NAP.Core.Model;
using Fanex.NAP.Core.Service.Impl;
using Fanex.NAP.Core.Service.Interface;
using Fanex.NAP.Core.Web.Auth;
using Fanex.NAP.Core.Web.Session;
using System.Collections.Generic;

namespace Fanex.NAP.Hub.Areas.UserLogin.Controllers
{
    public class SSOController : BaseController
    {
        private IUserService _userService;
        private IMenuItemService _menuItemService;
        private ICurrentSession _currentSession;

        public SSOController()
            : this(null, null, null)
        {
        }

        public SSOController(IUserService userService, IMenuItemService menuItemService,
            ICurrentSession currentSession)
        {
            _userService = userService ?? new UserService();
            _menuItemService = menuItemService ?? new MenuItemService();
            _currentSession = currentSession ?? new CurrentSession();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult SSOIndex(string UserId, string OnUserId, string Time)
        {
            string strUserId = Cryptography.CfsDeCrypt(UserId.Replace(" ", "+"));
            string strOnUserId = Cryptography.CfsDeCrypt(OnUserId.Replace(" ", "+"));
            string strTime = Cryptography.CfsDeCrypt(Time.Replace(" ", "+"));

            DateTime time = Convert.ToDateTime(strTime);
            DateTime timeNow = DateTime.Now;
            TimeSpan expiration = timeNow - time;
            if (expiration.TotalSeconds <= 10)
            {
                int userId = Convert.ToInt32(strUserId);
                int onUserId = Convert.ToInt32(strOnUserId);
                UserSSOCheckInUserCriteria criteria = new UserSSOCheckInUserCriteria();
                criteria.UserId = userId;
                criteria.SiteId = _siteId;
                criteria.CheckOnUserId = onUserId;
                criteria.OnUserId = 0;
                criteria.IP = _currentContext.IP;
                criteria.NetSessionId = Session.SessionID;

                _userService.UserSSOCheckIn(ref criteria);
                if (criteria.OnUserId != 0)
                {
                    User objUser = new User();
                    objUser = (User)_userService.GetById(userId);

                    _currentSession.UserId = objUser.Id;
                    _currentSession.Username = objUser.Username;
                    _currentSession.OnUserId = criteria.OnUserId;
                    _currentSession.NetSessionId = Session.SessionID.ToString();
                    _currentSession.Level = _userService.GetUserSiteLevel(objUser.Id, this._siteId);

                    if (this._siteId != 0)
                    {
                        CreateUserMenu(objUser);
                    }

                    CreateAuthInfo(objUser);
                    return Redirect(FormsAuthentication.DefaultUrl);
                }
            }
            else
            {
                RedirectToAction("Index", "Home");
            }
            return View();
        }

        private void CreateUserMenu(User user)
        {
            _currentSession.MenuItemList = (IList<MenuItem>)_menuItemService.GetByUserAndSiteIncludeGroup(user.Id, _siteId,
                _environment, true);
        }

        private void CreateAuthInfo(User user)
        {
            var authInfo = new AuthInfo();
            authInfo.UserId = user.Id;
            authInfo.OnUserId = this._currentSession.OnUserId;

            var authTicket = new FormsAuthenticationTicket(1, user.Username,
                DateTime.Now, DateTime.Now.AddMinutes(20000), false, authInfo.ToXml());
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            Response.Cookies.Add(authCookie);
        }

        public ActionResult LogOut()
        {
            LogOutUserCriteria criteria = new LogOutUserCriteria();
            criteria.OnUserId = _currentSession.OnUserId;
            _userService.UserLogOut(criteria);
            _currentSession.ClearAll();
            return RedirectToAction("Index", "~/Home");
        }
    }
}