// --------------------------------------------------------------------------
// <copyright file="ISchedules.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.5">Created</change>
// </history>

namespace Fanex.Maintenance.Entity.Domain
{
    using System;

    public interface Schedule : IEntity<int, string>
    {
        int ScheduleUId { get; set; }

        OrinatingServer OrinatingServer { get; set; }

        byte[] OwnerSid { get; set; }

        int Enable { get; set; }

        /// <summary>
        /// How frequently a job runs for this schedule.
        ///1 = One time only
        ///4 = Daily
        ///8 = Weekly
        ///16 = Monthly
        ///32 = Monthly, relative to freq_interval
        ///64 = Runs when the SQL Server Agent service starts
        ///128 = Runs when the computer is idle
        /// </summary>
        int FregType { get; set; }

        /// <summary>
        /// Days that the job is executed. Depends on the value of freq_type. 
        /// The default value is 0, which indicates that freq_interval is unused.
        /// Value of freq_type              Effect on freq_interval
        /// 
        /// 1 (once)                        freq_interval is unused (0)
        /// 
        /// 4 (daily)                       Every freq_interval days
        /// 
        /// 8 (weekly)                      freq_interval is one or more of the following:
        ///                                 1 = Sunday
        ///                                 2 = Monday
        ///                                 4 = Tuesday
        ///                                 8 = Wednesday
        ///                                 16 = Thursday
        ///                                 32 = Friday
        ///                                 64 = Saturday     
        ///                                 
        /// 16 (monthly)                    On the freq_interval day of the month   
        /// 
        /// 32 (monthly, relative)          freq_interval is one of the following:
        ///                                 1 = Sunday
        ///                                 2 = Monday
        ///                                 3 = Tuesday
        ///                                 4 = Wednesday
        ///                                 5 = Thursday
        ///                                 6 = Friday
        ///                                 7 = Saturday
        ///                                 8 = Day
        ///                                 9 = Weekday
        ///                                 10 = Weekend day
        ///                                 
        /// 64 (starts when SQL Server Agent service starts) freq_interval is unused (0)                                 
        /// 128 (runs when computer is idle) freq_interval is unused (0)                               
        /// </summary>
        int FregInterval { get; set; }

        /// <summary>
        /// Units for the freq_subday_interval. Can be one of the following values:
        /// 1 At the specified time
        /// 2 Seconds
        /// 4 Minutes
        /// 8 Hours
        /// </summary>
        int FregSubdayType { get; set; }

        /// <summary>
        /// Number of freq_subday_type periods to occur between each execution of the job.
        /// </summary>
        int FregSubdayInterval { get; set; }

        /// <summary>
        /// When freq_interval occurs in each month, if freq_interval is 32 (monthly relative).
        /// Can be one of the following values:
        ///  0 = freq_relative_interval is unused
        ///1 = First
        ///2 = Second
        ///4 = Third
        ///8 = Fourth
        ///16 = Last
        /// </summary>
        int FregRelativeInterval { get; set; }

        /// <summary>
        /// Number of weeks or months between the scheduled execution of a job. 
        /// freq_recurrence_factor is used only if freq_type is 8, 16, or 32. 
        /// If this column contains 0, freq_recurrence_factor is unused.
        /// </summary>
        int FregRecurrenceFactor { get; set; }

        /// <summary>
        /// Date on which execution of a job can begin. 
        /// The date is formatted as YYYYMMDD. NULL indicates today's date.
        /// </summary>
        int ActiveStartDate { get; set; }

        /// <summary>
        /// Date on which execution of a job can stop. 
        /// The date is formatted YYYYMMDD.
        /// </summary>
        int ActiveEndDate { get; set; }

        /// <summary>
        /// Time on any day between active_start_date and active_end_date that job begins executing. 
        /// Time is formatted HHMMSS, using a 24-hour clock.
        /// </summary>
        int ActiveStartTime { get; set; }

        /// <summary>
        /// Time on any day between active_start_date and active_end_date that job stops executing. 
        /// Time is formatted HHMMSS, using a 24-hour clock.
        /// </summary>
        int ActiveEndTime { get; set; }

        DateTime DateCreated { get; set; }

        DateTime ModifedCreated { get; set; }

        /// <summary>
        /// Current version number of the schedule. For example,
        /// if a schedule has been modified 10 times, the version_number is 10.
        /// </summary>
        int VersionNumber { get; set; }
    }
}
