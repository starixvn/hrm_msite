// --------------------------------------------------------------------------
// <copyright file="IBaseService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.25">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Interface
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Maintenance.Criteria.Misc;

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public interface IBaseService
    {
        IServiceContext ServiceContext { get; set; }

        void HandleException(Exception ex);

        LogExecSpCriteria GetLogExecSpCriteria();
    }
}
