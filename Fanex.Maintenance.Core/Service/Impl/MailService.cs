// --------------------------------------------------------------------------
// <copyright file="MailService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.20">Created</change>
// </history>

namespace Fanex.Maintenance.Core.Service.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Maintenance.Core.Service.Interface;
    using Fanex.MailDeliver.Model;
    using Fanex.MailDeliver.MailClient.API;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MailService : IMailService
    {
        #region Member

        private int _appId;
        private string _byDate;
        private string _toDate;
        private int _page;
        private int _counter;
        private MailStatus _mailStatus;

        #endregion

        #region Ctor

        private MailService()
        {
        }

        public static MailService Create()
        {
            return new MailService();
        }

        #endregion

        /// <summary>
        /// Get list mail config filter by server ip
        /// </summary>
        /// <param name="ip">if ip=null get all</param>
        /// <returns></returns>
        public List<MailConfig> GetListConfig(string ip = "")
        {
            var configs = new List<MailConfig>();
            using (var client = new MailDeliver())
            {
                var data = client.GetListConfig(ip);
                if (data != null)
                {
                    configs = data.ToList();
                }
            }

            return configs;
        }

        /// <summary>
        /// By applicationid
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public IMailService WithApp(int appId)
        {
            this._appId = appId;

            return this;
        }

        /// <summary>
        /// From date
        /// </summary>
        /// <param name="byDate"></param>
        /// <returns></returns>
        public IMailService FromDate(string byDate)
        {
            this._byDate = byDate;

            return this;
        }

        /// <summary>
        /// To date
        /// </summary>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public IMailService ToDate(string toDate)
        {
            this._toDate = toDate;

            return this;
        }

        /// <summary>
        /// Pagination
        /// </summary>
        /// <param name="page"></param>
        /// <param name="counter"></param>
        /// <returns></returns>
        public IMailService WithPagination(int page, int counter)
        {
            this._page = page;
            this._counter = counter;

            return this;
        }

        /// <summary>
        /// Get list mail
        /// </summary>
        /// <returns></returns>
        public List<MailRecord> GetListMail()
        {
            var mails = new List<MailRecord>();
            using (var client = new MailDeliver())
            {
                var dateTime = DateTime.Parse(_byDate);
                var data = client.GetListMail(_appId, dateTime, _page, _counter);
                if (data != null)
                {
                    mails = data.ToList();
                }
            }

            return mails;
        }

        /// <summary>
        /// Get list mail from date to date
        /// </summary>
        /// <returns></returns>
        public List<MailRecord> GetListMailByDate()
        {
            var mails = new List<MailRecord>();
            using (var client = new Fanex.MailDeliver.MailClient.API.MailDeliver())
            {
                var dateTime = DateTime.Parse(_byDate);
                var dateTimeTo = DateTime.Parse(_toDate);
                var data = client.GetListMailByDate(_appId, dateTime, dateTimeTo, _page, _counter, _mailStatus);
                if (data != null)
                {
                    mails = data.ToList();
                }
            }

            return mails;
        }

        /// <summary>
        /// Get detail mail by mailid
        /// </summary>
        /// <param name="mailID"></param>
        /// <returns></returns>
        public MailRecord GetMail(int mailID)
        {
            MailRecord mail = null;
            using (var client = new MailDeliver())
            {
                mail = client.GetMail(mailID);
            }

            return mail;
        }

        /// <summary>
        /// Get list mail setting
        /// </summary>
        /// <returns></returns>
        public List<MailSetting> GetListSetting()
        {
            var settings = new List<MailSetting>();
            using (var client = new MailDeliver())
            {
                var data = client.GetListSetting();
                if (data != null)
                {
                    settings = data.ToList();
                }
            }

            return settings;
        }

        /// <summary>
        /// Get list mail function
        /// </summary>
        /// <returns></returns>
        public List<MailFunction> GetListFunction()
        {
            var functions = new List<MailFunction>();
            using (var client = new MailDeliver())
            {
                var data = client.GetListFuntion();
                if (data != null)
                {
                    functions = data.ToList();
                }
            }

            return functions;
        }

        /// <summary>
        /// ReSend mail
        /// </summary>
        /// <param name="mailID"></param>
        /// <returns></returns>
        public bool SendMail(long mailID)
        {
            try
            {
                using (var client = new MailDeliver())
                {
                    client.ReSendMail(mailID);
                }

                return true;
            }
            catch
            {
            }

            return false;
        }

        /// <summary>
        /// Insert new mail setting
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public bool StoreSetting(MailSetting setting)
        {
            bool result = false;
            if (setting != null)
            {
                using (var client = new MailDeliver())
                {
                    client.StoreSetting(setting);
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Insert new mail config
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public bool StoreConfig(MailConfig config)
        {
            bool result = false;
            if (config != null)
            {
                using (var client = new MailDeliver())
                {
                    client.StoreConfig(config);
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Insert new mail function
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public bool StoreFunction(MailFunction func)
        {
            bool result = false;
            if (func != null)
            {
                using (var client = new MailDeliver())
                {
                    client.StoreFunction(func);
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Get mail setting by setting id
        /// </summary>
        /// <param name="settingId"></param>
        /// <returns></returns>
        public MailSetting GetSetting(int settingId)
        {
            MailSetting mail = null;
            using (var client = new MailDeliver())
            {
                mail = client.GetSetting(settingId);
            }

            return mail;
        }

        /// <summary>
        /// Get mail config by config id
        /// </summary>
        /// <param name="configId"></param>
        /// <returns></returns>
        public MailConfig GetConfig(int configId)
        {
            MailConfig mail = null;
            using (var client = new MailDeliver())
            {
                mail = client.GetConfig(configId);
            }

            return mail;
        }

        /// <summary>
        /// Get mail function by function id
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns></returns>
        public MailFunction GetFunction(int functionId)
        {
            MailFunction mail = null;
            using (var client = new MailDeliver())
            {
                mail = client.GetFunction(functionId);
            }

            return mail;
        }

        /// <summary>
        /// By Status mail
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public IMailService WithStatus(int status)
        {
            if (status == 0)
            {
                this._mailStatus = MailStatus.All;
            }

            if (status == -1)
            {
                this._mailStatus = MailStatus.Fail;
            }

            if (status == 1)
            {
                this._mailStatus = MailStatus.Success;
            }

            return this;
        }
    }
}
