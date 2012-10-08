// --------------------------------------------------------------------------
// <copyright file="JobService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.8">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Impl
{
    using System;
    using System.Collections.Generic;
    using Fanex.Maintenance.Service.Interface;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Repository.Interface;
    using Fanex.Maintenance.Repository.Impl;
    using Fanex.Maintenance.Core.Configurations;

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
        private bool? _status;
        private string _server;
        private string _schedule;
        private string _jobName;
        private int? _currentExcuteStatus;

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

        public IJobService WithStatus(bool status)
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

        public IJobService WithCurrentExecuteStatus(int executeStatus)
        {
            this._currentExcuteStatus = executeStatus;
            return this;
        }

        public IJobService WithLastRunDate(DateTime lastRunDate)
        {
            this._lastRunDate = lastRunDate;
            return this;
        }

        public IJobService WithNextRunDate(DateTime nextRunDate)
        {
            this._nextRunDate = nextRunDate;
            return this;
        }

        public IEnumerable<JobSchedule> GetJobs()
        {
            IEnumerable<JobSchedule> listJob;
            if (!this._server.Equals(string.Empty))
            {
                listJob = _jobRepository.GetAllJob(this._server, this._schedule);
            }
            else
            {
                foreach (var server in DatabaseServersSection.Instance.Servers)
                {

                }
            }

            return null;
        }
    }
}
