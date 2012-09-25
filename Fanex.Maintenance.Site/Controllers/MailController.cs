// --------------------------------------------------------------------------
// <copyright file="MailController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.20">Created</change>
// </history>

namespace Fanex.Maintenance.Site.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Web.Mvc;
    using Fanex.MailDeliver.Model;
    using Fanex.Maintenance.Core.Model;
    using Fanex.Maintenance.Core.Service.Impl;
    using Fanex.Maintenance.Core.Service.Interface;

    [Authorize]
    public class MailController : Controller
    {
        #region Member

        private readonly IMailService _mailServer;

        #endregion

        #region Ctor

        public MailController()
            : this(null)
        {
        }

        public MailController(IMailService mailService)
        {
            this._mailServer = mailService ?? MailService.Create();
        }

        #endregion

        #region InitData

        private void InitPageSize()
        {
            List<SelectListItem> listPageSize = new List<SelectListItem>();
            listPageSize.Add(new SelectListItem
            {
                Text = "10",
                Value = "10"
            });

            listPageSize.Add(new SelectListItem
            {
                Text = "30",
                Value = "30"
            });

            listPageSize.Add(new SelectListItem
            {
                Text = "50",
                Value = "50"
            });

            listPageSize.Add(new SelectListItem
            {
                Text = "100",
                Value = "100"
            });

            ViewBag.PageSize = listPageSize;
        }

        /// <summary>
        /// Init list mail status for view
        /// </summary>
        private void InitMailStatus()
        {
            List<SelectListItem> listMailStatus = new List<SelectListItem>();
            listMailStatus.Add(new SelectListItem
            {
                Text = MailStatus.All.ToString(),
                Value = ((int)MailStatus.All).ToString()
            });

            listMailStatus.Add(new SelectListItem
            {
                Text = MailStatus.Fail.ToString(),
                Value = ((int)MailStatus.Fail).ToString()
            });

            listMailStatus.Add(new SelectListItem
            {
                Text = MailStatus.Success.ToString(),
                Value = ((int)MailStatus.Success).ToString()
            });

            ViewBag.MailStatus = listMailStatus;
        }

        /// <summary>
        /// Init list mail config for view
        /// </summary>
        private void InitDataMailConfig(int appId = 0)
        {
            List<MailConfig> listConfig = new List<MailConfig>();
            listConfig = _mailServer.GetListConfig(string.Empty);

            ViewBag.ListMailConfig = listConfig.Select(c => new SelectListItem
            {
                Text = c.AppName,
                Value = c.AppID.ToString(),
                Selected = c.AppID == appId
            }).ToList();
        }

        /// <summary>
        /// Init list mail setting for view
        /// </summary>
        private void InitDataMailSetting()
        {
            List<MailSetting> listMailSetting = new List<MailSetting>();
            listMailSetting = _mailServer.GetListSetting();
            ViewBag.ListMailSetting = listMailSetting.Select(s => new SelectListItem
            {
                Text = s.SettingName,
                Value = s.SettingID.ToString(),
            }).ToList();
        }

        /// <summary>
        /// Init list server ip for view
        /// </summary>
        private void InitDataIpServer(string serverIp = "")
        {
            List<MailSetting> listMailSetting = new List<MailSetting>();
            listMailSetting = _mailServer.GetListSetting();
            listMailSetting = listMailSetting.GroupBy(s => s.Server).Select(s => s.First()).ToList();

            ViewBag.ListServerIp = listMailSetting.Select(s => new SelectListItem
            {
                Text = s.Server,
                Value = s.Server,
                Selected = s.Server == serverIp
            }).ToList();
        }

        #endregion

        #region Action

        #region MailList

        /// <summary>
        /// Get mail list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MailList(int id = 0)
        {
            InitPageSize();

            InitDataMailConfig(id);

            InitMailStatus();

            ViewBag.DateFrom = DateTime.Now.ToString("dd/MM/yyyy");
            ViewBag.DateTo = DateTime.Now.ToString("dd/MM/yyyy");

            List<MailRecord> listMail = new List<MailRecord>();
            listMail = this._mailServer.WithApp(id)
                .FromDate(DateTime.Now.ToShortDateString())
                .ToDate(DateTime.Now.AddDays(1).ToShortDateString())
                .WithStatus(0)
                .WithPagination(1, 10)
                .GetListMailByDate();

            return View(listMail);
        }

        /// <summary>
        /// Get mail list
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="applicationID">if applicationid=0 get all</param>
        /// <param name="mailStatus"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MailList(string from, string to, int applicationID = 0, int mailStatus = 0, int size = 10)
        {
            InitPageSize();

            InitDataMailConfig();

            InitMailStatus();

            DateTime dateFrom = DateTime.ParseExact(from, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dateTo = DateTime.ParseExact(to, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(1);

            List<MailRecord> listMail = new List<MailRecord>();
            listMail = this._mailServer.WithApp(applicationID)
                .FromDate(dateFrom.ToString())
                .ToDate(dateTo.ToString())
                .WithStatus(mailStatus)
                .WithPagination(1, size)
                .GetListMailByDate();

            ViewBag.DateFrom = from;
            ViewBag.DateTo = to;

            return View(listMail);
        }

        #endregion

        #region MailSetting

        /// <summary>
        /// Get all mail setting
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MailSetting()
        {
            List<MailSetting> listMailSetting = new List<MailSetting>();
            listMailSetting = this._mailServer.GetListSetting();
            return View(listMailSetting);
        }

        /// <summary>
        /// Add mail setting
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddMailSetting()
        {
            return View(new MailSettingViewModel());
        }

        /// <summary>
        /// Add mail setting
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddMailSetting(MailSettingViewModel model)
        {
            ViewBag.Success = false;
            if (ModelState.IsValid)
            {
                model.Password = model.Password == null ? string.Empty : model.Password;
                model.SettingName = model.Server + ":" + model.Port + " (" + model.Username + ")";
                MailSetting mailSetting = new MailSetting();
                mailSetting = model.GetMailSetting();
                ViewBag.Success = this._mailServer.StoreSetting(mailSetting);
            }

            return View(model);
        }

        /// <summary>
        /// Add mail setting popup
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddMailSettingPopUp()
        {
            return View(new MailSetting());
        }

        /// <summary>
        /// Edit mail setting
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditMailSetting(int id)
        {
            MailSetting mailSetting = new MailSetting();
            mailSetting = _mailServer.GetSetting(id);
            MailSettingViewModel model = new MailSettingViewModel(mailSetting);

            return View(model);
        }

        /// <summary>
        /// Edit mail setting
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditMailSetting(MailSettingViewModel model)
        {
            ViewBag.Success = false;
            if (ModelState.IsValid)
            {
                model.Password = model.Password == null ? string.Empty : model.Password;
                model.SettingName = model.Server + ":" + model.Port + " (" + model.Username + ")";
                MailSetting mailSetting = new MailSetting();
                mailSetting = model.GetMailSetting();
                ViewBag.Success = _mailServer.StoreSetting(mailSetting);
            }

            return View(model);
        }

        #endregion

        #region MailConfig

        /// <summary>
        /// Get all mail config
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MailConfig(int id = 0)
        {
            var mailSetting = _mailServer.GetSetting(id);
            string serverIp = mailSetting != null ? mailSetting.Server : string.Empty;
            InitDataIpServer(serverIp);

            List<MailConfig> listMailConfig = new List<MailConfig>();
            listMailConfig = _mailServer.GetListConfig(serverIp);

            return View(listMailConfig);
        }

        /// <summary>
        /// Get mail config filter by serverip
        /// </summary>
        /// <param name="serverIp"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MailConfig(string serverIp = "")
        {
            InitDataIpServer(serverIp);

            List<MailConfig> listMailConfig = new List<MailConfig>();
            listMailConfig = this._mailServer.GetListConfig(serverIp);

            return View(listMailConfig);
        }

        /// <summary>
        /// Add new mail config
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddMailConfig()
        {
            InitDataMailSetting();

            return View(new MailConfigViewModel());
        }

        /// <summary>
        /// Add new mail config
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddMailConfig(MailConfigViewModel model)
        {
            InitDataMailSetting();

            ViewBag.Success = false;

            if (ModelState.IsValid)
            {
                MailConfig mailConfig = new MailConfig();
                mailConfig = model.GetMailConfig();
                ViewBag.Success = this._mailServer.StoreConfig(mailConfig);
            }

            return View(model);
        }

        /// <summary>
        /// Add new mail config popup
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddMailConfigPopUp()
        {
            InitDataMailSetting();

            return View(new MailConfig());
        }

        /// <summary>
        /// Edit mail config by config id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditMailConfig(int id)
        {
            InitDataMailSetting();

            ViewBag.Success = false;

            MailConfig mailConfig = new MailConfig();
            mailConfig = _mailServer.GetConfig(id);
            MailConfigViewModel model = new MailConfigViewModel(mailConfig);
            return View(model);
        }

        /// <summary>
        /// Edit mail config
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditMailConfig(MailConfigViewModel model)
        {
            InitDataMailSetting();

            ViewBag.Success = false;

            if (ModelState.IsValid)
            {
                MailConfig mailConfig = new MailConfig();
                mailConfig = model.GetMailConfig();
                ViewBag.Success = _mailServer.StoreConfig(mailConfig);
            }

            return View(model);
        }

        #endregion

        #region MailFunction

        /// <summary>
        /// Get all mail function
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MailFunction()
        {
            List<MailFunction> listMailFunction = new List<MailFunction>();
            listMailFunction = _mailServer.GetListFunction();

            return View(listMailFunction);
        }

        /// <summary>
        /// Add new mail function
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddMailFunction()
        {
            InitDataMailSetting();

            InitDataMailConfig();

            return View(new MailFunctionViewModel());
        }

        /// <summary>
        /// Add new mail function
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddMailFunction(MailFunctionViewModel model)
        {
            InitDataMailSetting();

            InitDataMailConfig();

            ViewBag.Success = false;

            if (ModelState.IsValid)
            {
                MailFunction mailFunction = new MailFunction();
                mailFunction = model.GetMailFunction();
                ViewBag.Success = _mailServer.StoreFunction(mailFunction);
            }

            return View(model);
        }

        /// <summary>
        /// Edit mail function
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditMailFunction(int id)
        {
            InitDataMailSetting();

            InitDataMailConfig();

            MailFunction mailFunction = new MailFunction();
            mailFunction = _mailServer.GetFunction(id);

            MailFunctionViewModel model = new MailFunctionViewModel(mailFunction);

            return View(model);
        }

        /// <summary>
        /// Edit mail function
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditMailFunction(MailFunctionViewModel model)
        {
            InitDataMailSetting();

            InitDataMailConfig();

            ViewBag.Success = false;

            if (ModelState.IsValid)
            {
                MailFunction mailFunction = new MailFunction();
                mailFunction = model.GetMailFunction();
                ViewBag.Success = _mailServer.StoreFunction(mailFunction);
            }

            return View(model);
        }

        #endregion

        #endregion

        #region Ajax

        /// <summary>
        /// Add mail setting for popup view
        /// </summary>
        /// <param name="server"></param>
        /// <param name="port"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="isAutho"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddMailSettingPopUp(string server, int port, string username, string password, bool isAutho)
        {
            if (Request.IsAjaxRequest())
            {
                MailSetting mail = new MailSetting
                {
                    SettingName = server + ":" + port.ToString() + " (" + username + ") ",
                    Server = server,
                    Port = port,
                    Username = username,
                    Password = password,
                    IsAuth = isAutho
                };

                bool check = _mailServer.StoreSetting(mail);
                return Json(mail);
            }

            return Json(false);
        }

        /// <summary>
        /// Add mail config for popup view
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="description"></param>
        /// <param name="settingId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddMailConfigPopUp(string appName, string description, int settingId)
        {
            MailConfig mailConfig = new MailConfig
            {
                AppName = appName,
                Description = description,
                Setting = new MailSetting
                {
                    SettingID = settingId
                }
            };

            InitDataMailSetting();

            ViewBag.Success = false;

            if (ModelState.IsValid)
            {
                ViewBag.Success = _mailServer.StoreConfig(mailConfig);
            }

            return Json(mailConfig);
        }

        /// <summary>
        /// Resend mail
        /// </summary>
        /// <param name="mailId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ResendMail(int mailId)
        {
            if (Request.IsAjaxRequest())
            {
                bool check = _mailServer.SendMail(mailId);
                return Json(check);
            }

            return Json(false);
        }

        /// <summary>
        /// Get message detail of mail
        /// </summary>
        /// <param name="mailId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMessage(int mailId)
        {
            if (Request.IsAjaxRequest())
            {
                MailRecord mail = _mailServer.GetMail(mailId);
                return View(mail);
            }

            return View(new MailRecord());
        }

        /// <summary>
        /// Get list mail pagination
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="applicationID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MailListPagi(string from, string to, int page, int size, int applicationID = 0)
        {
            InitDataMailConfig();

            DateTime dateFrom = DateTime.ParseExact(from, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dateTo = DateTime.ParseExact(to, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(1);

            List<MailRecord> listMail = new List<MailRecord>();
            listMail = _mailServer.WithApp(applicationID)
                .FromDate(dateFrom.ToString())
                .ToDate(dateTo.ToString())
                .WithPagination(page, size)
                .GetListMailByDate();

            return Json(listMail);
        }

        /// <summary>
        /// Get list mail setting
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMailSetting()
        {
            if (Request.IsAjaxRequest())
            {
                List<MailSetting> listMailSetting = new List<MailSetting>();
                listMailSetting = _mailServer.GetListSetting();

                return Json(listMailSetting);
            }

            return Json(null);
        }

        /// <summary>
        /// Get list mail config
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMailConfig()
        {
            if (Request.IsAjaxRequest())
            {
                List<MailConfig> listMailConfig = new List<MailConfig>();
                listMailConfig = _mailServer.GetListConfig();

                return Json(listMailConfig);
            }

            return Json(null);
        }
        #endregion
    }
}
