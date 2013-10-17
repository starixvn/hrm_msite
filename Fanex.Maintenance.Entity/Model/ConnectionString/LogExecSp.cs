// --------------------------------------------------------------------------
// <copyright file="LogExecSp.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.28">Created</change>
// </history>

namespace Fanex.Maintenance.Entity.Model.ConnectionString
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LogExecSp
    {
        public long Id { get; set; }

        public DateTime LogDate { get; set; }

        public string Name { get; set; }

        public string Modifier { get; set; }

        public string ActionName { get; set; }

        public int ActionId { get; set; }

        public string OldData { get; set; }

        public string Data { get; set; }

        public int LogType { get; set; }

        public string Items { get; set; }

        public string SubItems { get; set; }
    }
}
