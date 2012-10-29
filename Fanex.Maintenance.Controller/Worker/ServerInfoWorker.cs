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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using Fanex.Logging;
using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Core.Configurations;
using Fanex.Maintenance.Repository.Impl.JobManagement;
    using Fanex.Maintenance.Repository.Interface.JobManagement;
    using Fanex.Maintenance.Criteria.Job;
    using SignalR;
    using Fanex.Maintenance.Controller.Hubs;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ServerInfoWorker : BaseWorker
    {
        private int _getDataInterval { get; set; }
        private IServerRepository _serverRepository { get; set; }

        public ServerInfoWorker(
            int getDataInterval = 60000)
            : base()
        {
            _getDataInterval = getDataInterval;
            _serverRepository = new ServerRepository();
        }

        /// <summary>
        /// Run main function
        /// </summary>
        protected override void Run()
        {
            while (true)
            {
                Thread.Sleep(_getDataInterval);

                if (IsStop)
                {
                    break;
                }


                try
                {
                    GetListResourceInfo();
                }
                catch (Exception ex)
                {
                    Logger.Log.Error(ex.Message, ex);
                }
            }
        }

        /// <summary>
        /// Get List Server Resource info
        /// </summary>
        private void GetListResourceInfo()
        {
            //List<ResourceInfo> resourceInfoes = new List<ResourceInfo>();
            var context = GlobalHost.ConnectionManager.GetHubContext<ServerInfoHub>();
            foreach (ServerElement server in DatabaseServersSection.Instance.ResourceServers)
            {
                try
                {
                    ResourceInfo resourceInfo = _serverRepository.GetServerResourceInfo(new GetResourceInfoCriteria(), server.ConnectionString);
                    resourceInfo.ServerIp = server.ServerIp;
                    resourceInfo.ServerName = server.ServerIp.Replace(".", "_");
                    context.Clients.gotMessage(resourceInfo);
                }
                catch (Exception ex)
                {
                    Logger.Log.Error(ex.ToString(), ex);
                }
            }
        }
    }
}
