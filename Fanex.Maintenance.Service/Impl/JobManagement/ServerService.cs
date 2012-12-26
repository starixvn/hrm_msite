// -----------------------------------------------------------------------
// <copyright file="ServerService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Maintenance.Service.Impl.JobManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Maintenance.Service.Interface.JobManagement;
    using Fanex.Maintenance.Entity.Model;
using Fanex.Maintenance.Repository.Interface.JobManagement;
    using Fanex.Maintenance.Repository.Impl.JobManagement;
    using Fanex.Maintenance.Criteria.Job;
    using Fanex.Logging;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
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
        /// Get Server Resource Info for Update User View throught SignalR
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
