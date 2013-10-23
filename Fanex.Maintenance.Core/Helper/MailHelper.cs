// --------------------------------------------------------------------------
// <copyright file="MailHelper.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.24">Created</change>
// </history>

namespace Fanex.Maintenance.Core.Helper
{
    using System;
    using Fanex.Logging;
    using Fanex.Maintenance.Core.Configurations;
    
    /// <summary>
    /// Mail helper.
    /// </summary>
    public class MailHelper
    {
        /// <summary>
        /// Send mail with list mail in configuration file.
        /// </summary>
        /// <param name="mailSubject">Mail subject.</param>
        /// <param name="mailTitle">Mail title.</param>
        /// <param name="mailBody">Mail body.</param>
        public static void SendMail(string mailSubject = "", string mailTitle = "", string mailBody = "")
        {
            try
            {
                foreach (MailElement mail in MailSection.Instance.Mails)
                {
                    if (!string.IsNullOrEmpty(mail.To))
                    {
                        using (var client = new Fanex.MailDeliver.MailClient.API.MailDeliver())
                        {
                            client.CreateMail(mailTitle)
                                .AddBody(mailBody)
                                .AddSubject(mailSubject)
                                .To(mail.To)
                                .ToCcMails(mail.CC)
                                .ToBccMails(mail.BCC)
                                .SendMail();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.ToString(), ex);
            }
        }
    }
}
