// --------------------------------------------------------------------------
// <copyright file="JobRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.8">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Impl.JobManagement
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using Fanex.Common.Extensions;
    using Fanex.Maintenance.Entity.Domain;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Repository.Interface.JobManagement;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Job repository.
    /// </summary>
    public class JobRepository : BaseRepository<Job>, IJobRepository
    {
        /// <summary>
        /// Get all job.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="serverIp">Server IP.</param>
        /// <returns>List of job schedule.</returns>
        public IEnumerable<JobSchedule> GetAllJob(ICriteria criteria, string serverIp)
        {
            if (criteria.IsValid())
            {
                Func<IDataReader, JobSchedule> objMapFunc = (reader) =>
                {
                    return new JobSchedule
                        {
                            ExecuteStatus = reader.ToInt("current_execution_status"),
                            LastRunDate = reader.ToInt("last_run_date"),
                            LastRunTime = reader.ToInt("last_run_time"),
                            NextRunDate = reader.ToInt("next_run_date"),
                            NextRunTime = reader.ToInt("next_run_time"),
                            Job = new Job
                            {
                                Category = new JobCategory
                                {
                                    Name = reader.ToString("category")
                                },
                                DateCreated = reader.ToDateTime("date_created"),
                                DateModified = reader.ToDateTime("date_modified"),
                                DeleteLevel = reader.ToInt("delete_level"),
                                Description = reader.ToString("description"),
                                Enable = reader.ToInt("enabled"),
                                JobId = reader.To<Guid>("job_id"),
                                Name = reader.ToString("name"),
                                NotifyEmailOperator = new Operator
                                {
                                    Name = reader.ToString("notify_email_operator")
                                },
                                NotifyLevelEmail = reader.ToInt("notify_level_email"),
                                NotifyLevelEventLog = reader.ToInt("notify_level_eventlog"),
                                NotifyLevelNetsend = reader.ToInt("notify_level_netsend"),
                                NotifyLevelPage = reader.ToInt("notify_level_page"),
                                OrinatingServer = new OrinatingServer
                                {
                                    Name = reader.ToString("originating_server")
                                },
                                StartStepId = reader.ToInt("start_step_id"),
                                VersionNumber = reader.ToInt("version_number")
                            },
                            ServerIp = serverIp
                        };
                };

                return this.Fetch<JobSchedule>(criteria, serverIp, objMapFunc);
            }

            return null;
        }

        /// <summary>
        /// Get log of job.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="serverIp">Server IP.</param>
        /// <returns>List of job log.</returns>
        public IEnumerable<JobLog> GetJobLog(ICriteria criteria, string serverIp)
        {
            try
            {
                if (criteria.IsValid())
                {
                    Func<IDataReader, JobLog> objMapFunc = (reader) =>
                    {
                        return new JobLog
                        {
                            InstanceId = reader.ToInt("instance_id"),
                            Job = !reader.IsDBNull("job_id") ? new Job
                            {
                                JobId = reader.To<Guid>("job_id"),
                                Name = reader.ToString("job_name")
                            } 
                            : new Job(),
                            Step = !reader.IsDBNull("step_id") ? new Step
                            {
                                Id = reader.ToInt("step_id"),
                                Name = reader.ToString("step_name")
                            } 
                            : new Step(),
                            SqlMessageId = reader.ToInt("sql_message_id"),
                            SqlSeverity = reader.ToInt("sql_severity"),
                            Message = reader.ToString("message"),
                            RunSatus = reader.ToInt("run_status"),
                            RunDate = reader.ToInt("run_date"),
                            RunTime = reader.ToInt("run_time"),
                            RunDuration = reader.ToInt("run_duration"),
                            Server = reader.ToString("server")
                        };
                    };

                    return this.Fetch<JobLog>(criteria, serverIp, objMapFunc);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Start job idle.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="serverIp">Server IP address.</param>
        /// <returns>Command result.</returns>
        public Hashtable StartJob(ICriteria criteria, string serverIp)
        {
            if (criteria.IsValid())
            {
                return this.ExecuteCommand(criteria, serverIp);
            }

            return null;
        }

        /// <summary>
        /// Stop job executing.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="serverIp">Server IP address.</param>
        /// <returns>Command result.</returns>
        public Hashtable StopJob(ICriteria criteria, string serverIp)
        {
            if (criteria.IsValid())
            {
                return this.ExecuteCommand(criteria, serverIp);
            }

            return null;
        }
    }
}
