// --------------------------------------------------------------------------
// <copyright file="JobService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.8">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Impl.JobManagement
{
    using System;
    using System.Collections.Generic;
    using Fanex.Maintenance.Service.Interface.JobManagement;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Repository.Interface.JobManagement;
    using Fanex.Maintenance.Core.Configurations;
    using System.Linq;
    using Fanex.Maintenance.Repository.Impl.JobManagement;
    using Fanex.Maintenance.Criteria.Job;
    using Fanex.Common;
    using Fanex.Maintenance.Core.Web;
    using Fanex.Maintenance.Entity.Domain;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class JobService : IJobService
    {
        #region Member

        private readonly IJobRepository _jobRepository;

        #endregion

        #region Properties

        private DateTime? _lastRunDate;
        private DateTime? _nextRunDate;
        private int? _status;
        private string _server;
        private string _schedule;
        private string _jobName;
        private int? _currentExcuteStatus;
        private Guid _jobId;
        private int _startRunDate;
        private int _endRunDate;
        #endregion

        #region Ctor

        private JobService()
            : this(null)
        {
        }

        private JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository ?? new JobRepository();

            _lastRunDate = null;
            _nextRunDate = null;
            _status = null;
            _server = string.Empty;
            _schedule = string.Empty;
            _jobName = string.Empty;
            _currentExcuteStatus = null;
        }

        public static IJobService Create()
        {
            return new JobService();
        }

        #endregion

        public IEnumerable<JobSchedule> GetJob()
        {
            return null;
        }

        public IJobService WithStatus(int? status)
        {
            this._status = status;
            return this;
        }

        public IJobService FromServer(string serverName)
        {
            this._server = serverName;
            return this;
        }

        public IJobService WithJob(string jobName)
        {
            this._jobName = jobName;
            return this;
        }

        public IJobService WithSchedule(string scheduleName)
        {
            this._schedule = scheduleName;
            return this;
        }

        public IJobService WithCurrentExecuteStatus(int? executeStatus)
        {
            this._currentExcuteStatus = executeStatus;
            return this;
        }

        public IJobService WithLastRunDate(DateTime? lastRunDate)
        {
            this._lastRunDate = lastRunDate;
            return this;
        }

        public IJobService WithNextRunDate(DateTime? nextRunDate)
        {
            this._nextRunDate = nextRunDate;
            return this;
        }

        public IJobService WithStartRunDate(DateTime? startRunDate)
        {
            if (ReferenceEquals(startRunDate, null))
            {
                startRunDate = DateTime.Now;
            }

            string date = ((DateTime)startRunDate).ToString("yyyyMMdd");
            this._startRunDate = Convert.ToInt32(date);

            return this;
        }

        public IJobService WithEndRunDate(DateTime? endRunDate)
        {
            if (ReferenceEquals(endRunDate, null))
            {
                endRunDate = DateTime.Now;
            }

            string date = ((DateTime)endRunDate).ToString("yyyyMMdd");
            this._endRunDate = Convert.ToInt32(date);
            return this;
        }

        public IEnumerable<JobSchedule> GetJobs()
        {
            var criteria = new GetListJobCriteria
            {
                Job_Name = this._jobName,
                Execution_Status = this._currentExcuteStatus,
                Enabled = this._status
            };

            var listJob = new List<JobSchedule>();

            //// get job with current server ip
            if (!ReferenceEquals(this._server, null) && !this._server.Equals(string.Empty))
            {
                try
                {
                    var list = _jobRepository.GetAllJob(criteria, this._server);

                    listJob.AddRange(list);

                    //// check filter by schedule
                    if (!ReferenceEquals(this._schedule, null) && !this._schedule.Equals(string.Empty))
                    {
                        var listTemp = new List<JobSchedule>();
                        List<JobSchedule> listJobSchedule = GetJobBySchedule(this._schedule, this._server).ToList();

                        //// get job filter by schedule name
                        foreach (var item in listJobSchedule)
                        {
                            listTemp.AddRange(list.Where(j => j.Job.JobId == item.Job.JobId).ToList());
                        }
                        return listTemp;
                    }
                }
                catch (Exception ex)
                {

                }
            }
            //// get job all server
            else
            {
                foreach (ServerElement server in DatabaseServersSection.Instance.Servers)
                {
                    try
                    {
                        var list = _jobRepository.GetAllJob(criteria, server.ServerIp);
                        listJob.AddRange(list);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            return listJob;
        }


        public IEnumerable<JobLog> GetJobLogs()
        {
            //// create criteria get job history
            var criteria = new GetJobHistoryCriteria
            {
                Job_Name = this._jobName,
                Run_Status = this._currentExcuteStatus,
                Start_Run_Date = this._startRunDate,
                End_Run_Date = this._endRunDate,
                Mode = Constants.FULL
            };

            try
            {
                return _jobRepository.GetJobLog(criteria, this._server);
            }
            catch (Exception ex)
            {
            }

            return null;
        }

        private IEnumerable<JobSchedule> GetJobBySchedule(string schedule, string server)
        {
            try
            {
                var criteria = new GetAllJobInScheduleCriteria
                {
                    Schedule_Name = schedule
                };

                return _jobRepository.GetAllJob(criteria, server);
            }
            catch (Exception ex) 
            {            
            }
            return new List<JobSchedule>();
        }
    }
}
