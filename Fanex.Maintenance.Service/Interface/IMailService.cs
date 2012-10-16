// --------------------------------------------------------------------------
// <copyright file="IMailService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.20">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Interface
{
    using System.Collections.Generic;
    using Fanex.MailDeliver.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        /// Get list config
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        List<MailConfig> GetListConfig(string ip = "");

        /// <summary>
        /// Filter by appication id
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        IMailService WithApp(int appId);

        /// <summary>
        /// Filter by status
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        IMailService WithStatus(int status);

        /// <summary>
        /// Filter by from date
        /// </summary>
        /// <param name="byDate"></param>
        /// <returns></returns>
        IMailService FromDate(string byDate);

        /// <summary>
        /// Filter by to date
        /// </summary>
        /// <param name="toDate"></param>
        /// <returns></returns>
        IMailService ToDate(string toDate);

        /// <summary>
        /// Pagination
        /// </summary>
        /// <param name="page"></param>
        /// <param name="counter"></param>
        /// <returns></returns>
        IMailService WithPagination(int page, int counter);

        /// <summary>
        /// Get list mail
        /// </summary>
        /// <returns></returns>
        List<MailRecord> GetListMail();

        /// <summary>
        /// Get list mail by date
        /// </summary>
        /// <returns></returns>
        List<MailRecord> GetListMailByDate();

        /// <summary>
        /// Get mail by mail id
        /// </summary>
        /// <param name="mailId"></param>
        /// <returns></returns>
        MailRecord GetMail(int mailId);

        /// <summary>
        /// Get setting by setting id
        /// </summary>
        /// <param name="settingId"></param>
        /// <returns></returns>
        MailSetting GetSetting(int settingId);

        /// <summary>
        /// Get config by config id
        /// </summary>
        /// <param name="configId"></param>
        /// <returns></returns>
        MailConfig GetConfig(int configId);

        /// <summary>
        /// Get function by function id
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns></returns>
        MailFunction GetFunction(int functionId);

        /// <summary>
        /// Get list setting
        /// </summary>
        /// <returns></returns>
        List<MailSetting> GetListSetting();

        /// <summary>
        /// Get list function
        /// </summary>
        /// <returns></returns>
        List<MailFunction> GetListFunction();

        /// <summary>
        /// Send or resend mail
        /// </summary>
        /// <param name="mailID"></param>
        /// <returns></returns>
        bool SendMail(long mailID);

        /// <summary>
        /// Create new setting
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        bool StoreSetting(MailSetting setting);

        /// <summary>
        /// Create new config
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        bool StoreConfig(MailConfig config);

        /// <summary>
        /// Create new function
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        bool StoreFunction(MailFunction func);
    }
}