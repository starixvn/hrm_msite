// --------------------------------------------------------------------------
// <copyright file="ScheduleRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.8">Created</change>
//   <change who="Ricky.Nguyen" date="2012.10.12">Update StyleCop</change>
// </history>

namespace Fanex.Maintenance.Repository.Impl.JobManagement
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Fanex.Common.Extensions;
    using Fanex.Logging;
    using Fanex.Maintenance.Core.Helper;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Repository.Interface.JobManagement;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ScheduleRepository : BaseRepository<ScheduleDetail>, IScheduleRepository
    {
        /// <summary>
        /// Get all schedule.
        /// </summary>
        /// <param name="scheduleCriteria">Schedule criteria.</param>
        /// <param name="serverIp">Server IP.</param>
        /// <returns></returns>
        public IEnumerable<ScheduleDetail> GetAllSchedule(ICriteria scheduleCriteria, string serverIp)
        {
            try
            {
                //// Create Function will map Data from DataReader to Schedule Object
                Func<IDataReader, ScheduleDetail> mapObjectFunc = (reader) =>
                    {
                        return new ScheduleDetail
                            {
                                Id = reader.ToInt("schedule_id"),
                                ScheduleUId = reader.ToString("schedule_uid"),
                                Name = reader.ToString("schedule_name"),
                                Enable = reader.ToInt("enabled"),
                                FreqType = reader.ToInt("freq_type"),
                                FreqInterval = reader.ToInt("freq_interval"),
                                FreqSubdayType = reader.ToInt("freq_subday_type"),
                                FreqSubdayInterval = reader.ToInt("freq_subday_interval"),
                                FreqRelativeInterval = reader.ToInt("freq_relative_interval"),
                                FreqRecurrenceFactor = reader.ToInt("freq_recurrence_factor"),
                                ActiveStartDate = DateTimeHelper.GetFullDate(reader.ToString("active_start_date")),
                                ActiveEndDate = DateTimeHelper.GetFullDate(reader.ToString("active_end_date")), 
                                ActiveStartTime = reader.ToInt("active_start_time"),
                                ActiveEndTime = reader.ToInt("active_end_time"),
                                DateCreated = reader.ToDateTime("date_created"),
                                JobCount = reader.ToInt("job_count")
                            };
                    };

                if (!string.IsNullOrEmpty(serverIp))
                {
                    IEnumerable<ScheduleDetail> schedules = 
                        Fetch<ScheduleDetail>(scheduleCriteria, serverIp, mapObjectFunc);

                    schedules.ForEach(schedule =>
                    {
                        schedule.ServerIp = serverIp;
                    });

                    return schedules;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }

            return new List<ScheduleDetail>();
        }
    }
}