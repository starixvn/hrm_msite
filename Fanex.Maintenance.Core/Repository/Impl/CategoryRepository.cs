// --------------------------------------------------------------------------
// <copyright file="CategoryRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.20">Created</change>
// </history>

namespace Fanex.Maintenance.Core.Repository.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.NAP.Framework.Data;
    using Fanex.Maintenance.Core.Model;
    using Fanex.Data;

    public class CategoryRepository : ReadonlyRepository<Categorys>
    {
        /// <summary>
        /// Override return Categorys
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IEnumerable<Categorys> Fetch(ICriteria criteria)
        {
            try
            {
                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    return db.Query<Categorys>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
