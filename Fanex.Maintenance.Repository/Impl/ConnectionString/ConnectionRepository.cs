// --------------------------------------------------------------------------
// <copyright file="ConnectionRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.22">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Impl.ConnectionString
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Fanex.Common.Extensions;
    using Fanex.ConnectionString.Domain;
    using Fanex.Data;
    using Fanex.Maintenance.Repository.Interface.ConnectionString;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ConnectionRepository : BaseRepository<ConnectionString>, IConnectionRepository
    {
        public IEnumerable<ConnectionString> GetByAlais(ICriteria criteria)
        {
            try
            {
                Func<IDataReader, ConnectionString> objMapFunc = (reader) =>
                {
                    return new ConnectionString
                    {
                        Id = reader.ToInt("Id"),
                        Alias = new Alias
                        {
                            Id = reader.ToInt("Id"),
                            Name = reader.ToString("Name")
                        },
                        InitialCatalog = reader.ToString("InitialCatalog"),
                        IntegratedSecurity = reader.ToBool("IntegratedSecurity"),
                        UserID = reader.ToString("UserId"),
                        Password = reader.ToString("Password"),
                        FreeText = reader.ToString("FreeText"),
                        Server = new DatabaseServer
                        {
                            ServerIP = reader.ToString("ServerIP")
                        },
                        IsDefault = reader.ToBool("IsDefault")
                    };
                };

                return this.Fetch<ConnectionString>(criteria, objMapFunc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertOrUpdate(ICriteria criteria)
        {
            try
            {
                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    var hsParam = criteria.GetSpParams();
                    db.ExecuteNonQuery(hsParam);

                    return 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}