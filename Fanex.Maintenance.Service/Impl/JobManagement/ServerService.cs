// --------------------------------------------------------------------------
// <copyright file="ServerService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.12.6">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Impl.JobManagement
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Logging;
    using Fanex.Maintenance.Criteria.Job;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Repository.Impl.JobManagement;
    using Fanex.Maintenance.Repository.Interface.JobManagement;
    using Fanex.Maintenance.Service.Interface.JobManagement;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class ServerService : IServerService
    {
        private IServerRepository _serverRepository;

        public ServerService()
        {
            _serverRepository = new ServerRepository();
        }

        public static IServerService Create()
        {
            return new ServerService();
        }

        /// <summary>
        /// Get Server Full information for showing in Detail Page
        /// </summary>
        /// <param name="serverIp"></param>
        /// <returns></returns>
        public ServerFullInfo GetServerFullInformation(string serverIp)
        {
            try
            {
                return _serverRepository.GetServerFullInformation(new GetCpuInfoCriteria(), serverIp);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                return new ServerFullInfo
                {
                    CpuName = "None"
                };
            }
        }

        /// <summary>
        /// Get Server Resource Info for Update User View through SignalR
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public ResourceInfo GetServerResourceInfo(string connectionString)
        {
            try
            {
                return _serverRepository.GetServerResourceInfo(new GetResourceInfoCriteria(), connectionString);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                return new ResourceInfo();
            }
        }

        public ServerStatus GetServerStatus(string connectionString)
        {
            try
            {
                return _serverRepository.GetServerStatus(new GetServerStatusCriteria(), connectionString);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                return new ServerStatus
                    {
                        SqlServerCannotConnect= false,
                        SqlServerAgentRunning = false
                    };
            }
        }
    }
}
