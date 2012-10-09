// -----------------------------------------------------------------------
// <copyright file="Contains.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Maintenance.Core.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class Constans
    {
        public const string JOB = "JOB";
        public const string STEPS = "STEPS";
        public const string SCEDULES = "SCEDULES";
        public const string TARGETS = "TARGETS";        

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

        
    }
}
