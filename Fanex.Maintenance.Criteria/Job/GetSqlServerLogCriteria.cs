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
        /// Gets or sets value of error log file you want to read.
        /// </summary>
        /// <value>0 = current, 1 = Archive #1, 2 = Archive #2, etc... </value>
        public int? P1 { get; set; }

        /// <summary>
        /// Gets or sets log file type.
        /// </summary>
        /// <value>1 or NULL = error log, 2 = SQL Agent log.</value>
        public int? P2 { get; set; }

        /// <summary>
        /// Gets or sets search string.
        /// </summary>
        /// <value>1: String one you want to search for.</value>
        public string P3 { get; set; }

        /// <summary>
        /// Gets or sets search string.
        /// </summary>
        /// <value>2: String two you want to search for to further refine the results.</value>
        public string P4 { get; set; }

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
