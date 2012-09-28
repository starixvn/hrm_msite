using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Fanex.NAP.Core.Model.ValueObject;
using Fanex.NAP.Core.Service.Impl;
using Fanex.NAP.Core.Service.Interface;
using Fanex.Common;
using Fanex.NAP.Core.Web.Session;
using System.Web;
using Fanex.NAP.Core.Model;
using Fanex.NAP.Core.Business;

namespace Fanex.NAP.Hub.Areas.UserLogin.Controllers
{
    public class UserSiteController : BaseController
    {
        #region members

        private ISiteService _siteService = new SiteService();
        private ICurrentSession _currentSession = new CurrentSession();

        #endregion

        #region ctors

        public UserSiteController() : this(null, null) { }

        public UserSiteController(ISiteService siteService, ICurrentSession currentSession)
        {
            _siteService = _siteService ?? new SiteService();
            _currentSession = _currentSession ?? new CurrentSession();
        }

        #endregion

        #region actions

        public ActionResult Site()
        {
            if (HttpContext.User.Identity.IsAuthenticated
                && _currentSession.UserId > 0)
            {
                var listSite = _siteService.GetByUser(_currentSession.UserId);

                ViewBag.OnUserId = _currentSession.OnUserId;

                return View(listSite);
            }

            return RedirectToAction("logout", "login");
        }

        [HttpGet]
        public ActionResult GoToSite(string id)
        {
            string param = GenerateQueryParam(_currentSession.UserId, _currentSession.OnUserId);
            Site objSite = (Site)_siteService.GetById(int.Parse(id));

            if (this._environment.Equals(Constant.LIVE))
            {
                return new RedirectResult(objSite.Url + "/UserLogin/SSO/SSOIndex?" + param);
            }
            else
            {
                return new RedirectResult(objSite.TestUrl + "/UserLogin/SSO/SSOIndex?" + param);
            }
        }

        protected string GenerateQueryParam(int userId, int onUserId)
        {
            string timeSubmit = DateTime.Now.ToString("G");
            timeSubmit = Cryptography.CfsEnCrypt(timeSubmit);
            string strUserId = Cryptography.CfsEnCrypt(userId.ToString());
            string strOnUserId = Cryptography.CfsEnCrypt(onUserId.ToString());
            return "UserId=" + strUserId + "&OnUserId=" + strOnUserId + "&Time=" + timeSubmit;
        }

        #endregion
    }
}