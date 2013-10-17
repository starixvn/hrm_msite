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
    using System;
    using System.Collections.Generic;
    using Fanex.Maintenance.Core.Configurations;
    using Fanex.Maintenance.Core.Helper;
    using Fanex.Maintenance.Criteria.Job;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Repository.Impl.JobManagement;
    using Microsoft.AspNet.SignalR.Hubs;

    /// <summary>
    /// Server information hub.
    /// </summary>
    public class ServerInfoHub : Hub
    {
        /// <summary>
        /// Send data.
        /// </summary>
        public void SendData(string data)
        {
            try
            {
                Clients.All.addMessage(data);
                string currentData = "Data: " + data + "<br/>"
                    + "Clients: " + (Clients.All == null).ToString() + "<br/>"
                    + "ClientCaller: " + (Clients.Caller == null).ToString() + "<br/>";

                MailHelper.SendMail("Test Hub", "Test Hub", currentData);
            }
            catch (Exception ex)
            {
                MailHelper.SendMail("Test Hub", "Test Hub", ex.Message + "<br/>" + ex.StackTrace);
            }
        }

        /// <summary>
        /// Get list server information first time.
        /// </summary>
        public void GetListServerInfoFirstTime()
        {
            List<ResourceInfo> resourceInfoes = new List<ResourceInfo>();

            foreach (ServerElement server in DatabaseServersSection.Instance.ResourceServers)
            {
                ResourceInfo resourceInfo = 
                    (new ServerRepository()).GetServerResourceInfo(new GetResourceInfoCriteria(), server.ConnectionString);

                resourceInfo.ServerIp = server.ServerIp;
                resourceInfo.ServerName = server.ServerIp.Replace(".", "_");
                resourceInfoes.Add(resourceInfo);
            }

            Clients.All.firstMessage(resourceInfoes);
        }
    }
}
