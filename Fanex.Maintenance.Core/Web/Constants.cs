// --------------------------------------------------------------------------
// <copyright file="Constants.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.10">Created</change>
//   <change who="Ricky.Nguyen" date="2012.10.16">Stylecop</change>
// </history>

namespace Fanex.Maintenance.Core.Web
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;    
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public static class Constants
    {
        /// <summary>
        /// Job Constant
        /// </summary>
        public const string JOB = "JOB";

        /// <summary>
        /// Steps Constant
        /// </summary>
        public const string STEPS = "STEPS";

        /// <summary>
        /// SCEDULES
        /// </summary>
        public const string SCEDULES = "SCEDULES";

        /// <summary>
        /// Targets Constant
        /// </summary>
        public const string TARGETS = "TARGETS";

        /// <summary>
        /// Full Constant
        /// </summary>
        public const string FULL = "FULL";

        public static readonly List<int> NOTIFY_WHEN_JOB_STATUS = new List<int>
        {
            0,
            3
        };

        /// <summary>
        /// Freq Interval Values
        /// </summary>
        public static string[] FreqIntervalValues = new string[]
        {
            "Sunday",
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday",
            "Day",
            "Weekday",
            "Weekend day"
        };
    }

    /// <summary>
    /// Status job
    /// </summary>
    public enum Status
    {
        Enabled = 1,

        Disabled = 0
    }

    /// <summary>
    /// Current execution status of job
    /// </summary>
    public enum ExecutionStatus
    {
        Obsolete = 0,

        Executing = 1,

        WaitingFoThread = 2,

        BetweenRtries = 3,

        Idle = 4,

        Suspended = 5,

        PerformingCompletionActions = 6
    }

    /// <summary>
    /// Last run date and next run date
    /// </summary>
    public enum DateType
    {
        Last = 0,
        Next = 1
    }

    /// <summary>
    /// Frequent Time to of Schedule
    /// </summary>
    public enum FrequentTime : int
    {
        OneTimeOnly = 1,
        Daily = 4,
        Weekly = 8,
        Monthly = 16,
        MonlyRelative = 32,
        SqlAgentStart = 64,
        ComputerIdle = 128
    }

    /// <summary>
    /// Frequen Sub Time of Schedule
    /// </summary>
    public enum FrequentSubTime : int
    {
        SpecificTime = 1,
        Seconds = 2,
        Minutes = 4,
        Hours = 8
    }

    /// <summary>
    /// Search schedule by Date Type
    /// </summary>
    public enum SearchDate : int
    {
        All = 0,
        StartDate = 1,
        EndDate = 2
    }

    /// <summary>
    /// Step run status
    /// </summary>
    public enum StepRunStatus
    {
        Success = 1,
        Fail = 0
    }

    /// <summary>
    /// Server Log Type
    /// </summary>
    public enum LogType : int
    {
        SqlServerLog = 1,
        SqlAgentLog = 2
    }

    /// <summary>
    /// Error Type
    /// </summary>
    public enum ErrorType : int
    {
        Error = 1,
        Warning = 2,
        Infomation = 3
    }

    /// <summary>
    /// Server Type for get Connection String
    /// </summary>
    public enum ServerType : int
    {
        Normal = 0,
        Resource = 1
    }
}
