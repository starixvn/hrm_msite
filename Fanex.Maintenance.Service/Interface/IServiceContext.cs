// --------------------------------------------------------------------------
// <copyright file="IServiceContext.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.25">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Interface
{
    public interface IServiceContext
    {
        /// <summary>
        /// who perform a business action
        /// </summary>
        int UserId { get; set; }

        string Ip { get; set; }
    }
}
