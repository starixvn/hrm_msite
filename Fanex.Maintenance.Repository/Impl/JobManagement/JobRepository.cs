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
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class JobRepository : BaseRepository<Job>, IJobRepository
    {
        public IEnumerable<JobSchedule> GetAllJob(string serverName, string jobName, int? enabled, int? currentExecutionnStaus)
        {
            try
            {
                var criteria = new GetListJobCriteria
                {
                    Enabled = enabled,
                    Execution_Status = currentExecutionnStaus
                };

                if (!jobName.Equals(string.Empty))
                {
                    criteria.Job_Name = jobName;
                }

                //// new dbsetting from server name and setting key
                DbSetting setting = new DbSetting
                {
                    //// get connection string from servername
                    ConnectionString = SectionHelper.GetConnectionStringFromServerName(serverName),
                    SpName = SectionHelper.GetSpNameFromSpKey(criteria.GetSettingKey())
                };

                var list = new List<JobSchedule>();

                using (var db = new ObjectDb(setting))
                {
                    using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                    {
                        while (reader.Read())
                        {
                            list.Add(new JobSchedule
                            {
                                ExecuteStatus = reader.ToInt("current_execution_status"),
                                LastRunDate = reader.ToString("last_run_date").Length == 8 ? DateTime.ParseExact(reader.ToString("last_run_date"), "yyyyMMdd"
                                        , CultureInfo.InvariantCulture
                                        , DateTimeStyles.None) : DateTime.MinValue,
                                NextRunDate = reader.ToString("next_run_date").Length == 8 ? DateTime.ParseExact(reader.ToString("next_run_date"), "yyyyMMdd"
                                        , CultureInfo.InvariantCulture
                                        , DateTimeStyles.None) : DateTime.MinValue,
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
                                }
                            });
                        }
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
