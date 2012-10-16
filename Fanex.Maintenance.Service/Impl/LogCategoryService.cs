// --------------------------------------------------------------------------
// <copyright file="CategoryService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.20">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Impl
{
    using System.Collections.Generic;
    using Fanex.Maintenance.Criteria.Log;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Repository.Impl;

    public class LogCategoryService
    {
        #region Member

        private readonly LogCategoryRepository _categoryRepository = new LogCategoryRepository();

        #endregion Member

        #region Ctor

        /// <summary>
        /// Constructor
        /// </summary>
        private LogCategoryService()
        {
        }

        public static LogCategoryService Create()
        {
            return new LogCategoryService();
        }

        #endregion Ctor

        /// <summary>
        /// Get all category
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LogCategory> GetAll()
        {
            GetAllCategoryCritria criteria = new GetAllCategoryCritria();

            return _categoryRepository.Fetch(criteria);
        }
    }
}