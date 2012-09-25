// --------------------------------------------------------------------------
// <copyright file="GetLogCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.20">Created</change>
// </history>

namespace Fanex.Maintenance.Core.Criteria.Log
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.NAP.Framework.Data;
    using System.ComponentModel.DataAnnotations;

    public class GetLogCriteria : CriteriaBase
    {
        public int CategoryId { get; set; }

        public string Severity { get; set; }

        [DataType(DataType.Date)]
        public DateTime From { get; set; }

        [DataType(DataType.Date)]
        public DateTime To { get; set; }

        public int Page { get; set; }

        public int Size { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "Logging_Sel_Padding";
        }
    }
}
