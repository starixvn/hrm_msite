// --------------------------------------------------------------------------
// <copyright file="LogController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.20">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Impl
{
    using System;
    using System.Collections;
    using Fanex.Data;
    using Fanex.Maintenance.Repository.Interface;
    using Fanex.NAP.Framework.Data;
    using Fanex.Maintenance.Criteria;
    using Fanex.Maintenance.Entity.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UserRepository : ReadonlyRepository<dynamic>, IUserRepository
    {
        /// <summary>
        /// Check login
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public ICommandResult Login(ICriteria criteria)
        {
            try
            {
                Hashtable hash = criteria.GetSpParams();
                hash = (new ResultCriteria()).AddSpParams(hash);
                using (ObjectDb objectDb = new ObjectDb(criteria.GetSettingKey()))
                {
                    objectDb.ExecuteNonQuery(hash);

                    return GetCommandResult(hash);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get error code and error message
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        protected CommandResult GetCommandResult(Hashtable hash)
        {
            var errCode = hash["errcode"];
            var errMsg = hash["errmsg"];

            return new CommandResult
            {
                Code = (errCode is DBNull) ? 0 : Convert.ToInt32(errCode),
                Message = (errMsg is DBNull) ? string.Empty : Convert.ToString(errMsg)
            };
        }
    }
}