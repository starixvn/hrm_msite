// --------------------------------------------------------------------------
// <copyright file="CategoryService.cs" company="Nexcel Solutions Vietnam">
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
    using Fanex.Maintenance.Core.Model;
    using Fanex.Maintenance.Core.Criteria.Category;
    using Fanex.Maintenance.Core.Repository.Impl;

    public class CategoryService
    {
        #region Member

        private readonly CategoryRepository _categoryRepository = new CategoryRepository();

        #endregion

        #region Ctor

        private CategoryService()
        {
        }

        public static CategoryService Create()
        {
            return new CategoryService();
        }


        #endregion

        /// <summary>
        /// Get all categorys
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Categorys> GetAll()
        {
            GetAllCategoryCritria criteria = new GetAllCategoryCritria();

            return _categoryRepository.Fetch(criteria);
        }
    }
}
