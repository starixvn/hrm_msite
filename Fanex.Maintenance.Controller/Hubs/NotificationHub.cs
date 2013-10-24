// --------------------------------------------------------------------------
// <copyright file="NotificationHub.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.17">Created</change>
// </history>

namespace Fanex.Maintenance.Controller.Hubs
{
    using Microsoft.AspNet.SignalR.Hubs;

    /// <summary>
    /// Notification hub.
    /// </summary>
    public class NotificationHub : Hub
    {
        public void SendData(string data)
        {
            Clients.All.addMessage(data);
        }
    }
}
