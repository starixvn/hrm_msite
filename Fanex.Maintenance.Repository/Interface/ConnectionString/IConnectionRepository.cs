// --------------------------------------------------------------------------
// <copyright file="IConnectionRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.22">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Interface.ConnectionString
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.ConnectionString.Domain;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IConnectionRepository
    {
        IEnumerable<ConnectionString> GetByAlais(ICriteria criteria);

        ConnectionString GetById(ICriteria criteria);

        int InsertOrUpdate(ICriteria criteria);
    }
}
