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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Maintenance.Entity.Domain;
    using Fanex.Maintenance.Repository.Interface.JobManagement;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Data;
    using Fanex.Maintenance.Core.Helper;
    using Fanex.Maintenance.Criteria.Job;
    using Fanex.Common.Extensions;
    using System.Globalization;
    using System.Data;
    using Fanex.NAP.Framework.Data;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class JobRepository : BaseRepository<Job>, IJobRepository
    {
        public IEnumerable<JobSchedule> GetAllJob(ICriteria criteria, string serverIp)
        {
            try
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

                    return Fetch<JobSchedule>(criteria, serverIp, objMapFunc);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


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
                            } : new Job(),
                            Step = !reader.IsDBNull("step_id") ? new Step
                            {
                                Id = reader.ToInt("step_id"),
                                Name = reader.ToString("step_name")
                            } : new Step(),
                            SqlMessageId = reader.ToInt("sql_message_id"),
                            SqlSeverity = reader.ToInt("sql_severity"),
                            Message = reader.ToString("message"),
                            RunSatus = reader.ToInt("run_status"),
                            RunDate = reader.ToInt("run_date"),
                            RunTime = reader.ToInt("run_time"),
                            RunDuration = reader.ToInt("run_duration"),
                            OperatorIdEmailed = !reader.IsDBNull("operator_emailed") ?
                                                    reader.ToInt("operator_emailed") : 0,
                            OperatorIdNetsend = !reader.IsDBNull("operator_netsent") ?
                                                    reader.ToInt("operator_netsent") : 0,
                            OperatorIdPaged = !reader.IsDBNull("operator_paged") ?
                                                    reader.ToInt("operator_paged") : 0,
                            RetriesAttempted = reader.ToInt("retries_attempted"),
                            Server = reader.ToString("server")
                        };
                    };

                    return Fetch<JobLog>(criteria, serverIp, objMapFunc);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
