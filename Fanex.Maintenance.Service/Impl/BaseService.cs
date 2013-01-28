// --------------------------------------------------------------------------
// <copyright file="BaseService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.25">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Impl
{
    using System;
    using Fanex.Common.Helper;
    using Fanex.Logging;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Maintenance.Service.Interface;
    using Fanex.Maintenance.Criteria.Misc;

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class BaseService : IBaseService
    {
        public IServiceContext ServiceContext { get; set; }

        public void HandleException(Exception ex)
        {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
        }

        public LogExecSpCriteria GetLogExecSpCriteria()
        {
            Check.Argument.IsNotNull(this.ServiceContext, "ServiceContext");

            return new LogExecSpCriteria
            {
                CurrentUserId = this.ServiceContext.UserId,
                LogDate = DateTime.Now,
                LoginIp = this.ServiceContext.Ip
            };
        }
    }
}
