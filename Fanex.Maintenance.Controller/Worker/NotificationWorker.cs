// --------------------------------------------------------------------------
// <copyright file="NotificationWorker.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.17">Created</change>
// </history>

namespace Fanex.Maintenance.Controller.Worker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using Fanex.Maintenance.Service.Interface.JobManagement;
    using Fanex.Maintenance.Service.Impl.JobManagement;
    using Fanex.Logging;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Core.Web;
    using Fanex.Maintenance.Core.Configurations;
    using Fanex.Maintenance.Core.Helper;
    using SignalR;
    using Fanex.Maintenance.Controller.Hubs;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class NotificationWorker : BaseWorker
    {
        private int _getDataInterval;
        public static DateTime LastSqlAgentLogNotificationTime { get; set; }
        public static DateTime LastJobLogNotificationTime { get; set; }
        private IJobService _jobService;
        private ISqlServerLogService _sqlServerLogService;
        private string _sqlAgentErrorMailFormat = " <table cellspacing=\"0\" border=\"1\" style=\"border-color:DodgerBlue;border-style:Solid;border-collapse:collapse;font-size:8pt;font-family:Tahoma,Arial,Verdana,Sans-Serif\" rules=\"all\"> "
               + " <thead>"
                 + "   <tr align=\"center\" valign=\"Middle\" style=\"background-color:#deedef;min-height:25px;font-weight:bold\">"
                    + "    <th  width=\"20\">"
                   + "         #"
                   + "     </th>"
                   + "     <th  width=\"80\">"
                   + "         Server"
                    + "    </th>"
                   + "     <th  width=\"120\">"
                    + "        Occured Date"
                   + "     </th>"
                   + "     <th>"
                   + "         Message"
                   + "     </th>"
                  + "  </tr>"
               + " </thead>"
              + "  <tbody> {0}"
              + "  </tbody>"
           + " </table>";
        private string _jobErrorMailFormat = " <table cellspacing=\"0\" border=\"1\" style=\"border-color:DodgerBlue;border-style:Solid;border-collapse:collapse;font-size:8pt;font-family:Tahoma,Arial,Verdana,Sans-Serif\" rules=\"all\"> "
               + " <thead>"
                 + "   <tr align=\"center\" valign=\"Middle\" style=\"background-color:#deedef;min-height:25px;font-weight:bold\">"
                    + "    <th width=\"20\">"
                   + "         #"
                   + "     </th>"
                     + "    <th width=\"80\">"
                   + "         Server"
                   + "     </th>"
                   + "     <th width=\"80\">"
                   + "         Job Name"
                    + "    </th>"
                   + "     <th width=\"80\">"
                    + "        Failed at Step"
                   + "     </th>"
                     + "     <th width=\"80\">"
                    + "         Occured Date"
                   + "     </th>"
                   + "     <th>"
                   + "         Message"
                   + "     </th>"
                  + "  </tr>"
               + " </thead>"
              + "  <tbody> {0}"
              + "  </tbody>"
           + " </table>";
        private string _rowOddStyle = "align=\"center\" valign=\"Middle\" style=\"background-color:#fcfcfc;min-height:25px\"";
        private string _rowEvenStyle = "align=\"center\" valign=\"Middle\" style=\"background-color:#f2f2f2;min-height:25px\"";


        public NotificationWorker(
            int getDataInterval = 60000)
            : base()
        {
            _getDataInterval = getDataInterval;
            _jobService = JobService.Create();
            _sqlServerLogService = SqlServerLogService.Create();
            LastSqlAgentLogNotificationTime = DateTime.Now;
            LastJobLogNotificationTime = DateTime.Now;
        }

        /// <summary>
        /// Run Untill stop thread
        /// </summary>
        protected override void Run()
        {
            while (true)
            {
                Thread.Sleep(_getDataInterval);

                if (IsStop)
                {
                    break;
                }

                try
                {
                    List<NotifyInfo> notifyInfoes = new List<NotifyInfo>();
                    try
                    {
                        notifyInfoes.AddRange(GetSqlAgentLogs());
                        notifyInfoes.AddRange(GetJobLogs());
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error(ex.Message, ex);
                    }

                    var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                    context.Clients.gotMessage(notifyInfoes);
                }
                catch (Exception ex)
                {
                    Logger.Log.Error(ex.Message, ex);
                }
            }
        }


        /// <summary>
        /// Get Sql Agent Log For notifications
        /// </summary>
        /// <returns></returns>
        private IEnumerable<NotifyInfo> GetSqlAgentLogs()
        {
            //// Get all agent log
            IEnumerable<SqlServerLog> sqlServerLogs = _sqlServerLogService
                .FromDate(LastSqlAgentLogNotificationTime)
                .ToDate(DateTime.Now.AddDays(1))
                .GetSqlServerLogs((int)LogType.SqlAgentLog);
            List<NotifyInfo> notifyInformaitons = new List<NotifyInfo>();
            DateTime tempDatetime = DateTime.Now;

            foreach (ServerElement server in DatabaseServersSection.Instance.Servers)
            {
                foreach (SqlServerLog sqlServerLog in sqlServerLogs)
                {
                    //// if satisfy filter condition, then break to go to next server
                    if (sqlServerLog.LogDate > LastSqlAgentLogNotificationTime
                        && sqlServerLog.ErrorLevel == (int)ErrorType.Error
                        && sqlServerLog.ServerIp == server.ServerIp)
                    {
                        notifyInformaitons.Add(new NotifyInfo
                        {
                            Type = NotifyType.AgentLog,
                            ServerIp = server.ServerIp.Replace(".", "_"),
                            OccurTime = sqlServerLog.LogDate.ToString("MMMM dd, yyyy HH:mm:ss"),
                            Message = sqlServerLog.LogMessage
                        });

                        //// Send Mail
                        SendMailForSqlAgentError(sqlServerLogs, server.ServerIp);

                        break;
                    }
                }
            }

            //// Update Last run time
            LastSqlAgentLogNotificationTime = tempDatetime;

            return notifyInformaitons;
        }

        /// <summary>
        /// Get Job For notification
        /// </summary>
        /// <returns></returns>
        private IEnumerable<NotifyInfo> GetJobLogs()
        {
            //// Initial variables
            IEnumerable<JobLog> jobLogs = null;
            List<NotifyInfo> notifyInformaitons = new List<NotifyInfo>();
            DateTime tempDatetime = DateTime.Now;
            bool isBreak = false;
            foreach (ServerElement server in DatabaseServersSection.Instance.Servers)
            {
                _jobService = JobService.Create();
                IEnumerable<JobSchedule> jobs = _jobService
                    .FromServer(server.ServerIp)
                    .GetJobs();

                _jobService
                    .WithStartRunDate(LastJobLogNotificationTime)
                    .WithEndRunDate(DateTime.Now.AddDays(1));
                foreach (JobSchedule job in jobs)
                {

                    //// Get All job Log with daterange and job name
                    jobLogs = _jobService
                        .WithJob(job.Job.Name)
                        .GetJobLogs();

                    if (jobLogs != null)
                    {
                        foreach (JobLog jobLog in jobLogs)
                        {

                            //// Get specific job satisfy filter contions, if yes then break out
                            DateTime jobLogDate = DateTimeHelper.GetFullDatetime(jobLog.RunDate, jobLog.RunTime);
                            if (jobLogDate > LastJobLogNotificationTime
                                && (Constants.NOTIFY_WHEN_JOB_STATUS.Contains(jobLog.RunSatus))
                                && SectionHelper.GetServerIpFromServerName(jobLog.Server) == server.ServerIp)
                            {
                                notifyInformaitons.Add(new NotifyInfo
                                {
                                    Type = NotifyType.Job,
                                    ServerIp = server.ServerIp.Replace(".", "_"),
                                    OccurTime = jobLogDate.ToString("MMMM dd, yyyy HH:mm:ss"),
                                    Message = "Job " + job.Job.Name + " has error: " + jobLog.Message,
                                    JobName = job.Job.Name
                                });

                                //// Send mail
                                SendMailForJobStepError(job, jobLogs, server.ServerIp);


                                isBreak = true;
                                break;
                            }
                        }
                    }

                    //// Get out if break
                    if (isBreak)
                    {
                        isBreak = false;
                        break;
                    }
                }
            }

            //// Update Last run time
            LastJobLogNotificationTime = DateTime.Now;

            return notifyInformaitons;
        }

        /// <summary>
        /// Send mail for Sql Agent Error
        /// </summary>
        /// <param name="sqlServerLogs"></param>
        /// <param name="serverIp"></param>
        private void SendMailForSqlAgentError(IEnumerable<SqlServerLog> sqlServerLogs, string serverIp)
        {
            string mailTitle = "Sql Agent Error -" + DateTime.Now.ToString();
            string mailSubject = "Sql Agent Error -" + DateTime.Now.ToString();
            string mailHeader = _sqlAgentErrorMailFormat;
            string mailBody = string.Empty;
            int index = 1;
            foreach (SqlServerLog sqlServerLog in sqlServerLogs)
            {
               
                //// if satisfy filter condition
                if (sqlServerLog.LogDate > LastSqlAgentLogNotificationTime
                    && sqlServerLog.ErrorLevel == (int)ErrorType.Error
                    && sqlServerLog.ServerIp == serverIp)
                {

                    mailBody += "<tr " + ((index % 2 == 0) ? _rowOddStyle : _rowEvenStyle) + ">"
                        + "<td>" + index.ToString() + "</td>"
                        +"<td>" + serverIp + "</td>"
                        + "<td>" + sqlServerLog.LogDate + "</td>"
                        + "<td align=\"left\">" + sqlServerLog.LogMessage + "</td></tr>";
                    index++;
                }
            }

            if (mailBody != string.Empty)
            {
                MailHelper.SendMail(mailSubject, mailTitle, string.Format(mailHeader, mailBody));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="job"></param>
        /// <param name="jobLogs"></param>
        /// <param name="serverIp"></param>
        private void SendMailForJobStepError(JobSchedule job, IEnumerable<JobLog> jobLogs, string serverIp)
        {
            string mailTitle = "Job Step Error -" + DateTime.Now.ToString();
            string mailSubject = "Job Step Error -" + DateTime.Now.ToString();
            string mailHeader = _jobErrorMailFormat;
            string mailBody = string.Empty;
            int index = 1;
            foreach (JobLog jobLog in jobLogs)
            {
                //// Get specific job satisfy filter contions, if yes then break out
                DateTime jobLogDate = DateTimeHelper.GetFullDatetime(jobLog.RunDate, jobLog.RunTime);
                if (jobLogDate > LastJobLogNotificationTime
                    && (Constants.NOTIFY_WHEN_JOB_STATUS.Contains(jobLog.RunSatus))
                    && SectionHelper.GetServerIpFromServerName(jobLog.Server) == serverIp)
                {
                    mailBody += "<tr " + ((index % 2 == 0) ? _rowOddStyle : _rowEvenStyle) + ">"
                        + "<td>" + index.ToString() + "</td>" +
                        "<td align=\"center\">" + serverIp + "</td>"
                        + "<td align=\"center\">" + job.Job.Name + "</td>"
                        + "<td align=\"center\">" + jobLog.Step.Name + "</td>"
                       + "<td align=\"center\">" + jobLogDate + "</td>"
                       + "<td align=\"left\">" + jobLog.Message + "</td></tr>";
                    index++;
                }
            }

            if (mailBody != string.Empty)
            {
                MailHelper.SendMail(mailSubject, mailTitle, string.Format(mailHeader, mailBody));
            }
        }


    }
}
