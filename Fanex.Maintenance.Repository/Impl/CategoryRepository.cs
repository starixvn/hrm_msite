// --------------------------------------------------------------------------
// <copyright file="CategoryRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.20">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Impl
{
    using System;
    using System.Collections.Generic;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using Fanex.Maintenance.Entity.Model;

    public class CategoryRepository : ReadonlyRepository<LogCategorys>
    {
        /// <summary>
        /// Override return Categorys
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IEnumerable<LogCategorys> Fetch(ICriteria criteria)
        {
            try
            {
                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    return db.Query<LogCategorys>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}