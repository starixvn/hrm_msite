// --------------------------------------------------------------------------
// <copyright file="GetSqlServerLogCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.10">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.Job
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Fanex.Maintenance.Core;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class GetSqlServerLogCriteria : CriteriaBase
    {
        [SpParam(Ignored = true)]
        public string Server { get; set; }

        [SpParam(Ignored = true)]
        [Display(Name = "From", ResourceType = typeof(ViewResource))]
        public DateTime From { get; set; }

        [SpParam(Ignored = true)]
        [Display(Name = "To", ResourceType = typeof(ViewResource))]
        public DateTime To { get; set; }

        [SpParam(Ignored = true)]
        public int? ErrorLevel { get; set; }

        [SpParam(Ignored = true)]
        public int? PageSize { get; set; }

        [SpParam(Ignored = true)]
        public int? PageIndex { get; set; }

        /// <summary>
        /// Value of error log file you want to read: 0 = current, 1 = Archive #1, 2 = Archive #2, etc... 
        /// </summary>
        public int? p1 { get; set; }

        /// <summary>
        /// Log file type: 1 or NULL = error log, 2 = SQL Agent log 
        /// </summary>
        public int? p2 { get; set; }

        /// <summary>
        /// Search string 1: String one you want to search for 
        /// </summary>
        public string p3 { get; set; }

        /// <summary>
        /// Search string 2: String two you want to search for to further refine the results
        /// </summary>
        public string p4 { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "sp_readerrorlog";
        }
    }
}
