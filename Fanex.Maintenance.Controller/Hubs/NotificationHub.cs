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
    using System.Diagnostics.CodeAnalysis;
    using SignalR.Hubs;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class NotificationHub : Hub
    {
        public void SendData(string data)
        {
            Clients.addMessage(data);
        }
    }
}
