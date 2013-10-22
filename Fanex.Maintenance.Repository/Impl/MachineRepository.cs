// --------------------------------------------------------------------------
// <copyright file="MachineRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.8.7">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Impl
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Fanex.Data;
    using Fanex.Maintenance.Criteria.Log;
    using Fanex.Maintenance.Criteria.Misc;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Machine Repository.
    /// </summary>
    public class MachineRepository : BaseRepository<Machine>
    {
        /// <summary>
        /// Return Machine list.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        public override IEnumerable<Machine> Fetch(ICriteria criteria)
        {
            try
            {
                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    var param = criteria.GetSpParams();
                    return db.Query<Machine>(param);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Insert machine.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        public CommandResult InsertMachine(InsertMachineCriteria criteria)
        {
            try
            {
                Hashtable hash = criteria.GetSpParams();
                hash = (new ResultCriteria()).AddSpParams(hash);

                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    db.ExecuteNonQuery(hash);
                    return GetCommandResult(hash);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update machine.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        public CommandResult UpdateMachine(UpdateMachineCriteria criteria)
        {
            try
            {
                Hashtable hash = criteria.GetSpParams();
                hash = (new ResultCriteria()).AddSpParams(hash);

                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    db.ExecuteNonQuery(hash);
                    return GetCommandResult(hash);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}