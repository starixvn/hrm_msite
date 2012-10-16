// --------------------------------------------------------------------------
// <copyright file="StepRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.9">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Impl.JobManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Maintenance.Repository.Interface.JobManagement;
    using Fanex.Maintenance.Criteria.Job;
    using Fanex.Maintenance.Core.Web;
    using System.Data;
    using Fanex.Common.Extensions;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Entity.Domain;
    using System.Globalization;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class StepRepository : BaseRepository<Step>, IStepRepository
    {
        /// <summary>
        /// Get step by job name
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public IEnumerable<StepDetail> GetStepByJob(ICriteria criteria, string serverName)
        {
            if (criteria.IsValid())
            {
                Func<IDataReader, StepDetail> objMapFunc = (reader) =>
                {
                    return new StepDetail
                    {
                        Id = reader.ToInt("step_id"),
                        Name = reader.ToString("step_name"),
                        SubSystem = reader.ToString("subsystem"),
                        Command = reader.ToString("command"),
                        Flags = reader.ToString("flags"),
                        CmdExecSuccessCode = reader.ToInt("cmdexec_success_code"),
                        OnSuccessAction = reader.ToString("on_success_action"),
                        OnSuccessStep = !reader.IsDBNull("on_success_step_id") ? new Step
                        {
                            Id = reader.ToInt("on_success_step_id")
                        } : new Step(),
                        OnFailAction = reader.ToString("on_fail_action"),
                        OnFailStep = !reader.IsDBNull("on_fail_step_id") ? new Step
                        {
                            Id = reader.ToInt("on_fail_step_id")
                        } : new Step(),
                        Server = reader.ToString("server"),
                        DatabaseName = reader.ToString("database_user_name"),
                        RetryAttempts = reader.ToInt("retry_attempts"),
                        RetryInterval = reader.ToInt("retry_interval"),
                        OsRunPriority = reader.ToString("os_run_priority"),
                        OutputFileName = reader.ToString("output_file_name"),
                        LastRunOutCome = reader.ToInt("last_run_outcome"),
                        LastRunDuration = reader.ToInt("last_run_duration"),
                        LastRunRetries = reader.ToInt("last_run_retries"),
                        LastRunDate = reader.ToInt("last_run_date"),
                        LastRunTime = reader.ToInt("last_run_time"),
                        ProxyId = reader.ToInt("proxy_id")
                    };
                };

                return this.Fetch<StepDetail>(criteria, serverName, objMapFunc);
            }

            return null;
        }
    }
}
