// --------------------------------------------------------------------------
// <copyright file="ServerInfoHub.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.24">Created</change>
// </history>

namespace Fanex.Maintenance.Controller.Hubs
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Maintenance.Core.Configurations;
    using Fanex.Maintenance.Criteria.Job;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Repository.Impl.JobManagement;
    using SignalR.Hubs;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class ServerInfoHub : Hub
    {
        public void SendData(string data)
        {
            Clients.addMessage(data);
        }

        /// <summary>
        /// Get List Server Resource info
        /// </summary>
        public void GetListServerInfoFirstTime()
        {
            List<ResourceInfo> resourceInfoes = new List<ResourceInfo>();

            foreach (ServerElement server in DatabaseServersSection.Instance.ResourceServers)
            {
                ResourceInfo resourceInfo = (new ServerRepository()).GetServerResourceInfo(new GetResourceInfoCriteria(), server.ConnectionString);
                resourceInfo.ServerIp = server.ServerIp;
                resourceInfo.ServerName = server.ServerIp.Replace(".", "_");
                resourceInfoes.Add(resourceInfo);
            }

            //Caller.firstMessage(resourceInfoes);
            Clients.firstMessage(resourceInfoes);
        }
    }
}
