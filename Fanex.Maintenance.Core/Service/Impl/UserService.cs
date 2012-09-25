// --------------------------------------------------------------------------
// <copyright file="LogController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.20">Created</change>
// </history>

namespace Fanex.Maintenance.Core.Service.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Maintenance.Core.Service.Interface;
    using Fanex.NAP.Framework.Data;
    using Fanex.Maintenance.Core.Repository.Interface;
    using Fanex.Maintenance.Core.Repository.Impl;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository = new UserRepository();

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public ICommandResult Login(ICriteria criteria)
        {
            if (criteria.IsValid())
            {
                return _userRepository.Login(criteria);
            }

            return null;
        }
    }
}
