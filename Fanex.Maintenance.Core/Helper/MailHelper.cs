// -----------------------------------------------------------------------
// <copyright file="MailHelper.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Maintenance.Core.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Maintenance.Core.Configurations;
    using Fanex.Logging;
    

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MailHelper
    {
        /// <summary>
        /// Send mail with list mail in configuration file
        /// </summary>
        /// <param name="mailSubject"></param>
        /// <param name="mailTitle"></param>
        /// <param name="mailBody"></param>
        public static void SendMail(string mailSubject = "", 
            string mailTitle = "", 
            string mailBody = "")
        {
            try
            {
                foreach (MailElement mail in MailSection.Instance.Mails)
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
            catch (Exception ex)
            {
                Logger.Log.Error(ex.ToString(), ex);
            }
        }
    }
}
