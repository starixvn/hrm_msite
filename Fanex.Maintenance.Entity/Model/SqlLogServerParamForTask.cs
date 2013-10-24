// --------------------------------------------------------------------------
// <copyright file="SqlLogServerParamForTask.cs" company="Nexcel Solutions Vietnam">
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
    /// SQL server log parameter for task.
    /// </summary>
    public class SqlLogServerParamForTask
    {
        public int LogType { get; set; }

        public string ServerIp { get; set; }
    }
}
