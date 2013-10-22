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
    using System.Linq;
    using Fanex.Logging;
    using Fanex.Maintenance.Core.Configurations;
    using Fanex.Maintenance.Core.Web;
    using Fanex.Maintenance.Criteria.Job;
    using Fanex.Maintenance.Entity.Domain;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Repository.Impl.JobManagement;
    using Fanex.Maintenance.Repository.Interface.JobManagement;
    using Fanex.Maintenance.Service.Interface.JobManagement;

    /// <summary>
    /// Job service.
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

        private int? _scheduleId;

        private string _stepName;

        #endregion

        #region Ctor

        /// <summary>
        /// Constructor.
        /// </summary>
        private JobService()
            : this(null)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="jobRepository"></param>
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

        /// <summary>
        /// Create job service.
        /// </summary>
        /// <returns>Job service.</returns>
        public static IJobService Create()
        {
            return new JobService();
        }

        #endregion

        /// <summary>
        /// Get job.
        /// </summary>
        /// <returns>List of job schedule.</returns>
        public IEnumerable<JobSchedule> GetJob()
        {
            return null;
        }

        /// <summary>
        /// Filter by status of job.
        /// </summary>
        /// <param name="status"></param>
        /// <returns>Job service object.</returns>
        public IJobService WithStatus(int? status)
        {
            this._status = status;

            return this;
        }

        /// <summary>
        /// From server.
        /// </summary>
        /// <param name="serverName"></param>
        /// <returns>Job service object.</returns>
        public IJobService FromServer(string serverName)
        {
            this._server = serverName;

            return this;
        }

        /// <summary>
        /// Filter by job name.
        /// </summary>
        /// <param name="jobName"></param>
        /// <returns>Job service object.</returns>
        public IJobService WithJob(string jobName)
        {
            this._jobName = jobName;

            return this;
        }

        /// <summary>
        /// Filter by schedule name.
        /// </summary>
        /// <param name="scheduleName">Schedule name.</param>
        /// <returns>Job service object.</returns>
        public IJobService WithSchedule(string scheduleName)
        {
            this._schedule = scheduleName;

            return this;
        }

        /// <summary>
        /// Filter by current execution status.
        /// </summary>
        /// <param name="executeStatus"></param>
        /// <returns>Job service object.</returns>
        public IJobService WithCurrentExecuteStatus(int? executeStatus)
        {
            this._currentExcuteStatus = executeStatus;

            return this;
        }

        /// <summary>
        /// Filter by last run date.
        /// </summary>
        /// <param name="lastRunDate"></param>
        /// <returns></returns>
        public IJobService WithLastRunDate(DateTime? lastRunDate)
        {
            this._lastRunDate = lastRunDate;

            return this;
        }

        /// <summary>
        /// Filter buy next run date.
        /// </summary>
        /// <param name="nextRunDate"></param>
        /// <returns>Job service object.</returns>
        public IJobService WithNextRunDate(DateTime? nextRunDate)
        {
            this._nextRunDate = nextRunDate;

            return this;
        }

        /// <summary>
        /// Filter by start run date.
        /// </summary>
        /// <param name="startRunDate"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Filter by end run date.
        /// </summary>
        /// <param name="endRunDate"></param>
        /// <returns>Job service object.</returns>
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

        /// <summary>
        /// Filter by job ID.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>Job service object.</returns>
        public IJobService WithJobId(string guid)
        {
            this._jobId = new Guid(guid);

            return this;
        }

        /// <summary>
        /// Filter by step name.
        /// </summary>
        /// <param name="stepName"></param>
        /// <returns>Job service object.</returns>
        public IJobService WithStepName(string stepName)
        {
            if (!string.IsNullOrEmpty(stepName))
            {
                this._stepName = stepName;
            }

            return this;
        }

        /// <summary>
        /// Get jobs.
        /// </summary>
        /// <returns>List of job schedule.</returns>
        public IEnumerable<JobSchedule> GetJobs()
        {
            var criteria = new GetListJobCriteria
            {
                JobName = this._jobName,
                ExecutionStatus = this._currentExcuteStatus,
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
                    if (!ReferenceEquals(this._scheduleId, null))
                    {
                        var listTemp = new List<JobSchedule>();
                        List<JobSchedule> listJobSchedule = GetJobByScheduleId(this._scheduleId, this._server).ToList();

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
                    Logger.Log.Error(ex.Message, ex);
                }
            }
            else
            {
                //// get job all server
                foreach (ServerElement server in DatabaseServersSection.Instance.Servers)
                {
                    try
                    {
                        var list = _jobRepository.GetAllJob(criteria, server.ServerIp);
                        listJob.AddRange(list);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error(ex.Message, ex);
                    }
                }
            }

            return listJob;
        }

        /// <summary>
        /// Get job logs.
        /// </summary>
        /// <returns>List of job log.</returns>
        public IEnumerable<JobLog> GetJobLogs()
        {
            //// create criteria get job history
            var criteria = new GetJobHistoryCriteria
            {
                JobName = this._jobName,
                RunStatus = this._currentExcuteStatus,
                StartRunDate = this._startRunDate,
                EndRunDate = this._endRunDate,
                Mode = Constants.FULL
            };

            try
            {
                List<JobLog> list = _jobRepository.GetJobLog(criteria, this._server).ToList();

                if (!ReferenceEquals(list, null) && list.Count > 0)
                {
                    int indexOutCome = 0;
                    for (int i = 0; i < list.Count; i++)
                    {
                        var itemLog = list[i];
                        if (itemLog.Step.Id == 0)
                        {
                            indexOutCome = i;
                        }
                        else if (itemLog.RunSatus == 0 || itemLog.RunSatus == 2)
                        {
                            list[indexOutCome].RunSatus = 6;
                        }
                    }

                    if (indexOutCome == 0)
                    {
                        list.Insert(
                            0, 
                            new JobLog
                            {
                                Step = new Step
                                {
                                    Id = 0,
                                    Name = "(Job outcome)"
                                }
                            });
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }

            return null;
        }

        /// <summary>
        /// Start Job.
        /// </summary>
        /// <returns>Command result.</returns>
        public CommandResult StartJob()
        {
            StartJobCriteria startJobCriteria = new StartJobCriteria
            {
                JobName = this._jobName,
                StepName = this._stepName
            };

            CommandResult commandResult = new CommandResult
            {
                Code = 0,
                Message = "Start Job successfully!"
            };

            try
            {
                _jobRepository.StartJob(startJobCriteria, this._server);
            }
            catch (Exception ex)
            {
                commandResult.Code = 1000;
                commandResult.Message = ex.Message;
            }

            return commandResult;
        }

        /// <summary>
        /// Stop Job.
        /// </summary>
        /// <returns>Command result.</returns>
        public CommandResult StopJob()
        {
            StopJobCriteria stopJobCriteria = new StopJobCriteria
            {
                JobName = this._jobName
            };

            CommandResult commandResult = new CommandResult
            {
                Code = 0,
                Message = "Stop Job successfully!"
            };

            try
            {
                _jobRepository.StartJob(stopJobCriteria, this._server);
            }
            catch (Exception ex)
            {
                commandResult.Code = 1000;
                commandResult.Message = ex.Message;
            }

            return commandResult;
        }

        /// <summary>
        /// Stop Job.
        /// </summary>
        /// <param name="scheduleId">Schedule ID.</param>
        /// <returns>Job service.</returns>
        public IJobService WithScheduleId(int? scheduleId)
        {
            this._scheduleId = scheduleId;
            return this;
        }

        /// <summary>
        /// Get job buy schedule name.
        /// </summary>
        /// <param name="scheduleId">Schedule ID.</param>
        /// <param name="server">Server name.</param>
        /// <returns>List of job schedule.</returns>
        private IEnumerable<JobSchedule> GetJobByScheduleId(int? scheduleId, string server)
        {
            try
            {
                var criteria = new GetAllJobInScheduleCriteria
                {
                    ScheduleId = scheduleId
                };

                return _jobRepository.GetAllJob(criteria, server);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }

            return new List<JobSchedule>();
        }

        /// <summary>
        /// Get job buy schedule name.
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="server"></param>
        /// <returns>List of job schedule.</returns>
        private IEnumerable<JobSchedule> GetJobBySchedule(string schedule, string server)
        {
            try
            {
                var criteria = new GetAllJobInScheduleCriteria
                {
                    ScheduleName = schedule
                };

                return _jobRepository.GetAllJob(criteria, server);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }

            return new List<JobSchedule>();
        }
    }
}
