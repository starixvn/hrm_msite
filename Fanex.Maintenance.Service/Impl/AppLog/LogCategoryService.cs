// --------------------------------------------------------------------------
// <copyright file="LogCategoryService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.20">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Impl.AppLog
{
    using System.Collections.Generic;
    using Fanex.Maintenance.Repository.Criteria.Log;
    using Fanex.NAP.Framework.Data;
    using Fanex.Maintenance.Core.Model;

    /// <summary>
    /// Log category service.
    /// </summary>
    public class LogCategoryService
    {
        private readonly ReadonlyRepository<LogCategory> _categoryRepository = new ReadonlyRepository<LogCategory>();

        private LogCategoryService()
        {
        }

        /// <summary>
        /// Create log category service.
        /// </summary>
        /// <returns>Log category service.</returns>
        public static LogCategoryService Create()
        {
            return new LogCategoryService();
        }

        /// <summary>
        /// Get all category.
        /// </summary>
        /// <returns>List of log category.</returns>
        public IEnumerable<LogCategory> GetAll()
        {
            GetAllCategoryCriteria criteria = new GetAllCategoryCriteria();

            return _categoryRepository.Fetch(criteria);
        }
    }
}