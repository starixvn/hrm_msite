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
    using Fanex.Maintenance.Criteria.LogCategory;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Repository.Impl;

    public class CategoryService
    {
        #region Member

        private readonly CategoryRepository _categoryRepository = new CategoryRepository();

        #endregion Member

        #region Ctor

        private CategoryService()
        {
        }

        public static CategoryService Create()
        {
            return new CategoryService();
        }

        #endregion Ctor

        /// <summary>
        /// Get all categorys
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LogCategorys> GetAll()
        {
            GetAllCategoryCritria criteria = new GetAllCategoryCritria();

            return _categoryRepository.Fetch(criteria);
        }
    }
}