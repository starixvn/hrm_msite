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
    }
}
