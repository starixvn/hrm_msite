// --------------------------------------------------------------------------
// <copyright file="IServerService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.26">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Interface.JobManagement
{
    using Fanex.Maintenance.Entity.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IServerService
    {
        ServerFullInfo GetServerFullInformation(string serverIp);

        ResourceInfo GetServerResourceInfo(string connectionString);

        CPUInfo GetCpuUtilization(string connectionString);

        MemoryInfo GetMemoryUtilization(string connectionString);

        ServerStatus GetServerStatus(string connectionString);
    }
}
