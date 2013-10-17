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
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Common.Extensions;
    using Fanex.ConnectionString.Domain;
    using Fanex.Data;
    using Fanex.Maintenance.Criteria.Misc;
    using Fanex.Maintenance.Entity.Model.ConnectionString;
    using Fanex.Maintenance.Repository.Interface.ConnectionString;
    using Fanex.NAP.Framework.Data;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class ConnectionRepository : BaseRepository<ConnectionString>, IConnectionRepository
    {
        public IEnumerable<ConnectionString> GetSecondaryConection(ICriteria criteria)
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

        public IEnumerable<ConnectionStringConnection> GetByAlais(ICriteria criteria)
        {
            try
            {
                Func<IDataReader, ConnectionStringConnection> objMapFunc = (reader) =>
                {
                    return new ConnectionStringConnection
                    {
                        ConnectionString = new ConnectionString
                        {
                            Id = reader.ToInt("Id"),
                            Alias = new Alias
                            {
                                Id = reader.ToInt("AliasId"),
                                Name = reader.ToString("Name")
                            },
                            InitialCatalog = reader.ToString("InitialCatalog"),
                            IntegratedSecurity = reader.ToBool("IntegratedSecurity"),
                            UserID = reader.ToString("UserId"),
                            Password = reader.ToString("Password"),
                            FreeText = reader.ToString("FreeText"),
                            Server = !reader.IsDBNull("DbServerId") ? new DatabaseServer
                            {
                                Id = reader.ToInt("DbServerId"),
                                Name = reader.ToString("ServerName"),
                                ServerIP = reader.ToString("ServerIP")
                            } 
                            : new DatabaseServer(),
                            Modifier = !reader.IsDBNull("ModifierUserId") ? new Users
                            {
                                Id = reader.ToInt("ModifierUserId"),
                                Name = reader.ToString("ModifierUsername")
                            } 
                            : new Users(),
                            ModifiedDate = !reader.IsDBNull("ModifiedDate") ? reader.ToDateTime("ModifiedDate") : DateTime.Now,
                            IsDefault = reader.ToBool("IsDefault")
                        },
                        NumOfConection = reader.ToInt("NumOfConection")
                    };
                };

                return this.Fetch<ConnectionStringConnection>(criteria, objMapFunc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ConnectionString GetById(ICriteria criteria)
        {
            try
            {
                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    return db.QueryEntity<ConnectionString, DatabaseServer, Alias, ConnectionString, ConnectionString>(
                        (c, d, a, c1) => { c.Server = d; c.Alias = a; c.PrimaryConnection = c1; return c; },
                        "ServerId,AliasId,PrimaryConectionId",
                        criteria.GetSpParams());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICommandResult InsertOrUpdate(ICriteria criteria, ICriteria logCriteria)
        {
            try
            {
                Hashtable hash = criteria.GetSpParams();
                hash = logCriteria.AddSpParams(hash);
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