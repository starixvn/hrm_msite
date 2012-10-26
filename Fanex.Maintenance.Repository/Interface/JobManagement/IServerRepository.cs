// --------------------------------------------------------------------------
// <copyright file="IServerRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.24">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Interface.JobManagement
{
    using Fanex.Maintenance.Entity.Model;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IServerRepository
    {
        ResourceInfo GetServerResourceInfo(ICriteria criteria, string connectionString);
        ServerFullInfo GetServerFullInformation(ICriteria criteria, string serverIp);
    }
}