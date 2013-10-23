// --------------------------------------------------------------------------
// <copyright file="ServerInfoWorker.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.24">Created</change>
// </history>

namespace Fanex.Maintenance.Controller.Worker
{
    using System;
    using System.Threading;
    using Fanex.Logging;
    using Fanex.Maintenance.Controller.Hubs;
    using Fanex.Maintenance.Core.Configurations;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Service.Impl.JobManagement;
    using Fanex.Maintenance.Service.Interface.JobManagement;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CpuUtilizationWorker : BaseWorker
    {
        private IHubContext _hubContext;

        public CpuUtilizationWorker(long getDataInterval = 10000) : base()
        {
            _getDataInterval = (int)getDataInterval;
            _serverService = ServerService.Create();
        }

        private int _getDataInterval { get; set; }

        private IServerService _serverService { get; set; }

        /// <summary>
        /// Run main function.
        /// </summary>
        protected override void Run()
        {
            //// Main Function
            while (true)
            {
                Thread.Sleep(_getDataInterval);
                if (IsStop)
                {
                    break;
                }

                try
                {
                    if (!this.IsDownTime)
                    {
                        GetListCpuUtilization();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log.Error(ex.Message, ex);
                }
            }
        }

        /// <summary>
        /// Get List Server Resource info.
        /// </summary>
        private void GetListCpuUtilization()
        {
            //List<ResourceInfo> resourceInfoes = new List<ResourceInfo>();
            _hubContext = GlobalHost.ConnectionManager.GetHubContext<ServerInfoHub>();
            try
            {
                foreach (ServerElement serverElement in DatabaseServersSection.Instance.ResourceServers)
                {
                    if (serverElement != null)
                    {
                        //// Get Data
                        CPUInfo cpuInfo = _serverService.GetCpuUtilization(serverElement.ConnectionString);
                        cpuInfo.ServerIp = serverElement.ServerIp.ToString();
                        cpuInfo.ServerName = cpuInfo.ServerIp.Replace(".", "_");
                        if (_hubContext != null)
                        {
                            _hubContext.Clients.All.getCpuInfo(cpuInfo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.ToString(), ex);
            }
        }
    }
}