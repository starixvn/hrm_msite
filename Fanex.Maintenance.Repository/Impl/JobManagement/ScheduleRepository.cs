// --------------------------------------------------------------------------
// <copyright file="ScheduleRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.8">Created</change>
// </history>


namespace Fanex.Maintenance.Repository.Impl.JobManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Maintenance.Entity.Domain;
    using Fanex.Maintenance.Repository.Interface.JobManagement;
    using Fanex.NAP.Framework.Data;
    using System.Data;
    using Fanex.Common.Extensions;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ScheduleRepository : BaseRepository<Schedule>, IScheduleRepository
    {
        /// <summary>
        /// Read Schedules from Database 
        /// </summary>
        /// <param name="scheduleCriteria"></param>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public IEnumerable<Schedule> GetAllSchedule(ICriteria scheduleCriteria, string serverName)
        {
            try
            {
                //// Create Function will map Data from DataReader to Schedule Object
                Func<IDataReader, Schedule> mapObjectFunc = (reader) =>
                    {
                        return new Schedule
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
                                ActiveStartDate = reader.ToInt("active_start_date"),
                                ActiveEndDate = reader.ToInt("active_end_date"),
                                ActiveStartTime = reader.ToInt("active_start_time"),
                                ActiveEndTime = reader.ToInt("active_end_time"),
                                DateCreated = reader.ToDateTime("date_created"),
                                JobCount = reader.ToInt("job_count")
                            };
                    };
                
                if(!string.IsNullOrEmpty(serverName))
                {
                    return Fetch<Schedule>(scheduleCriteria, serverName, mapObjectFunc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new List<Schedule>();
        }
    }
}
