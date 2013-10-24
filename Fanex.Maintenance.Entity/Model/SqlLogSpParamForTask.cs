// --------------------------------------------------------------------------
// <copyright file="SqlLogSpParamForTask.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.10">Created</change>
//   <change who="Ricky.Nguyen" date="2012.10.22">Update Task Object</change>
// </history>

namespace Fanex.Maintenance.Entity.Model
{
    /// <summary>
    /// SQL server log SP parameter for task.
    /// </summary>
    public class SqlLogSpParamForTask
    {
        public int P1 { get; set; }

        public int P2 { get; set; }

        public string ServerIp { get; set; }
    }
}
