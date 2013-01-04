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
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;
    using Fanex.Logging;
    using Fanex.Maintenance.Controller.Hubs;
    using Fanex.Maintenance.Core.Configurations;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Service.Impl.JobManagement;
    using Fanex.Maintenance.Service.Interface.JobManagement;
    using Microsoft.AspNet.SignalR.Hubs;
    using Microsoft.AspNet.SignalR;
    using Fanex.Maintenance.Core.Web;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class ServerInfoWorker : BaseWorker
    {
        private int _getDataInterval { get; set; }

        private IServerService _serverService { get; set; }

        private static Dictionary<string, bool> _checkingList;

        private IHubContext _hubContext;

        public ServerInfoWorker(
            long getDataInterval = 10000)
            : base()
        {
            _getDataInterval = (int)getDataInterval;
            _serverService = ServerService.Create();
        }

        /// <summary>
        /// Run main function
        /// </summary>
        protected override void Run()
        {
            //// Init check list for query
            _checkingList = new Dictionary<string, bool>();
            foreach (ServerElement server in DatabaseServersSection.Instance.ResourceServers)
            {
                _checkingList.Add(server.ServerIp, false);
            }

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
                    if (!Constants.IsUMTime)
                    {
                        GetListResourceInfo();
                    }
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
            _hubContext = GlobalHost.ConnectionManager.GetHubContext<ServerInfoHub>();
            foreach (ServerElement server in DatabaseServersSection.Instance.ResourceServers)
            {
                Task.Factory.StartNew(UpdateResourceInfoToClient, server);
            }
        }

        /// <summary>
        /// Get Resource info and return to client
        /// </summary>
        /// <param name="serverIp"></param>
        private void UpdateResourceInfoToClient(object server)
        {
            try
            {
                ServerElement serverElement = server as ServerElement;
                if (serverElement != null
                    && _checkingList[serverElement.ServerIp] == false)
                {
                    //// Update that server is doing, so other thread cannot access
                    _checkingList[serverElement.ServerIp] = true;
                    //// Get Data
                    ResourceInfo resourceInfo = _serverService.GetServerResourceInfo(serverElement.ConnectionString);
                    resourceInfo.ServerIp = serverElement.ServerIp.ToString();
                    resourceInfo.ServerName = resourceInfo.ServerIp.Replace(".", "_");
                    if (_hubContext != null)
                    {
                        _hubContext.Clients.All.gotMessage(resourceInfo);
                    }

                    _checkingList[serverElement.ServerIp] = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.ToString(), ex);
            }
        }
    }
}