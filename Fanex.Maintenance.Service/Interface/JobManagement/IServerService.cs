// --------------------------------------------------------------------------
// <copyright file="IServerService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.26">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Interface.JobManagement
{
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Maintenance.Entity.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public interface IServerService
    {
        ServerFullInfo GetServerFullInformation(string serverIp);

        ResourceInfo GetServerResourceInfo(string connectionString);

        ServerStatus GetServerStatus(string connectionString);
    }
}
