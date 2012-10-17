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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using SignalR.Hubs;
    using Fanex.Maintenance.Entity.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class NotificationHub : Hub
    {
        public void SendData(string data)
        {
            Clients.addMessage(data);
        }
    }
}
