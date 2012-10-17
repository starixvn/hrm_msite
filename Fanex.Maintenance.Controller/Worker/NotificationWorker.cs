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
        private bool _isFristTime = true;


        public NotificationWorker(
            int getDataInterval = 60000)
            : base()
        {

            _getDataInterval = getDataInterval;
            _jobService = JobService.Create();
            _sqlServerLogService =  SqlServerLogService.Create();
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
                    notifyInfoes.AddRange(GetSqlAgentLogs());
                    //notifyInfoes.AddRange(GetJobLogs());

                    var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                    context.Clients.gotMessage(notifyInfoes);

                    if (_isFristTime)
                    {
                        _isFristTime = false;
                    }
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
            if (!_isFristTime)
            {
                _sqlServerLogService.FromDate(LastSqlAgentLogNotificationTime);
            }
            else
            {
                _sqlServerLogService.FromDate(DateTime.MinValue);
            }
            IEnumerable<SqlServerLog> sqlServerLogs = _sqlServerLogService.ToDate(DateTime.Now.AddDays(1))
                .GetSqlServerLogs((int)LogType.SqlAgentLog);
            List<NotifyInfo> notifyInformaitons = new List<NotifyInfo>();
            DateTime tempDatetime = DateTime.Now;
            foreach (ServerElement server in DatabaseServersSection.Instance.Servers)
            {
                foreach (SqlServerLog sqlServerLog in sqlServerLogs)
                {
                    if (sqlServerLog.LogDate > LastSqlAgentLogNotificationTime
                        && sqlServerLog.ErrorLevel == (int)ErrorType.Error
                        && sqlServerLog.ServerIp == server.ServerIp)
                    {
                        notifyInformaitons.Add(new NotifyInfo { 
                            Type = NotifyType.AgentLog,
                            ServerIp = server.ServerIp.Replace(".","_"),
                            OccurTime = sqlServerLog.LogDate,
                            Message = sqlServerLog.LogMessage
                        });
                        break;
                    }
                }
            }

            LastSqlAgentLogNotificationTime = tempDatetime;

            return notifyInformaitons;
        }

        /// <summary>
        /// Get Job For notification
        /// </summary>
        /// <returns></returns>
        private IEnumerable<NotifyInfo> GetJobLogs()
        {
            if (!_isFristTime)
            {
                _jobService.WithStartRunDate(LastJobLogNotificationTime);
            }
            else
            {
                _jobService.WithStartRunDate(DateTime.MinValue);
            }
            IEnumerable<JobLog> jobLogs = _jobService.WithEndRunDate(DateTime.Now.AddDays(1))
                .GetJobLogs();

            List<NotifyInfo> notifyInformaitons = new List<NotifyInfo>();
            DateTime tempDatetime = DateTime.Now;
            foreach (ServerElement server in DatabaseServersSection.Instance.Servers)
            {
                foreach (JobLog jobLog in jobLogs)
                {
                    DateTime jobLogDate = DateTimeHelper.GetFullDatetime(jobLog.RunDate, jobLog.RunTime);
                    if (jobLogDate > LastJobLogNotificationTime
                        && (jobLog.RunSatus == 0 || jobLog.RunSatus == 3) 
                        && jobLog.Server == server.ServerIp)
                    {
                        notifyInformaitons.Add(new NotifyInfo
                        {
                            Type = NotifyType.Job,
                            ServerIp = server.ServerIp,
                            OccurTime = jobLogDate,
                            Message = jobLog.Message
                        });
                        break;
                    }
                }
            }

            LastJobLogNotificationTime = DateTime.Now;

            return notifyInformaitons;
        }
    }
}
